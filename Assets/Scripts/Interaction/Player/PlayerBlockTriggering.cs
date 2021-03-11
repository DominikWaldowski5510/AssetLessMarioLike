using UnityEngine;

//handles players interaction with triggerign blocks above the player
public class PlayerBlockTriggering : MonoBehaviour
{
    private CharacterController2D controller;                   //character controller reference to check if object is grounded
    [SerializeField] private AudioSource source = null;
    //assigns the reference for character controller
    private void Start() => controller = this.GetComponent<CharacterController2D>();

    private bool canTrigger = true;                 //checks if block can be destroyed, used not to destroy more than 1 at once
    private float rayDistance = 0.6f;               //how far the ray extends
    private const float raySmallDist = 0.6f;        //the size of small ray when player is at size small
    private const float rayLargeDist = 1.3f;        //the size of the ray when player is in his large form
    [SerializeField] private LayerMask groundMask = 0;          //the layer mask of the triggable blocks above player

    public bool CanTrigger { get => canTrigger; set => canTrigger = value; }

    //creates raycast which then destroys a block if conditions are met
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, rayDistance, groundMask);
        if (hit.collider != null && !controller.Grounded)
        {
            if (canTrigger == true)
            {
                if (hit.collider.tag == "BreakableBlock")
                {
                    hit.transform.GetComponent<DestroyBlock>().TriggerBlock(true);
                }
                else if (hit.collider.tag == "InteractableBlock")
                {
                    hit.transform.GetComponent<DestroyBlock>().TriggerBlock(false);
                }
            }
            canTrigger = false;
            source.Play();
        }
    }

    //sets the ray size
    public void SetRaySize(bool isSmall)
    {
        if (isSmall)
        {
            rayDistance = raySmallDist;
        }
        else
        {
            rayDistance = rayLargeDist;
        }
    }
}

