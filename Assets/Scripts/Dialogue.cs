using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is similar to the sound class, in which is serves as a template for a 
//dialogue in the game, which contains all he sentences and the characters saying the dialogue

//System.serializable allows the non unity class to show in the editor
[System.Serializable]
public class Dialogue 
{
    //The character saying the sentence
    public string characterSpeaking;
    //The sentances the character is saying
    public string[] dialogueSentences;
    //True if the dialogue has already been done
    public bool dialogueFinished;
}
