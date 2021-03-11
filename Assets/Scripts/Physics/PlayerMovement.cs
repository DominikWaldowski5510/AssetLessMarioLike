using UnityEngine;

//handles player movement
[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovement : MonoBehaviour
{

    private CharacterController2D controller;               //reference to character controller script which does all physics

    private float horizontalInput = 0f;                     //takes in horizontal input from player
    private bool jump = false;                              //determines whenever player jumps or not
    private float moveSpeed = 40f;                          //the speed at which the player moves
    private bool canMove = true;                            //locks or unlocks player movement
    private float jumpDelay = 0.25f;                        //how much earlier can we press jump before we land

    public CharacterController2D Controller { get => controller; }
    public float HorizontalInput { get => horizontalInput; }

    //resets the values of player movement to default
    private void OnEnable()
    {
        canMove = true;
        horizontalInput = 0;
        jump = false;
    }

    //grabs the component of charactercontroller
    private void Awake() => controller = this.gameObject.GetComponent<CharacterController2D>();

    //takes in input for jumping and movement
    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if(Input.GetButtonDown("Jump"))
        {
            controller.JumpTimer = Time.time + jumpDelay;
        }

    }
    
    //handles physics for moving and jumping 
    private void FixedUpdate()
    {
        if (canMove == true)
        {
            if (controller.JumpTimer > Time.time && controller.Grounded)
            {
                jump = true;
                PlayerSounds.instance.PlaySound(PlayerSounds.instance.nameOfSound = PlayerSounds.SoundNames.Jump);
            }
            controller.Move(horizontalInput * Time.fixedDeltaTime, jump);
            jump = false;
        }
        else
        {
            controller.Move((1 * moveSpeed) * Time.fixedDeltaTime,  false);
        }
    }

    //stops the player from having any control of the player movement
    public void StopMovement()
    {
        canMove = false;
    }
}
