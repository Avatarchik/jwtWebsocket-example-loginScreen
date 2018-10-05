using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour {

	void Start () {
		Button button = GetComponent<Button>();
		button.onClick.AddListener(OnClick);
	}

	void OnClick()
	{
		Application.Quit();
	}
}
