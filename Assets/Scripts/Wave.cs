using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
	
	bool back = false;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D> ().velocity = Player.instance.d;
        Destroy(gameObject, 30);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			if(back){
            	GetComponent<AudioSource>().Play(); // Quando bater no submarino
			}
		} else if(other.gameObject.CompareTag("Bomb")) {
            if (!back) {
                GetComponent<Rigidbody2D>().velocity *= -1;
                back = true;
            }
		}
	}

}
