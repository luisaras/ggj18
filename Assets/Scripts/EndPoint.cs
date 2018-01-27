using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public GameObject victory;
    public GameObject stage;

    private void Start()
    {
        this.stage = GameObject.Find("Stage");
    }

    void Update()
    {
        if ((transform.position - Player.instance.transform.position).magnitude < 0.5)
        {
            GameObject vic = Instantiate(victory);
            vic.transform.position = Player.instance.transform.position;
            Stage.instance.Win();
            Destroy(gameObject);
        }
    }
}
