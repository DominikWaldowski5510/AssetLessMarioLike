using UnityEngine;

//inherits from block destroying to use moving of the block up and down corutine
public class BlockInteraction : DestroyBlock
{
    [SerializeField] private Material[] blockMaterials = null;              //stores alternative material for this block
    private Renderer rend;
    [SerializeField] private GameObject[] powerups = null;                  //stores alternative powerups for the character player
    private string unavailableTag = "UnbreakableBlock";                     //swaps tag that it changes to so it can be broken again
    [SerializeField] private Transform collectableSpawnLocation = null;         //the default spawn location of the powerup

    //resets material and disables powerups from triggering before they get activated
    private void Start()
    {
        rend = this.gameObject.GetComponent<Renderer>();
        rend.material = blockMaterials[0];
        for (int i = 0; i < powerups.Length; i++)
        {
            powerups[i].SetActive(false);
        }
    }

    //function overwriten from destroy block performs its base functions as well as triggers interaction function
    public override void TriggerBlock(bool isDestructable)
    {
        base.TriggerBlock(isDestructable);
        TriggerInteraction();
    }

    //trigers interaction with the interactable block
    private void TriggerInteraction()
    {
        rend.material = blockMaterials[0];
        //setup the powerup
        if(GameManager.instance.PlayerTierIndex == 0)
        {
            powerups[0].transform.position = collectableSpawnLocation.transform.position;
            powerups[0].gameObject.SetActive(true);
        }
        else
        {
            powerups[1].transform.position = collectableSpawnLocation.transform.position;
            powerups[1].gameObject.SetActive(true);
        }
        //Change block to unusable
        rend.material = blockMaterials[1];
        this.gameObject.tag = unavailableTag;
    }
}
