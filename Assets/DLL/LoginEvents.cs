using System;
using JwtWebSocket;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginBody : EventArgs
{
    public string identification, password;

    public LoginBody(string identification, string password)
    {
        this.identification = identification;
        this.password = password;
    }
}

public class LoginEvents : MonoBehaviour
{
    
    public TextMeshProUGUI identificationInput, passwordInput;
    private GlobalObject globalObject;
    private OnMessageHandler messageHandler;
    
	private void Start () {
		globalObject = GlobalObject.Instance;
	    messageHandler = globalObject.MessageHandler;

	    EventTag<string> loginResponse =
	        (EventTag<string>) messageHandler.SignTag("login/response", typeof(string));
	    loginResponse.Event += (sender, message) =>
	    {
            Debug.Log(message.message);
	        if (message.data != null)
	        {
	            Debug.Log("we got something : " + message.data);
	        }
	    };

	    Button button = GetComponent<Button>();
	    button.onClick.AddListener(OnClick);
	}

    public void OnClick()
    {
        string identification = GlobalObject.UnsharpifyString(identificationInput.text);
        string password = GlobalObject.UnsharpifyString(passwordInput.text);
        SocketMessage<LoginBody> body = new SocketMessage<LoginBody>("login", "",
            new LoginBody(identification, password));
        globalObject.Send(body, true);
    }
}
