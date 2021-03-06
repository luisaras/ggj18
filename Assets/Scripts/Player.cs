﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Singleton
	public static Player instance;

 	// Public access
    public bool dead = false;
	public Vector2 d = new Vector2(0, 1);
	public float playTime;

	// Private
	bool paused = false;
    bool blocked = false;
    Vector2 origin;
	AudioSource moveAudio;
	AudioSource turnAudio;
	float moveTime = 1;

	// Game variables
	public float cooldown = 1;
	public float speed = 1;
	public float energy = 100;
	public float energyPower;
	public float energyLossDPS;
    
	// External game objects
	public GameObject wave;
	public GameObject losegui;
	public GameObject wingui;

    void Awake() {
        playTime = Time.time;
		AudioSource[] sources = GetComponents<AudioSource> ();
		moveAudio = sources [0];
		turnAudio = sources [1];
        instance = this;
    }

    void Start() {
        energyLossDPS = energy / 30;
        energyPower = energyLossDPS * 10;
    }

    // Update is called once per frame
    void Update () {
		if (dead)
			return;
		if (Input.GetButtonDown ("Pause")) {
			paused = !paused;
		}
		if (paused)
			return;
		if (moveTime < 1) {
			moveTime += Time.deltaTime * speed;
			if (moveTime >= 1) {
				moveTime = 1;
                Stage.instance.shiftAll(transform.eulerAngles.z);
                Stage.instance.createBorderBombs(transform.eulerAngles.z);
                Debug.Log(Stage.instance.printBoard());
			}
			transform.position = origin + moveTime * d;
		} else {

            float dx = Input.GetButtonDown ("Horizontal") ? Input.GetAxisRaw ("Horizontal") : 0;
			float dy = Input.GetButtonDown ("Vertical") ? Input.GetAxisRaw ("Vertical") : 0;

            if(Stage.gameMode == 1) {
                if (transform.position.x + dx < -Stage.instance.width) {
                    dx = 0;
                } else if (transform.position.x + dx > Stage.instance.width) {
                    dx = 0;
                }

                if (transform.position.x + dx < 0) {
                    dy = 0;
                } else if (transform.position.x + dx > Stage.instance.height) {
                    dy = 0;
                }
            }

			if (dx != 0 || dy != 0) {
				if (dx != d.x || dy != d.y) {
					turnAudio.Play ();
					float angle = Mathf.Atan2 (dy, dx) * Mathf.Rad2Deg - 90;
                    Vector3 aux = Camera.main.transform.eulerAngles;
					transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
                    Camera.main.transform.eulerAngles = aux;
					d.x = dx;
					d.y = dy;
				} else {
					moveAudio.Play ();
					moveTime = 0;
					origin = transform.position;
				}
			} else if (Input.GetButtonDown ("Fire") && !blocked) {
				GameObject gameObj = Instantiate(wave);
				gameObj.transform.position = transform.position;
                blocked = true;
                Invoke("Unblock", cooldown);
			}
		}
		if (Stage.gameMode == 2)
        	executeEnergyModule();
    }

    public void executeEnergyModule() {
        energy = energy - (energyLossDPS * Time.deltaTime);
        if(energy < 0) {
            Die();
        }
    }

    public void Unblock() {
        blocked = false;
    }

	public void Die() {
        GameObject LG = Instantiate(losegui, Stage.instance.transform.position, Quaternion.identity);
		LG.SetActive (true);
        dead = true;
	}

    public void Win() {
		playTime = Time.time - playTime;
        GameObject WG = Instantiate(wingui, Stage.instance.transform.position, Quaternion.identity);
        WG.SetActive(true);
        dead = true;
    }

    public void FillBattery() {
        energy = energy + energyPower;
    }
}
