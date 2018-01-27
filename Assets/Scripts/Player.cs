﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	bool dead = false;
	bool paused = false;
	Vector2 origin;
	Vector2 d;
	float time = 1;

	public float speed = 1;
	public GameObject gui;
	
	// Update is called once per frame
	void Update () {
		if (dead)
			return;
		if (Input.GetButtonDown ("Pause")) {
			paused = !paused;
		}
		if (paused)
			return;
		if (time < 1) {
			time += Time.deltaTime;
			if (time >= 1) {
				time = 1;
			}
			transform.position = origin + time * d;
		} else {
			float dx = Input.GetButtonDown ("Horizontal") ? Input.GetAxisRaw ("Horizontal") : 0;
			float dy = Input.GetButtonDown ("Vertical") ? Input.GetAxisRaw ("Vertical") : 0;
			if (dx != 0 || dy != 0) {
				if (dx != d.x || dy != d.y) {
					float angle = Mathf.Atan2 (dy, dx) * Mathf.Rad2Deg - 90;
					transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
					d.x = dx;
					d.y = dy;
				} else {
					time = 0;
					origin = transform.position;
				}
			} else if (Input.GetButton("Fire")) {


			}
		}
	}

	public void Die() {
		gui.SetActive (true);
	}

}
