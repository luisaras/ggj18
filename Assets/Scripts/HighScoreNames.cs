using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreNames : MonoBehaviour {

	public Text myText;

	void Start () {
		myText = GetComponent<Text> ();
		myText.text = "";
		for (int i = 1; i <= WinGUI.n; i++) {
			if (!PlayerPrefs.HasKey("Name" + i))
				break;
			myText.text += PlayerPrefs.GetString ("Name" + i) + "\n";
		}
	}

}
