using System.Collections;
using UnityEngine;

//manages individual level, used for switching levels and displaying/loading level data
public class LevelManager : MonoBehaviour
{
    private CameraMovement cameraMov;           //reference to camera movement to reset player in case of defeat
    [SerializeField] private string levelname = "";             //name of the level that is assigned to UI text component
    [SerializeField] private Transform playerStartPosition = null;          //the position where player spawns if he loses
    [SerializeField] private GameObject playerObject = null;                //reference to play, so he can be reset upon death
    [SerializeField] private DeathAnim deadPlayerModel = null;              //stores reference to death animation object
    private bool isInvulnerable = false;                                    //checks whenever the player is currently invulnerable
    public const float levelMultiplayer = 0.5f;
    public bool IsInvulnerable { get => isInvulnerable; set => isInvulnerable = value; }

    //grabbing references on initialisation alongside loading correct UI names and player position
    private void Start()
    {
        deadPlayerModel.gameObject.SetActive(false);
        GameManager.instance.LevelBonus += levelMultiplayer;
        isInvulnerable = false;
        cameraMov = Camera.main.GetComponent<CameraMovement>();
        UiManager.instance.UpdateGameLevel(levelname);
        DefaultPlayerPosition();
    }

    //starts corutine that will reset playe rand do death animation
    public void ResetPlayerPosition()
    {
        StartCoroutine(ResetPlayer());
    }

    //resets player size
    public void ResetPlayerSize()
    {
        PlayerUpgradeControl upgrader = playerObject.GetComponent<PlayerUpgradeControl>();
        upgrader.ResetPlayer();
    }

    //resets players position while doing the death animation that trigers when player loses health
    private IEnumerator ResetPlayer()
    {
        deadPlayerModel.transform.position = playerObject.transform.position;
        float timer = 0;
        deadPlayerModel.gameObject.SetActive(true);
        deadPlayerModel.SetDeathAnim(playerObject.transform.position);
        playerObject.SetActive(false);
        while (timer < 1)
        {
            timer += Time.deltaTime;
            deadPlayerModel.DeathAnimation();
            yield return null;
        }
        deadPlayerModel.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.9f);
        if (GameManager.instance.Health > 0)
        {
            DefaultPlayerPosition();
        }
        yield return null;
    }

    //upgrades player by growing the collider and setting new model as active
    public void UpgradePlayer()
    {
        PlayerUpgradeControl upgradeController = playerObject.GetComponent<PlayerUpgradeControl>();
        upgradeController.UpgradeCollider();
        upgradeController.UpgradePlayerVisual();
    }

    //sets the default start position to the player
    private void DefaultPlayerPosition()
    {
        playerObject.transform.position = playerStartPosition.transform.position;
        playerObject.SetActive(true);
        if (cameraMov != null)
        {
            cameraMov.ResetPoisition();
        }
    }

    //starts corutine that triggers invulnerability within the player
    public void InvulnerabilityState()
    {
        if (isInvulnerable == false)
        {
            StartCoroutine(InvulnerableDuration());
        }
    }

    //triggers invulnerability for the player during this time he cant interact with enemies or powerups
    private IEnumerator InvulnerableDuration()
    {
        isInvulnerable = true;
        PlayerUpgradeControl upgrader = playerObject.GetComponent<PlayerUpgradeControl>();
        playerObject.layer = 14;
        upgrader.DownGradePlayer();
        yield return new WaitForSeconds(3);
        upgrader.EndDowngrade();
        playerObject.layer = 13;
        isInvulnerable = false;
        yield return null;
    }
        
}
