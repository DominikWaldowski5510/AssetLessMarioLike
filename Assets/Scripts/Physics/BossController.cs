using System.Collections;
using UnityEngine;

//handles movement and attack of the final boss in the game
public class BossController : MonoBehaviour
{
    [SerializeField] private AudioSource fireBallSound = null;          //the sound attached to firing the fire ball
    [SerializeField] private Transform fireBallSpawnLocation = null;        //location from which fire ball spawns
    private float fireRate = 3;                         //how often we fire a fireball
    private float jumpForce = 8.7f;                     //how strong the jump will be
     private bool isGrounded = true;                    //checks if object is colliding with the ground
    [SerializeField] private Pooling fireBallPool = null;               //allows us to use spawned fire balls
    private Rigidbody2D rb;                                      //reference to rigidbody attached to this object
    private float groundLength = 2f;                      //raycast of the ground
    [SerializeField] private LayerMask groundMask = 0;                  //layermask for what is considered ground

    //initialises jumping and shooting
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine(ShootFireBalls());
    }

    //handles constant jump of the enemy boss
    private void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundLength, groundMask);
        if(isGrounded == true)
        {
            Jump();
        }
    }

    //Adds force to our rigid body in order to jump upward
    private void Jump() => rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    //Constantly spawns fireballs at given locations based on fire rate
    private IEnumerator ShootFireBalls()
    {
        yield return new WaitForSeconds(1.5f);
        while(true)
        {
            GameObject fireBall = fireBallPool.GetPooledObject();
            fireBall.transform.position = fireBallSpawnLocation.position;
            fireBall.transform.transform.rotation = fireBallSpawnLocation.rotation;
            fireBall.SetActive(true);
            fireBallSound.Play();

            yield return new WaitForSeconds(fireRate);
        }
        
    }

}
