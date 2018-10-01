using JwtWebSocket;
using UnityEngine;
using UnityEngine.UI;

public class WhoamiButton : MonoBehaviour {

    private GlobalObject globalObject;
    private OnMessageHandler messageHandler;
    
	void Start () {
		globalObject = GlobalObject.Instance;
	    messageHandler = globalObject.MessageHandler;

	    EventTag<string> whoamiResponse = (EventTag<string>)messageHandler.SignTag("whoami/response", typeof(string));
	    whoamiResponse.Event += (sender, message) =>
	    {
            Debug.Log("whoami -> " + message.message);
	    };

	    Button button = GetComponent<Button>();
	    button.onClick.AddListener(OnClick);
	}

    void OnClick()
    {
        SocketMessage<string> message = new SocketMessage<string>("whoami", "", "");
        globalObject.Send(message, true);
    }
}
