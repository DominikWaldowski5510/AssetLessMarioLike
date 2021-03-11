using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//handles all the UI changes
public class UiManager : MonoBehaviour
{
    public static UiManager instance;                           //reference to iself so its easier to grab by other components
    [Header("Text components for player UI display")]
    [SerializeField] private Text playerHealth = null;                  //displays players current health   
    [SerializeField] private Text levelDisplay = null;                  //displays currently loaded level
    [SerializeField] private Text pointDisplay = null;                  //displays current point count
    [SerializeField] private Text timeDisplay = null;                  //displays current point count
    [Header("Game over Display")]
    [SerializeField] private Text gameOverDisplay = null;               //displays the current game time
    private bool hasLost = false;                                       //stores lost game state

    //sets instance when script first loads
    private void Awake() => instance = this;


    //inactivates defeat states
    private void Start()
    {
        hasLost = false;
        gameOverDisplay.gameObject.SetActive(false);
        UpdatePlayerHealth(GameManager.instance.Health, 3);
        UpdatePointsGained(GameManager.instance.Points);
    }

    //update The UI Text component that displays current player health
    public void UpdatePlayerHealth(int currentHealth, int maxHealth) => playerHealth.text = "Health: " + currentHealth + "/" + maxHealth;


    //update The UI Text component that displays current game Level
    public void UpdateGameLevel(string currentLevel) => levelDisplay.text = "Level: " + currentLevel;


    //update The UI Text component that displays current player points
    public void UpdatePointsGained(int points) => pointDisplay.text = "Points: " + points;

    //update The UI Text component that displays time based score
    public void UpdateTimesRemaining(float time) => timeDisplay.text = "Time: " + time.ToString("0");

    //runs when player loses all of their health points
    public void LoseGame()
    {
        hasLost = true;         //maybe add timer so we dont click too soon and can properly read the lose screen
        gameOverDisplay.gameObject.SetActive(true);
    }

    //trigger for winning the game and displaying game won instead of game over
    public void WinGame()
    {
        hasLost = true;         //maybe add timer so we dont click too soon and can properly read the lose screen
        gameOverDisplay.gameObject.SetActive(true);
        gameOverDisplay.text = "Game Won!";
    }

    //allows us to press any button after losing to end the game
    private void Update()
    {
        if(hasLost == true)
        {
            if(Input.anyKeyDown)
            {
                SetEndGame();
            }
        }
    }

    //sets up triggers and switches to end game scene
    private void SetEndGame()
    {
        PlayerPrefs.SetInt("HasWon", 0);        //0 means we lost, 1 means we won
        SceneManager.LoadScene(1);      //change to loading so it only appears when it loads properly
    }
}
