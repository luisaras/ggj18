﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {

	public int n = 10;
	public Text myText;

	// Use this for initialization
	void Start () {
		myText = GetComponent<Text> ();
		myText.text = "";
		for (int i = 1; i <= n; i++) {
			if (!PlayerPrefs.HasKey("Time" + i))
				break;
			myText.text += PlayerPrefs.GetFloat ("Time" + i) + "\n";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
}
