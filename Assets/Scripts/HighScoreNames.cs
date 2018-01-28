using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreNames : MonoBehaviour {

	public int n = 10;
	public Text myText;

	// Use this for initialization
	void Start () {
		myText = GetComponent<Text> ();
		myText.text = "";
		for (int i = 1; i <= n; i++) {
			if (!PlayerPrefs.HasKey("Name" + i))
				break;
			myText.text += PlayerPrefs.GetString ("Name" + i) + "\n";
		}
	}

	// Update is called once per frame
	void Update () {

	}

}
