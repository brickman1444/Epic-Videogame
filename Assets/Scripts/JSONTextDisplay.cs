using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JSONTextDisplay : SingletonBehaviour<JSONTextDisplay> {

	// Use this for initialization
	void Start () {
        HideText();
	}

    public void SetText(string str)
    {
        GetComponent<Text>().text = str;
    }

    public void ShowText()
    {
        GetComponent<Text>().enabled = true;
    }

    public void HideText()
    {
        GetComponent<Text>().enabled = false;
    }
}
