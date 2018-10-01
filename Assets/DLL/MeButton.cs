using JwtWebSocket;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class Me
{
    [JsonProperty("account-type")]
    public string accountType;
    [JsonProperty("screen-name")]
    public string screenName;

    public string email, createdAt;
}

public class MeButton : MonoBehaviour {

    private GlobalObject globalObject;
    private OnMessageHandler messageHandler;
    
    void Start () {
        globalObject = GlobalObject.Instance;
        messageHandler = globalObject.MessageHandler;

        EventTag<Me> meResponse = (EventTag<Me>)messageHandler.SignTag("me/response", typeof(Me));
        meResponse.Event += (sender, message) =>
        {
            if (message.message != null)
            {
                Debug.Log("me -> " + message.message);
            }
            else
            {
                Debug.Log("me -> " + JsonConvert.SerializeObject(message.data));
            }
        };

        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        SocketMessage<string> message = new SocketMessage<string>("me", "", "");
        globalObject.Send(message, true);
    }
}
