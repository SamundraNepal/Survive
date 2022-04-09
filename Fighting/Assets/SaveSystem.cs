
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
   
    public static void SaveData(BossMotor Bm)
    {

        BinaryFormatter Formatter = new BinaryFormatter();
        string Path = Application.persistentDataPath + "/Survive.File";
        FileStream Stream = new FileStream(Path, FileMode.Create);
     
        PlayerData data = new PlayerData(Bm);
        Formatter.Serialize(Stream, data);

        Stream.Close();

    }

    public static PlayerData LoadData()
    {


        string Path = Application.persistentDataPath + "/Survive.File";
        if(File.Exists(Path))
        {

            BinaryFormatter Formatter = new BinaryFormatter();
            FileStream Stream = new FileStream(Path, FileMode.Open);
           PlayerData data = Formatter.Deserialize(Stream) as PlayerData;

            Stream.Close();
            return data;
        }
        else
        {


            Debug.Log("Save file Missing");
            return null;
        }

    }

}
