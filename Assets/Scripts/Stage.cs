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

        fillBorderWithBombs(rawSpace);
        createPlayer(rawSpace);
        fillSpace(3, rawSpace);
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
                    Transform player = Instantiate(submarine, new Vector3(j, -i, 0), Quaternion.identity).transform;
                    Transform camera = GameObject.Find("Main Camera").transform;
                    camera.SetParent(player);
                    camera.position = new Vector3(player.position.x, player.position.y, -5);
                }
                else if (field == 3)
                {
                    createEndPoint(j, -i);
                }
            }
        }
    }

    public void createPlayer(int[,] rawSpace)
    {
        int x = height-1;
        int y = Random.Range(1, width-1);
        rawSpace[x, y] = 2;
    }

    public void createBomb(float x, float y) {
        Instantiate(bomb, new Vector3(x, y, 0), Quaternion.identity);
    }

    public void createEndPoint(float x, float y) {
        Instantiate(bomb, new Vector3(x, y, 0), Quaternion.identity);
    }

    public void fillBorderWithBombs(int[,] rawSpace) {
        // Top and Bottom:
        for (int i = 0; i < width; ++i) {
            rawSpace[0, i] = 3;
            rawSpace[height-1, i] = 1;
        }
        
        // Left and Right
        for (int i = 1; i < height-1; ++i) {
            rawSpace[i, 0] = 1;
            rawSpace[i, width - 1] = 1;
        }
    }

    public bool createWarningBomb(int _i, int _j, int[,] rawSpace) {
        for(int i = -1; i <= 1; i++) 
            for(int j = -1; j < 1; j++)
                return placeWarning(i + _i, j + _j, rawSpace);

        return false;
    }

    public bool placeWarning(int i, int j, int[,] rawSpace) {
        if (!p(i, j))
            return false;

        if (rawSpace[i, j] != 1)
            rawSpace[i, j] = 5;

        return true;
    }

    public bool p(int i, int j) {
        if (i < 0 || j < 0 || i >= width || j >= height)
        {
            return false;
        }

        return true;
    }

    public void fillSpace(int number, int[,] rawSpace) {
        int x, y;
        int counter = 0;

        for(int i = 0; i < 100; ++i) {
            x = Random.Range(1, width - 2);
            y = Random.Range(1, height - 2);

            if (createWarningBomb(x, y, rawSpace)) {
                counter++;
            };

            if(counter == number-1)
            {
                return;
            }
        }
    }

}
