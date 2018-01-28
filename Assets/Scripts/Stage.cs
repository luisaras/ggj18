using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

    public static Stage instance;

    public int height = 15;
    public int width = 15;

    public List<Vector2> endPos;
    public GameObject bomb;
    public GameObject endPoint;
    public GameObject submarine;
    public GameObject battery;

    /** represents a space **/
    public class Space {
        public GameObject obj;
        /**
         * 0 - Nothing.
         * 1 - Bomb.
         * 2 - Initial Position.
         * 3 - Final Position.
         * 5 - WarningBomb.
         * */
        public int value = 0;
    }

    // Holds objects of infinite run.
    // After each movement towards a direction, new objects are created on the border towards same direction. Distant objects will be deleted.
    // A map with 10x10 is suficient to make this gameMode works nicely.
    public Space[,] Board;

    /**
     * 1 - SURVIVAL
     * 2 - INFRUN
     **/
    public static int gameMode = 2;

	// Use this for initialization
	void Awake () {
		instance = this;
	}

    private void Start() {
        endPos = new List<Vector2>();
        prepareField();
    }

    public void prepareField() {
        if(gameMode == 1) {
            survivalPrepare();
        } else if (gameMode == 2) {
            infiniteRunPrepare();
        }
    }

    public void survivalPrepare() {
        int[,] rawSpace = new int[width, height];

        fillBorderWithBombs(rawSpace);
        createPlayer(rawSpace);
        fillSurvivalSpace(10, rawSpace);
        buildField(rawSpace);
    }

    public void infiniteRunPrepare() {
        initializeInfiniteRun();
        int numBombs = (width * height) / 5;
        createRandomMinesAround(1);
        int numBatteries = (width * height) / 10;
        createRandomBatteriesAround(numBatteries);
        Debug.Log(printBoard());
    }

    public void initializeInfiniteRun() {
        // initializeBoard:
        Board = new Space[width, height];
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                Board[i, j] = new Space();
            }
        }

        // Create player.
        int x, y;
        x = width / 2;
        y = height / 2;
        submarine = Instantiate(submarine, new Vector3(y, width - x, 0), Quaternion.identity);
		Camera.main.transform.position = new Vector3(x, y, -5);
    }

    public void createRandomMinesAround(int number) {
        int x, y;
        int counter = 0;

        for (int i = 0; i < 100; ++i) {
            x = Random.Range(0, width);
            y = Random.Range(0, height);

            if (createInfiniteWarningBomb(x, y)) {
                counter++;
            };

            if (counter == number) {
                return;
            }
        }
    }

    public void createRandomBatteriesAround(int number) {
        int x, y;
        int counter = 0;

        for (int i = 0; i < 100; ++i) {
            x = Random.Range(0, width);
            y = Random.Range(0, height);

            if (createBattery(x, y)) {
                counter++;
            };

            if (counter == number) {
                return;
            }
        }
    }

    public void setBoard(int i, int j, int value) {
        Board[i, j].value = value;
    }

    public void setBoard(int i, int j, GameObject obj) {
        Board[i, j].obj = obj;
    }

    public void deleteBoard(int i, int j) {
        Board[i, j].value = 0;
        Destroy(Board[i, j].obj);
        Board[i, j].obj = null;
    }

    public void buildField(int[,] rawSpace) {
        for (int i = 0; i < width; ++i) {
            for (int j = 0; j < height; ++j) {
                int field = rawSpace[i, j];
                if (field == 1) {
                    createBomb(j, -i);
                }
                else if (field == 2) {
                    Instantiate(submarine, new Vector3(j, -i, 0), Quaternion.identity);
					Camera.main.transform.position = new Vector3(j, -i, -5);
                }
                else if (field == 3) {
                    createEndPoint(j, -i);
                }
            }
        }
    }

    public void createPlayer(int[,] rawSpace) {
        int x = height-1;
        int y = Random.Range(1, width-1);
        rawSpace[x, y] = 2;
    }

    public GameObject createBomb(float x, float y) {
        return Instantiate(bomb, new Vector3(y, width-x, 0), Quaternion.identity);
    }

    public void createEndPoint(float x, float y) {
        Instantiate(endPoint, new Vector3(x, y, 0), Quaternion.identity);
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

    public bool createBattery(int i, int j) {
        if (Board[i, j].value != 0 && Board[i, j].value != 5 && Board[i, j].value != 7) {
            return false;
        }

        Board[i, j].value = 7;
        Instantiate(battery, new Vector3(j, width - i, 0), Quaternion.identity);

        return true;
    }

    public bool createInfiniteWarningBomb(int i, int j) {
        if(Board[i, j].value != 0) {
            return false;
        }

        Board[i, j].value = 1;
        Board[i, j].obj = createBomb(i, j);

        for (int a = -1; a <= 1; a++) 
            for(int b = -1; b <= 1; b++)
                placeInfiniteWarning(i + a, j + b);

        return true;
    }

    public bool createSurvivalWarningBomb(int _i, int _j, int[,] rawSpace) {
        if (p(_i, _j) && rawSpace[_i, _j] != 0) {
            return false;
        }

        rawSpace[_i, _j] = 1;

        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                placeSurvivalWarning(i + _i, j + _j, rawSpace);

        return true;
    }

    public bool placeSurvivalWarning(int i, int j, int[,] rawSpace) {
        if (!p(i, j))
            return false;

        if (rawSpace[i, j] == 0)
            rawSpace[i, j] = 5;

        return true;
    }

    public bool placeInfiniteWarning(int i, int j) {
        if (p(i, j) && Board[i, j].value == 0) {
            Board[i, j].value = 5;
        }

        return true;
    }

    public bool p(int i, int j) {
        if (i < 0 || j < 0 || i >= width || j >= height) {
            return false;
        }

        return true;
    }

    public void fillSurvivalSpace(int number, int[,] rawSpace) {
        int x, y;
        int counter = 0;

        for(int i = 0; i < 100; ++i) {
            x = Random.Range(1, width - 2);
            y = Random.Range(1, height - 2);

            if (createSurvivalWarningBomb(x, y, rawSpace)) {
                counter++;
            };

            if (counter == number) {
                return;
            }
        }
    }

    public void shiftAll(float angle) {

        angle = angle + 90;

        Space[,] auxBoard = new Space[width, height];
        for(int i = 0; i < width; ++i) {
            for(int j = 0; j < height; ++j) {
                auxBoard[i, j] = new Space();
                auxBoard[i, j].value = 0;
            }
        }

        for (int i = 0; i < width; ++i) {
            for (int j = 0; j < height; ++j) {
                // up
                if (close(angle, 90)) {
                    if (i == height-1) {
                        deleteBoard(i, j);
                    } else {
                        auxBoard[i + 1, j] = Board[i, j];
                    }
                }
                // down
                else if (close(angle, 270)) {
                    if (i == 0) {
                        deleteBoard(i, j);
                    } else {
                        auxBoard[i - 1, j] = Board[i, j];
                    }
                }
                // left
                else if (close(angle, 180)) {
                    if (j == width - 1) {
                        deleteBoard(i, j);
                    } else {
                        auxBoard[i, j + 1] = Board[i, j];
                    }
                }
                // right
                else if (close(angle, 360)) {
                    if (j == 0) {
                        deleteBoard(i, j);
                    } else {
                        auxBoard[i, j - 1] = Board[i, j];
                    }
                }
            }
        }

        Board = auxBoard;
    }

    public void createBorderBombs(float angle) {

        angle = angle + 90;
        int i = 0;
        int j = 0;

        // up
        if (close(angle, 90)) {
            i = 0;
            for (j = Random.Range(1, width); j < width;) {
                Board[i, j].value = 1;
                Board[i, j].obj = Instantiate(bomb, submarine.transform.position + new Vector3(j - height / 2, width - i - width / 2, 0), Quaternion.identity);
                j = j + Random.Range(1, width);
                Debug.Log(submarine.transform.position);
            }
        }
        // down
        else if (close(angle, 270)) {
            i = height - 1;
            for (j = Random.Range(1, width); j < width; ) {
                Board[i, j].value = 1;
                Board[i, j].obj = Instantiate(bomb, submarine.transform.position + new Vector3(j - height / 2, width - i - width / 2, 0), Quaternion.identity);
                j = j + Random.Range(1, width);
                Debug.Log(submarine.transform.position);
            }
        }
        // left
        else if (close(angle, 180)) {
            j = 0;
            for (i = Random.Range(1, height); i < width; ) {
                Board[i, j].value = 1;
                Board[i, j].obj = Instantiate(bomb, submarine.transform.position + new Vector3(j - height / 2, width - i - width / 2, 0), Quaternion.identity);
                i = i + Random.Range(1, height);
            }
        }
        // right
        else if (close(angle, 360)) {
            j = width - 1;
            for (i = Random.Range(1, height); i < width; ) {
                Board[i, j].value = 1;
                Board[i, j].obj = Instantiate(bomb, submarine.transform.position + new Vector3(j - height / 2, width - i - width / 2, 0), Quaternion.identity);
                i = i + Random.Range(1, height);
            }
        }
    }

    public bool close(float a, float b) {

        if (Mathf.Max(a, b) - Mathf.Min(a, b) < 0.5) {
            return true;
        } else {
            return false;
        }
    }

    public string printBoard() {
        string result = "";

        for (int i = 0; i < width; ++i) {
            for (int j = 0; j < height; ++j) {
                result += Board[i, j].value;
                if(Board[i,j].obj != null) {
                    result += ".";
                }
                else {
                    result += " ";
                }
            }
            result += '\n';
        }

        return result;
    }
}
