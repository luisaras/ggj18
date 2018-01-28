using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	public float speed = 0.5f;

	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp (transform.position, 
			Player.instance.transform.position + new Vector3 (0, 0, -5), 
			speed * Time.deltaTime * 60);
	}

}
