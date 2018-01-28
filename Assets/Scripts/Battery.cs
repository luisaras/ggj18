﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {

    public GameObject explosion;

    void Update() {
        if (!Player.instance) {
            return;
        }
        if (Player.instance.dead) {
            return;
        }
        if ((transform.position - Player.instance.transform.position).magnitude < 0.5) {
            // GameObject exp = Instantiate(explosion);
            // exp.transform.position = Player.instance.transform.position;
            Player.instance.fillBattery();
            Destroy(gameObject);
        }
    }
}
