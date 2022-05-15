using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryNPC : MonoBehaviour
{
    //The player in the scene
    public GameObject player;
    //The dialogue manager
    public DialogueManager dialogueManager;
    //An instance of the npcDialogue object
    public Dialogue npcDialogue;

    // Start is called before the first frame update
    void Start()
    {
        //Get the player and the dialogue manager
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    // Update is called with the physics system
    void FixedUpdate()
    {
        //If the distance between the player and the npc is less than 3 and the player has not already talked to the npc
        if (Vector2.Distance(player.transform.position, gameObject.transform.position) <= 1 && !npcDialogue.dialogueFinished)
        {
            //start the dialogue 
            dialogueManager.StartDialogue(npcDialogue);
            npcDialogue.dialogueFinished = true;
        }
    }
}
