using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player instance;

    public float cooldown = 1;

    public bool dead = false;
	bool paused = false;

    bool blocked = false;
 
    Vector2 origin;
	public Vector2 d = new Vector2(0, 1);
	float time = 1;

	public float speed = 1;
	public GameObject wave;
	public GameObject losegui;
    public GameObject wingui;

    public float playTime;

	void Awake() {
        playTime = Time.time;
        instance = this;
    }

    private void Start() {
        Debug.Log(losegui);
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
		if (time < 1) {
			time += Time.deltaTime;
			if (time >= 1) {
				time = 1;
			}
			transform.position = origin + time * d;
		} else {

            float dx = Input.GetButtonDown ("Horizontal") ? Input.GetAxisRaw ("Horizontal") : 0;
			float dy = Input.GetButtonDown ("Vertical") ? Input.GetAxisRaw ("Vertical") : 0;

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

			if (dx != 0 || dy != 0) {
				if (dx != d.x || dy != d.y) {
					float angle = Mathf.Atan2 (dy, dx) * Mathf.Rad2Deg - 90;
                    Vector3 aux = Camera.main.transform.eulerAngles;
					transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
                    Camera.main.transform.eulerAngles = aux;
					d.x = dx;
					d.y = dy;
				} else {
					time = 0;
					origin = transform.position;
				}
			} else if (Input.GetButtonDown ("Fire") && !blocked) {


				GameObject gameObj = Instantiate(wave);
				gameObj.transform.position = transform.position;
           
                blocked = true;

                Invoke("Unblock", cooldown);

			}
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
        time = Time.time - time;
        GameObject WG = Instantiate(wingui, Stage.instance.transform.position, Quaternion.identity);
        WG.SetActive(true);
        dead = true;

    }

}
