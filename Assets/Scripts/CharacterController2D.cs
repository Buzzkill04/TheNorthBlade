using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Class based off brackeys 2d character controller https://github.com/Brackeys/2D-Character-Controller
public class CharacterController2D : MonoBehaviour
{
    //Amount of force added when the player jumps.
    [SerializeField] private float jumpForce = 400f;
    //Whether or not a player can steer while jumping;
    [SerializeField] private bool airControl = true;
    //A mask determining what is ground to the character  
    [SerializeField] private LayerMask whatIsGround;
    //A position marking where to check if the player is grounded.
    [SerializeField] private Transform groundCheck;
    //Movement smoothing
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
    //Radius of the overlap circle to determine if grounded
    const float groundedRadius = 0.2f;
    //Whether or not the player is grounded.
    private bool grounded;
    //For determining which way the player is currently facing.
    private bool facingRight = true;                    
    //Create a zero (0, 0, 0) vector 3
    private Vector3 velocity = Vector3.zero;
    //Stores the rigidbody of the character sprite
    private Rigidbody2D characterRigidBody2D;
    //Invoked when the player lands
    public UnityEvent OnLandEvent;

    //Awake is called when the script instance is being loaded
    private void Awake()
    {
        //Get the rigid body component of the sprite the script is attached to.
        characterRigidBody2D = GetComponent<Rigidbody2D>();
    }
    //Called at a fixed frame rate, linked to the physics system
    private void FixedUpdate()
    {
        //make a copy of the grounded status and set grounded to false
        bool wasGrounded = grounded;
        grounded = false;
        //The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        //foreach collision
        foreach (var item in colliders)
        {
            //if the gameObject is not equal to the game object the script is applied to
            if (item.gameObject != gameObject)
            {
                //set grounded to true
                grounded = true;
                //Invoke the OnLandEvent 
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }
    public void Move(float move, bool jump)
    {
        if (grounded || airControl)
        {
            //Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, characterRigidBody2D.velocity.y);
            //And then smoothing it out and applying it to the character
            characterRigidBody2D.velocity = Vector3.SmoothDamp(characterRigidBody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

            //If the input is moving the player right and the player is facing left
            if (move > 0 && !facingRight)
            {
                // flip the player.
                Flip();
            }
            //Otherwise if the input is moving the player left and the player is facing right
            else if (move < 0 && facingRight)
            {
                //flip the player.
                Flip();
            }
        }
        //If the player should jump
        if (grounded && jump)
        {
            //Add a vertical force to the player
            grounded = false;
            characterRigidBody2D.AddForce(new Vector2(0f, jumpForce));
        }
    }
    private void Flip()
    {
        //Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        //Rotate the character about the y axis, 180 degrees
        transform.Rotate(0f, 180f, 0f);
    }


    //Debug
    /*
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.DrawWireSphere(groundCheck.position, groundedRadius);
    }
    */
}
