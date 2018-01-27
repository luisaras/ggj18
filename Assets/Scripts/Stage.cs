using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

	public static Stage instance;

	public int height;
	public int width;
    public bool finite;

    public List<Vector2> endPos;
    public GameObject bomb;
    public GameObject endPoint;
    public GameObject submarine;

    /**
     * 1 - SURVIVAL
     * 2 - INFRUN
     **/
    public int gameMode;

	// Use this for initialization
	void Awake () {
		instance = this;
	}

    private void Start() {
        endPos = new List<Vector2>();
        prepareField();
    }

    public void prepareField() {
        // Initializing public variables:
        int[,] rawSpace = new int[width, height];

        createPlayer(2, 2, rawSpace);
        fillBorderWithBombs(rawSpace);
        buildField(rawSpace);
    }

    public void buildField(int[,] rawSpace) {
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                int field = rawSpace[i, j];
                if (field == 1)
                {
                    createBomb(j, -i);
                }
                else if (field == 2)
                {
                    Instantiate(submarine, new Vector3(j, -i, 0), Quaternion.identity);
                }
                else if (field == 3)
                {
                    createEndPoint(j, -i);
                }
            }
        }
    }

    public void createPlayer(int i, int j, int[,] rawSpace)
    {
        rawSpace[i, j] = 2;
    }

    public void createBomb(float x, float y) {
        GameObject aux = Instantiate(bomb, new Vector3(x, y, 0), Quaternion.identity);
    }

    public void createEndPoint(float x, float y) {
        GameObject aux = Instantiate(bomb, new Vector3(x, y, 0), Quaternion.identity);
    }

    public void fillBorderWithBombs(int[,] rawSpace) {
        // Top and Bottom:
        for (int i = 0; i < width; ++i) {
            rawSpace[0, i] = 1;
            rawSpace[height-1, i] = 1;
        }
        
        // Left and Right
        for (int i = 1; i < height-1; ++i) {
            rawSpace[i, 0] = 1;
            rawSpace[i, width - 1] = 1;
        }
    }

    public void Win() {
        Debug.Log("VOCÊ GANHOU!");
    }
}
