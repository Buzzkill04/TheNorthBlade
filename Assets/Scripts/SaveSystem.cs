using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


//Tutorial followed https://www.youtube.com/watch?v=XOjd_qU2Ido
public static class SaveSystem
{
    public static void SavePlayerData(PlayerLife currentPlayer)
    {
        //Create a new instance of the binary formatter
        BinaryFormatter bFormatter = new BinaryFormatter();
        //create the file name, as it is a binary file the file extension does not matter
        string path = Application.persistentDataPath + "/playerSave.zzTop";
        //The player data to save
        PlayerData playerData = new PlayerData(currentPlayer);
        //This using statement will open the the filestream then run the code block and finally close the stream
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            //Serialize the player data
            bFormatter.Serialize(stream, playerData);
        }

    }

    public static PlayerData LoadPlayer()
    {
        //Create the path
        string path = Application.persistentDataPath + "/playerSave.zzTop";
        if (File.Exists(path))
        {
            //Create a new instance of the binary formatter
            BinaryFormatter bFormatter = new BinaryFormatter();
            //This using statement will open the the filestream then run the code block and finally close the stream
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                PlayerData playerData = bFormatter.Deserialize(stream) as PlayerData;
                return playerData;
            }
        }
        else
        {
            //If there is no file, throw a file not found exception
            throw new FileNotFoundException();
        }
    }

}
