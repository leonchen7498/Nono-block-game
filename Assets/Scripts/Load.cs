using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts
{
    public class Load : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
                LevelController.highestLevel = (string)bf.Deserialize(file);
                file.Close();
                if (Convert.ToInt32(LevelController.highestLevel) < 1)
                {
                    LevelController.highestLevel = "1";
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
