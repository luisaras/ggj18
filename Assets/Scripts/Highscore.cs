﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {

	public Text myText;

	// Use this for initialization
	void Start () {
		myText = GetComponent<Text> ();
		myText.text = "";
		for (int i = 1; i <= WinGUI.n; i++) {
			if (!PlayerPrefs.HasKey("Time" + i))
				break;
			float time = PlayerPrefs.GetFloat ("Time" + i);
			myText.text += time.ToString ("0.00") + "s\n";
		}
	}

}
