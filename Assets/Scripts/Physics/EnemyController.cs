using UnityEngine;

//handles movement of non player objects
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    private float speed = 1;               //speed at which the enemy moves
    private Rigidbody2D rb;                                 //reference to physics body
    private const float rayDistance = 0.6f;                       //left/right direction ray cast to change directions
    [SerializeField] private float groundLength = 0.6f;                      //raycast of the ground
    [SerializeField] private LayerMask groundMask = 0;                  //layermask for what is considered ground
    [SerializeField] private bool isRight = false;                      //check for current position
    private Vector2 direction;                                          //used for moving left or right by the enemy
    [SerializeField] private bool isGrounded = true;                    //checks if object is colliding with the ground
    [SerializeField] private Vector3 colliderOffset = new Vector3();        //used for more accurate ground collision

    public float Speed { get => speed; set => speed = value; }

    //defaults all values
    private void Start()
    {
        isGrounded = true;
        Flip();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    //drawing raycasts to determine ground and left right movement
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayDistance, groundMask);
        isGrounded = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundMask) ||
           Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundMask);
        if (hit.collider != null)
        {
            Flip();
        }
    }

    //stop movement if its not on ground, move left or right otherwise based on flip bool direction
    private void FixedUpdate()
    {
        if (!isGrounded)
            return;
        rb.velocity = new Vector2(0, 0);
        if(isRight)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
    }

    //flips the object by 180 degrees, essentially switching its direction from left to right and vice versa
    private void Flip()
    {
        isRight = !isRight;
        if(isRight)
        {
            direction = new Vector2(1, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            direction = new Vector2(-1, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    //Draws lines of for collision offsetting so its easier to set up collideroffset as it has to be done manually
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }
}
