using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    // Stop Panel
    private GameObject stopPanel;

    // Stop Text
    private TextMeshProUGUI stopText;

    // Game State
    public bool isGameStopped;

    // Initial Time Value
    // ENCAPSULATION
    protected float initTime {get; private set;}

    // Current Time Value
    private float currentTime;

    // Time Text
    private TextMeshProUGUI timeText;

    // Player Script
    private Player playerScript;

    // ___ VICTORY PANEL VARIABLES ___

    // Level completion state
    public bool isLvlFinished;

    // Victory Panel
    private GameObject victoryPanel;

    // Victory Panel Score Text
    private TextMeshProUGUI victoryScoreText;

    // Time Bonus Value
    private float timeBonus;

    // Time Bonus Text
    private TextMeshProUGUI timeBonusText;

    // Life Bonus Value
    private float lifeBonus;

    // Life Bonus Text
    private TextMeshProUGUI lifeBonusText;

    // Final Score Value
    private float finalScore;

    // Final Score Text
    private TextMeshProUGUI finalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        // Time moves
        Time.timeScale = 1;

        // Initialize stopPanel
        stopPanel = GameObject.Find("Stop Panel");
        stopText = GameObject.Find("Stop Text").GetComponent<TextMeshProUGUI>();

        stopPanel.SetActive(false);

        // Initialize isGameStopped
        isGameStopped = false;

        // Initialize initTime
        initTime = 400;

        // Initialize currentTime
        currentTime = initTime;

        // Initialize timeText
        timeText = GameObject.Find("Time Text").GetComponent<TextMeshProUGUI>();
        timeText.text = $"Time {currentTime:000}";

        // Initialize playerScript
        playerScript = FindObjectOfType<Player>();

        // ___ VICTORY PANEL INITIALIZATIONS ___

        // Initialize isLvlFinished
        isLvlFinished = false;

        // Initialize victoryPanel
        victoryPanel = GameObject.Find("Victory Panel");

        // Initialize victoryScoreText
        victoryScoreText = GameObject.Find("Victory Score Text").GetComponent<TextMeshProUGUI>();

        // Initialize timeBonus
        timeBonus = 0;

        // Initialize timeBonusText
        timeBonusText = GameObject.Find("Time Bonus Text").GetComponent<TextMeshProUGUI>();

        // Initialize lifeBonus
        lifeBonus = 0;

        // Initialize lifeBonusText
        lifeBonusText = GameObject.Find("Life Bonus Text").GetComponent<TextMeshProUGUI>();

        // Initialize finalScore
        finalScore = 0;

        // Initialize finalScoreText
        finalScoreText = GameObject.Find("Final Score Text").GetComponent<TextMeshProUGUI>();

        // Hide Victory Panel
        victoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // ABSTRACTION
        Stop();
        TimeDown();
        BackToTitle();
    }

    // Stop Method
    private void Stop()
    {
        // If game is not paused and there's still time and Escape is pressed, switch isGameStopped and show stop panel
        if(!isLvlFinished && !isGameStopped && currentTime > 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            // Show stop panel
            stopPanel.SetActive(true);

            // Change stopText to "Pause"
            stopText.text = "Pause";

            // Switch isGameStopped
            isGameStopped = true;

            // Time stops
            Time.timeScale = 0;
        }

        // Else if game is paused and there's still time and Escape is pressed, unstop the game and hide stop panel
        else if(!isLvlFinished && isGameStopped && currentTime > 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            // Hide stop panel
            stopPanel.SetActive(false);

            // Switch isGameStopped
            isGameStopped = false;

            // Time resumes
            Time.timeScale = 1;
        }

        // If the game is not stopped and time is out, show stop panel and change stopText to "Game Over" and stop game
        else if(!isLvlFinished && !isGameStopped && currentTime <= 0)
        {
            // Show stop panel
            stopPanel.SetActive(true);

            // Change stopText to "Game Over"
            stopText.text = "Game Over";

            // Switch isGameStopped
            isGameStopped = true;

            // Time stops
            Time.timeScale = 0;
        }

        // Else if the game is not stopped and player has less than 1 life, show stop panel, change stopText to "Game Over" and stop the game
        else if(!isLvlFinished && !isGameStopped && playerScript.lifeNum < 1)
        {
            // Show stop panel
            stopPanel.SetActive(true);

            // Change stopText to "Game Over"
            stopText.text = "Game Over";

            // Switch isGameStopped
            isGameStopped = true;

            // Time stops
            Time.timeScale = 0;
        }
    }

    // Time Countdown Method
    private void TimeDown()
    {
        // If the game is not stopped and there's still time, decrease time
        if(!isLvlFinished && !isGameStopped && currentTime > 0)
        {
            // Decrease time
            currentTime -= Time.deltaTime;

            // Update timeText
            timeText.text = $"Time {currentTime:000}";
        }
    }

    // Title Button Method
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Title Scene");
    }

    // Retry Button Method
    public void Retry()
    {
        SceneManager.LoadScene("Level Scene");
    }

    // Level Complete Method
    public void Victory()
    {
        // If the game is not finished, show victory panel, calculate bonuses and final score, and stop the game
        if(!isLvlFinished)
        {
            // Show Victory Panel
            victoryPanel.SetActive(true);

            // Show score
            victoryScoreText.text = $"Score {playerScript.score:0000}";

            // Calculate Time Bonus
            timeBonus = currentTime * 2;

            // Show Time Bonus
            timeBonusText.text = $"Time Bonus {timeBonus:000}";

            // Calculate Life Bonus
            lifeBonus = playerScript.lifeNum * 200;

            // Show Life Bonus
            lifeBonusText.text = $"Life Bonus {lifeBonus:000}";
            
            // Calculate Final Score
            finalScore = playerScript.score + timeBonus + lifeBonus;

            // Show Final Score
            finalScoreText.text = $"Final Score {finalScore:0000}";

            // Finish the game
            isLvlFinished = true;
        }   
    }

    // Back to Title Screen Method
    private void BackToTitle()
    {
        if(isLvlFinished && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Title Scene");
        }
    }
}
