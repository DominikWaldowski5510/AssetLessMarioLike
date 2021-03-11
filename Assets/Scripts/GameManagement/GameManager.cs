using UnityEngine;

//handles game data, and triggers between game states
public class GameManager : MonoBehaviour
{
    [Header("Classes")]
    private LevelManager manageLevel;                       //Reference to level manager
    private UiManager manageUi;                             //reference to ui manager 
    public static GameManager instance;                     //instance so other classes can access it easier
    [Header("Player Data")]
    private int points = 0;                                 //the amount of points that the player has
    private int health = 3;                                 //the total health of a player
    private const int maxHealth = 3;                        //maximum health a player can have
     private int playerTierIndex = 0;                       //the current powerup level of the player
     private float gameTime;                                //the timer score 
    private float levelBonus;                               //bonus level accumulated by visiting many levels
    private bool hasLost = false;                           //checks whenever or not the game has been lost, used for loading game over scene

    #region Accessors
    public int PlayerTierIndex { get => playerTierIndex; set => playerTierIndex = value; }
    public int Health { get => health;  }

    public int Points { get => points;  }

    public float GameTime { get => gameTime;  }
    public float LevelBonus { get => levelBonus; set => levelBonus = value; }
    #endregion

    //sets up a instance that is not destroyed when new level is loaded
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        if (manageUi == null)
        {
            manageUi = GameObject.Find("LevelManager").GetComponent<UiManager>();
        }
    }

    //Initialized stats, this only gets called once meaning dont destroy on load skips it next times
    private void Start()
    {
        gameTime = 360;
        hasLost = false;
        levelBonus = 0;
    }

    //Deals with losing a health point
    public void LoseAHealth(bool canAvoid)
    {
        if (manageLevel == null)
        {
            manageLevel = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        }
        if (manageLevel.IsInvulnerable == true && canAvoid)
            return;
        if (!canAvoid || playerTierIndex == 0)
        {
            SubstractHealth();
        }
        else
        {
            playerTierIndex = 0;
            manageLevel.InvulnerabilityState();
        }
    }

    //subtracts health from the players total pool and resets the game state accordingly
    private void SubstractHealth()
    {
        playerTierIndex = 0;
        PlayerSounds.instance.PlaySound(PlayerSounds.instance.nameOfSound = PlayerSounds.SoundNames.Death);
        health--;
        if (manageLevel == null)
        {
            manageLevel = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        }
        manageLevel.ResetPlayerSize();
        manageLevel.ResetPlayerPosition();
        if (health <= 0)
        {
            health = 0;
            hasLost = true;
            manageUi.LoseGame();
        }

        UiManager.instance.UpdatePlayerHealth(health, maxHealth);
    }

    //Triggers the game over state at the end of the game when player wins
    public void TriggerGameEnd()
    {
        hasLost = true;
        AddPoints(1000);
        manageUi.WinGame();
    }

    //checks for player input and counts time 
    private void Update()
    {
        if (hasLost == false)
        {
            if (manageUi == null)
            {
                manageUi = GameObject.Find("LevelManager").GetComponent<UiManager>();
            }
            gameTime -= Time.deltaTime;
            if(gameTime <=0)
            {
                gameTime = 0;
            }
            manageUi.UpdateTimesRemaining(gameTime);
        }
    }

    //adds points to the point section
    public void AddPoints(int pointAmount)
    {
        points += pointAmount;
        UiManager.instance.UpdatePointsGained(points);
    }

    //Adds powerup on pickup increasing player size 
    public void AddPowerup()
    {
        playerTierIndex++;
        if (playerTierIndex >= 2)
        {
            playerTierIndex = 2;
        }
        if (manageLevel == null)
        {
            manageLevel = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        }
        manageLevel.UpgradePlayer();
    }
}
