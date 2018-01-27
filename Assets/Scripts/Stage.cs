using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

	public static Stage instance;

	public int length;
	public int width;

	// Use this for initialization
	void Awake () {
		instance = this;
	}

}
