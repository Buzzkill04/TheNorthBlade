using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //The character controller
    public CharacterController2D controller;
    //Horizontal move will become +ve (1) when moving right and -ve (-1) when moving left, will be 0 when still.
    float horizontalMove = 0f;
    //Speed the character will move at
    public float movementSpeed = 10f;
    //If the player is jumping
    bool jump = false;
    //Character animator
    public Animator animator;
    //Rigidbody
    private Rigidbody2D charRB;

    private void Start()
    {
        animator = GetComponent<Animator>();
        charRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Get the player input on the horizontal axis and multiply by the movement speed
        horizontalMove = Input.GetAxisRaw("Horizontal") * movementSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        //If the player jumps, set jump to true
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            if (animator.GetBool("Grounded"))
            {
                //Play the jump sound
                FindObjectOfType<AudioManager>().playSound("jump");
            }
        }
        //if the character is falling or jumping set the animator parameters jump to true and grounded to false
        if (charRB.velocity.y != 0)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Grounded", false);
        }        
    }

    // Fixed update is called every fixed framerate based off the physics engine
    private void FixedUpdate()
    {
        //Move the character by horizontal move, multiply by Time.fixedDeltaTime, to make movement framerate independant
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        //Set jump to false so that the character doesnt continue, to jump.
        jump = false;
    }
    //Used to set the movement speed from an animation event
    public void SetSpeed(float speed)
    {
        movementSpeed = speed;
    }

    //This will be triggered when the character lands through the OnLandEvent unity event
    public void OnPlayerLand()
    {
        //Animation handling
        animator.SetBool("Jump", false);
        animator.SetBool("Grounded", true);

    }


}
