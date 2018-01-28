using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScore : MonoBehaviour {

	public int entryNumber = 10;
	public int score;
	// Use this for initialization
	void Start () {

		//SCORES
		for (int i = 0; i <entryNumber ; i++) {
			if (score > PlayerPrefs.GetInt ("Time" + i, 0)) {
				int j = 8;
				while (j != i) {
					PlayerPrefs.SetInt ("Time" + (j+1), PlayerPrefs.GetInt ("Time" + j)); //pega do j e joga pro j+1
					j--;
				}
				PlayerPrefs.SetInt ("Time" + i, score); //coloca o I no lugar dele

				//NAMES
				int k = 8;
				while (k != i) {
					PlayerPrefs.SetString ("Name" + (k+1), PlayerPrefs.GetString ("Name" + k)); //pega do j e joga pro j+1
					k--;
				}
				PlayerPrefs.SetString ("Name" + i, name); //coloca o I no lugar dele

			}
		}



	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
