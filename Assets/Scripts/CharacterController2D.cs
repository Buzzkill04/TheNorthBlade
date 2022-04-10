using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Class based off brackeys 2d character controller https://github.com/Brackeys/2D-Character-Controller
public class CharacterController2D : MonoBehaviour
{

    [SerializeField] private float jumpForce = 400f;	// Amount of force added when the player jumps.

    [SerializeField] private bool airControl = true;	// Whether or not a player can steer while jumping;

    [SerializeField] private LayerMask whatIsGround;	// A mask determining what is ground to the character   

    [SerializeField] private Transform groundCheck;     // A position marking where to check if the player is grounded.

    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f; //Movement smoothing

    const float groundedRadius = 0.2f;                  // Radius of the overlap circle to determine if grounded

    private bool grounded;                              // Whether or not the player is grounded.

    private bool facingRight = true;                    // For determining which way the player is currently facing.

    private Vector3 velocity = Vector3.zero;

    private Rigidbody2D characterRigidBody2D;           // stores the rigidbody of the character sprite

    public UnityEvent OnLandEvent;                      // Invokes methods when the player lands

    
                                                        

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        characterRigidBody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
    }

    // Fixed update is called every fixed framerate based off the physics engine
    private void FixedUpdate()
    {
        bool wasGrounded = grounded;
        grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        foreach (var item in colliders)
        {
            if (item.gameObject != gameObject)
            {
                grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    public void Move(float move, bool jump)
    {
        if (grounded || airControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, characterRigidBody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            characterRigidBody2D.velocity = Vector3.SmoothDamp(characterRigidBody2D.velocity, targetVelocity, ref velocity, movementSmoothing);

            // If the input is moving the player right and the player is facing left
            if (move > 0 && !facingRight)
            {
                // flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && facingRight)
            {
                // flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (grounded && jump)
        {
            // Add a vertical force to the player.
            grounded = false;
            characterRigidBody2D.AddForce(new Vector2(0f, jumpForce));
        }
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
