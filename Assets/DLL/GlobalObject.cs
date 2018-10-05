using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using JwtWebSocket;
using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class GlobalObject : MonoBehaviour
{
    private static GlobalObject instance;

    public static GlobalObject Instance
    {
        get { return instance; }
    }

    private Connection connection;

    public string ServerPath, JwtSecret;

    private OnMessageHandler messageHandler;

    public OnMessageHandler MessageHandler
    {
        get { return messageHandler; }
    }

    private void Awake()
    {
        instance = this;
        connection = new Connection(ServerPath, JwtSecret);
        messageHandler = new OnMessageHandler(DefaultTag);
        connection.MessageHandler = messageHandler;
        connection.SubscribeOnOpen(OnOpen);
        connection.SubscribeOnClose(OnClose);
    }

    private void Start()
    {
        connection.Start();

        List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        foreach (var assembly in assemblies)
        {
            Debug.Log(assembly.FullName);
            try
            {
                Debug.Log(assembly.Location);
            }
            catch
            {
                Debug.Log("no location");
            }
        }
//        string plainData = JsonConvert.SerializeObject(assemblies);
//        Debug.Log(plainData);
//        using (SHA256 hash = SHA256.Create())
//        {
//            byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(plainData));
//            StringBuilder builder = new StringBuilder();
//            foreach (var b in bytes)
//            {
//                builder.Append(b.ToString("x2"));
//            }
//            hashed = builder.ToString();
//        }
//        Debug.Log(hashed);
    }

    private void OnDestroy()
    {
        connection.Close();
    }

    private void DefaultTag(object sender, SocketMessage<object> message)
    {
        Debug.Log(message.tag + "/" + message.message);
    }

    private void OnOpen(object sender, EventArgs e)
    {
        Debug.Log("Connection was opened!");
    }

    private void OnClose(object sender, CloseEventArgs e)
    {
        Debug.Log("Connection was closed!");
    }

    public void Send(EventArgs data, bool jwt)
    {
        connection.Send(data, jwt);
    }
    
    public static string UnsharpifyString(string text)
    {
        return text.Remove(text.Length - 1);
    }
}