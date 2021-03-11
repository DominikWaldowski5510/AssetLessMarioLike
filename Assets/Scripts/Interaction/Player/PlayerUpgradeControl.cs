using UnityEngine;

//handles player upgrade system
public class PlayerUpgradeControl : MonoBehaviour
{
    private PlayerBlockTriggering triggerBlockSize;
    [Header("Assignables from Inspector")]
    [SerializeField] private GameObject[] playerVersions = null;           //0 is default small, 1 is big, 2 is big with fire
    [SerializeField] private Material[] characterColours = null;            //stores alternative materials 0 = defualt, 1 = transparent
    [SerializeField] private Renderer rend = null;                      //reference to renderer for changing materials
    [SerializeField] private CapsuleCollider2D colliderScaler = null;
    [Header("Preset Collision Values")]
    private Vector2 defaultScale = new Vector2(0.9978454f, 0.9978454f);
    private Vector2 defualtOffset = new Vector2(-0.0005537271f, -0.00845325f);
    private Vector2 bigScale = new Vector2(0.9978454f, 1.589392f);
    private Vector2 bigOffset = new Vector2(-0.0005537271f, 0.2873202f);
    //sets default player levels based on upgrade index
    private void Start()
    {
        triggerBlockSize = this.gameObject.GetComponent<PlayerBlockTriggering>();
        ResetModels();
        playerVersions[GameManager.instance.PlayerTierIndex].SetActive(true);
        EndDowngrade();
        ResetCollider();
    }

    //resets collider to small version
    private void ResetCollider()
    {
        colliderScaler.size = defaultScale;
        colliderScaler.offset = defualtOffset;
        triggerBlockSize.SetRaySize(true);
    }

    //sets collider to its bigger version
    public void UpgradeCollider()
    {
        colliderScaler.size = bigScale;
        colliderScaler.offset = bigOffset;
        triggerBlockSize.SetRaySize(false);
    }

    //upgrades player collider based on the upgrade number
    public void UpgradePlayerVisual()
    {
        ResetModels();
        playerVersions[GameManager.instance.PlayerTierIndex].SetActive(true);
    }

    //resets player to its small size 
    public void ResetPlayer()
    {
        ResetModels();
        ResetCollider();
        playerVersions[GameManager.instance.PlayerTierIndex].SetActive(true);
        EndDowngrade();
    }

    //resets all models to inactive state
    private void ResetModels()
    {
        for (int i = 0; i < playerVersions.Length; i++)
        {
            playerVersions[i].SetActive(false);
        }
    }

    //dispalys the see through material when player takes damage
    public void DownGradePlayer()
    {
        ResetModels();
        playerVersions[GameManager.instance.PlayerTierIndex].SetActive(true);
        rend.material = characterColours[1];
    }

    //displays default material of the player
    public void EndDowngrade()
    {
        rend.material = characterColours[0];
    }
}
