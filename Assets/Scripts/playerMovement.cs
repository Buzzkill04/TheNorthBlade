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
    public float movementSpeed = 30f;
    //If the player is jumping
    bool jump = false;

    // Update is called once per frame
    void Update()
    {
        //Get the player input on the horizontal axis and multiply by the movement speed
        horizontalMove = Input.GetAxisRaw("Horizontal") * movementSpeed;
        //If the player jumps, set jump to true
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
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
}
