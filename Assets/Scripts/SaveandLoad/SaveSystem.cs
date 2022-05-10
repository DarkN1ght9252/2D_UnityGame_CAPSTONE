using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayerData (Player character){

    BinaryFormatter formatter = new BinaryFormatter();
    string path = Application.persistentDataPath + "/player.data";
    FileStream stream = new FileStream(path,FileMode.Create);

    PlayerData data = new PlayerData(character);

    formatter.Serialize(stream,data);
    stream.Close();
    }

    public static PlayerData loadPlayer () {
        string path = Application.persistentDataPath + "/player.data";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }else{            
            Debug.LogError("Save file not found " + path);
            return null;
        }
        
    }
}
