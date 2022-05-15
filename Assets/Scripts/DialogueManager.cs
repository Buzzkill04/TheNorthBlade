using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //The sentences in the current dialogue, a queue is essentially an array that adds items to the end (Enqueue) in a 'queue' 
    //When we Dequeue, the first item in the queue (the first one added) is returned
    private Queue<string> sentences;
    //The dialogue box UI menu
    public GameObject dialogueBox;
    //The two text box's in the dialogue box menu
    public Text titleText;
    public Text sentenceText;
    //The game object that starts the move to the next scene at the end of the dialogue
    public GameObject NextSceneStarter;
    public GameObject player;

    private void Start()
    {
        //Create a new string queue
        sentences = new Queue<string>();
        //Get the player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void StartDialogue(Dialogue npcDialogue)
    {
        //Stop the player from being able to move
        player.GetComponent<PlayerMovement>().movementSpeed = 0f;
        //Open the dialogue box
        dialogueBox.SetActive(true);
        //set the title text to the character speaking
        titleText.text = npcDialogue.characterSpeaking;
        //Empty the sentences queue in case there is strings currently in it
        sentences.Clear();
        //for each sentence in the current npc dialogue 
        foreach (string sentence in npcDialogue.dialogueSentences)
        {
            //enqueue the sentence into the queue
            sentences.Enqueue(sentence);
        }
        //display the first sentence
        DisplayNextSentance();

    }
    //This method is called when the dialogue is first started as well as when the dialogueBox is clicked
    public void DisplayNextSentance()
    {
        //If there is no more sentences in the queue
        if (sentences.Count == 0)
        {
            //call the OnDialogueFinished method
            OnDialogueFinished();
            return;
        }
        //Get the next sentence
        string currentSentence = sentences.Dequeue();
        //set the sentence text to the next sentence
        sentenceText.text = currentSentence;
    }

    public void OnDialogueFinished()
    {
        //close the dialogueBox
        dialogueBox.SetActive(false);
        //Set the position of the next scene starter to the players position
        NextSceneStarter.transform.position = player.transform.position;
    }
}
