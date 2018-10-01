using JwtWebSocket;
using TMPro;
using UnityEngine;

public class ServerStatus
{
    public string databaseState, databaseNotice, serverName, serverVersion, onlineNotice, gameServerNotice;
    public bool onlineState, gameServerState;
}

public class StatusMessageHandler : MonoBehaviour
{
    public TextMeshProUGUI versionText, serverName, databaseState, onlineState, gameServerState;
    private ServerStatus data;
    
    private void Start()
    {
        GlobalObject globalObject = GlobalObject.Instance;
        OnMessageHandler handler = globalObject.MessageHandler;

        data = new ServerStatus();
        
        EventTag<ServerStatus> e = (EventTag<ServerStatus>) handler.SignTag("status", typeof(ServerStatus));
        e.Event += WriteState;
    }

    private void Update()
    {
        versionText.text = data.serverVersion;
        serverName.text = data.serverName;

        switch (data.databaseState)
        {
            case "disconnected":
                databaseState.color = Color.gray;
                break;
            case "connected":
                databaseState.color = Color.green;
                break;
            case "connecting":
                databaseState.color = Color.yellow;
                break;
            case "disconnecting":
                databaseState.color = Color.red;
                break;
        }

        if (data.onlineState)
        {
            onlineState.color = Color.green;
        }
        else
        {
            onlineState.color = Color.red;
        }

        if (data.gameServerState)
        {
            gameServerState.color = Color.green;
        }
        else
        {
            gameServerState.color = Color.red;
        }
    }

    private void WriteState(object sender, SocketMessage<ServerStatus> message)
    {
        data = message.data;
        if (data.databaseNotice != "")
        {
            Debug.LogWarning("Database notice :" + data.databaseNotice);
        }
        if (data.onlineNotice != "")
        {
            Debug.LogWarning("Online notice :" + data.onlineNotice);
        }
        if (data.gameServerNotice != "")
        {
            Debug.LogWarning("GameServer notice :" + data.gameServerNotice);
        }
    }
}