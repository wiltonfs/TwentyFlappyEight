using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    private bool playingState = false;
    private bool menuState = true;

    private int pipeCount;
    private int maxPoints = 0;
    private int highScore = 0;

    [SerializeField] private Text finalScore;
    [SerializeField] private Text highScoreMessage;
    [SerializeField] private CanvasGroup gameOverDisplay;
    [SerializeField] private CanvasGroup welcomeDisplay;

    private Spawner pipeSpawner;
    private Bird bird;
    private Logic2048 logic2048;

    private float welcomeFadeoutTimer;
    private float welcomeFadeoutLength = 1f;


    // Start is called before the first frame update
    void Start()
    {
        bird = GameObject.Find("Bird").GetComponent<Bird>();
        logic2048 = GameObject.Find("Logic2048").GetComponent<Logic2048>();
        pipeSpawner = GameObject.Find("PipeSpawner").GetComponent<Spawner>();
        startMenu();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        highScoreMessage.fontSize = 50 + (int)(10f*Mathf.Sin(2 * Mathf.PI * Time.time));
        if(menuState)
        {
            if(Input.GetKeyDown("space"))
            {
                startGame();
            }

        } else 
        {
            if (Time.time < welcomeFadeoutTimer)
            {
                welcomeDisplay.alpha = (welcomeFadeoutTimer - Time.time)/welcomeFadeoutLength;
            } 
            else
            {
                welcomeDisplay.alpha = 0f;
            }


            if (Input.GetKeyDown("r") && !playingState)
            {
                resetGame();
            }
        }

    }

    public void startMenu()
    {
        //Hide text
        gameOverDisplay.alpha = 0f;
        welcomeDisplay.alpha = 1f;

        bird.reset();
    }

    public void startGame()
    {
        welcomeFadeoutTimer = Time.time + welcomeFadeoutLength;
        menuState = false;
        bird.flap();

        //fade in the 2048 board
        logic2048.reset();

        playingState = true;
    }

    public void resetGame()
    {
        pipeCount = 0;

        //Hide text
        gameOverDisplay.alpha = 0;

        //resets
        logic2048.reset();
        pipeSpawner.reset();
        bird.reset();

        playingState = true;

    }

    public void endGame()
    {
        if (playingState)
        {
            maxPoints = logic2048.getHighestTile();
            playingState = false;

            //Display text
            gameOverDisplay.alpha = 1;
            finalScore.text = "Highest Tile: " + maxPoints.ToString();
            //finalScore.text = "Highest Tile: " + maxPoints.ToString() + "\nFlew between " + pipeCount.ToString() + " pipes";

            if (maxPoints > highScore)
            {
                //New high score!
                highScoreMessage.color = new Color(0.3f, 0.85f, 0.4f, 1f);
                highScore = maxPoints;
            } else
            {
                highScoreMessage.color = new Color(0.3f, 0.85f, 0.4f, 0f);
            }
        }
    }

    public void score()
    {
        if (playingState)
        {
            pipeCount++;
        }
    }

    public bool playing()
    {
        return playingState;
    }

    public bool menu()
    {
        return menuState;
    }
}
