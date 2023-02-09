using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Logic2048 : MonoBehaviour
{
    [SerializeField] private Vector2 topLeftCorner;
    [SerializeField] private Vector2 offset;
    [SerializeField] private GameObject tile;

    private PlayManager playManager;
    private bool setup = false;

    private int[,] board;
    private Tile[,] tiles;

    private float fadeinTimer;
    private float fadeinLength = 2f;

    // Start is called before the first frame update
    void Start()
    {
        playManager = GameObject.Find("PlayManager").GetComponent<PlayManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playManager.playing())
        {
            if (Time.time < fadeinTimer)
            {
                setTilesAlpha(1f - (fadeinTimer - Time.time)/fadeinLength);
            }
            else
            {
                setTilesAlpha(1f);
            }


            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(down())
                {
                    addRandom();
                }
                
                displayBoard();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (up())
                {
                    addRandom();
                }

                displayBoard();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(left())
                {
                    addRandom();
                }
                
                displayBoard();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(right())
                {
                    addRandom();
                }
                
                displayBoard();
            }
        }

    }

    public void setupBoard()
    {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        tiles = new Tile[4, 4];
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                GameObject newChild = Instantiate(tile, new Vector3(topLeftCorner.x + offset.x * x, topLeftCorner.y + offset.y * y), Quaternion.identity);
                newChild.transform.SetParent(this.transform, false);
                tiles[x, y] = newChild.GetComponent<Tile>();
            }
        }

        fadeinTimer = Time.time + fadeinLength;
        setup = true;
    }

    public void reset()
    {
        if (!setup) { setupBoard(); }

        board = new int[4, 4];
        addRandom();
        addRandom();
        displayBoard();
    }

    private void displayBoard()
    {
        
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                tiles[x, y].updateValue(board[x,y]);
            }
        }
    }

    public int getHighestTile()
    {
        int highestTile = 0;

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (board[x, y] > highestTile)
                {
                    highestTile = board[x, y];
                }
            }
        }

        return highestTile;

    }

    private void addRandom()
    {
        if (boardFull())
        {
            playManager.endGame();
            return;
        }
        else
        {
            while (true)
            {
                int add = Random.Range(1, 3) * 2;
                int x = Random.Range(0, 4);
                int y = Random.Range(0, 4);

                if (board[x, y] == 0)
                {
                    board[x, y] = add;
                    return;
                }
            }
        }
    }

    private bool boardFull()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (board[x, y] == 0)
                {
                    return false;
                }
            }
        }

        return true;

    }

    private bool up()
    {
        rotateCW();
        rotateCW();
        bool temp = down();
        rotateCW();
        rotateCW();
        return temp;

    }

    private bool down()
    {
        bool ret = false;
        //int[,] oldBoard = copy(board);

        for (int x = 0; x < 4; x++)
        {
            for (int y = 3; y >= 0; y--)
            {
                if (board[x, y] == 0)
                {
                    //Empty spot
                    for (int seek = y-1; seek >= 0; seek--)
                    {
                        if (board[x, seek] != 0)
                        {
                            board[x, y] = board[x, seek];
                            board[x, seek] = 0;
                            //Moved something down, let's re-check
                            ret = true;
                            y++;
                            break;

                        }
                    }
                } 
                else
                {
                    //Check for combo
                    for (int seek = y - 1; seek >= 0; seek--)
                    {
                        if (board[x, seek] == board[x, y])
                        {
                            board[x, y] *= 2;
                            board[x, seek] = 0;
                            //Combined something
                            ret = true;
                            break;

                        } else if (board[x, seek] != 0)
                        {
                            break;
                        }
                    }

                }
            }
        }

        //return !checkEquality(oldBoard, board);
        return ret;
    }

    private bool left()
    {
        rotateCW();
        bool temp = down();
        rotateCW();
        rotateCW();
        rotateCW();
        return temp;
    }

    private bool right()
    {
        rotateCW();
        rotateCW();
        rotateCW();
        bool temp = down();
        rotateCW();
        return temp;
    }

    private void rotateCW()
    {
        int[,] newBoard = new int[4, 4];

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                newBoard[y, 3 - x] = board[x, y];
            }
        }

        board = newBoard;

    }

    private bool checkEquality(int[,] arr1, int[,] arr2)
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (arr1[x, y] != arr2[2,y])
                {
                    return false;
                }
            }
        }
        
        return true;
    }

    private int[,] copy(int[,] boardToCopy)
    {
        int[,] ret = new int[4, 4];

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                ret[x, y] = boardToCopy[x, y];
            }
        }

        return ret;
    }

    private void setTilesAlpha(float alph)
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                tiles[x, y].setAlpha(alph);
            }
        }

    }
}
