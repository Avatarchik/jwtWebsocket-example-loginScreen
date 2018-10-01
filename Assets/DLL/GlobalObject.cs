using System;
using JwtWebSocket;
using Newtonsoft.Json;
using TMPro;
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