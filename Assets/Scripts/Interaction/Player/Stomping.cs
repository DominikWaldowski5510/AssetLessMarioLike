using UnityEngine;

//handles player stomping the enemy behaviour
public class Stomping : MonoBehaviour
{
    private PlayerMovement controller;                   //character controller reference to check if object is grounded
    [SerializeField] private Transform floorCast = null;            //transform from which we cast a raycast to determine enemy underneath
    private float verticalInput = 0f;                               //determines input for up and down used for traveling via pipes
    //assigns the reference for character controller
    private void Start() => controller = this.GetComponent<PlayerMovement>();

    private float rayDistance = 0.6f;               //how far the ray extends
    [SerializeField] private LayerMask enemyMask = 0;          //the layer mask of the triggable blocks above player

    //creates raycast which then destroys a block if conditions are met
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(floorCast.transform.position, Vector2.down, rayDistance, enemyMask);
        if(hit.collider == null)
            return;

        verticalInput = Input.GetAxisRaw("Vertical");
        if (verticalInput > 0.5f || verticalInput < -0.5f)
        {
            Debug.Log("vertical hit");
            if (hit.collider.transform.tag == "Warp")
            {
                Debug.Log("Warp selected");
                hit.transform.GetComponent<Warp>().WarpPlayer();
            }
        }
        if (hit.collider.transform.tag == "Enemy")
        {
            hit.transform.GetComponent<EnemyCollision>().SmashEnemy();
            controller.Controller.Grounded = true;
            controller.Controller.Move(controller.HorizontalInput * Time.fixedDeltaTime, true);
        }
    }
}
