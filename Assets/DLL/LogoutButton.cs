using JwtWebSocket;
using UnityEngine;
using UnityEngine.UI;

public class LogoutButton : MonoBehaviour {

    private GlobalObject globalObject;
    private OnMessageHandler messageHandler;
    
    void Start () {
        globalObject = GlobalObject.Instance;
        messageHandler = globalObject.MessageHandler;

        EventTag<string> logoutResponse = (EventTag<string>)messageHandler.SignTag("logout/response", typeof(string));
        logoutResponse.Event += (sender, message) =>
        {
            Debug.Log("logout -> " + message.message);
        };

        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        SocketMessage<string> message = new SocketMessage<string>("logout", "", "");
        globalObject.Send(message, true);
    }
}
