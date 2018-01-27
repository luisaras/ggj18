using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public Player player;
	public GameObject explosion;

	void Update() {
		if ((transform.position - player.transform.position).magnitude < 0.5) {
			GameObject exp = Instantiate (explosion);
			exp.transform.position = player.transform.position;
			player.Die ();
			Destroy (gameObject);
		}
	}

}
