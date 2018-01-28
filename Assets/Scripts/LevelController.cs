using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void ChangeScreen (string level){
		SceneManager.LoadScene(level);
	}

	public static void Quit(){
		Application.Quit();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
