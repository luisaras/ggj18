using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour {
	
    public GameObject victory;

    void Update() {
        if ((transform.position - Player.instance.transform.position).magnitude < 0.5) {
            Instantiate(victory);
            Player.instance.Win();
            Destroy(gameObject);
        }
    }

}
