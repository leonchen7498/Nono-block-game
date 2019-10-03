using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts
{
    public class NextLevel : MonoBehaviour
    {
        public int Levels;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnMouseUp()
        {
            var scene = Convert.ToInt32(LevelController.currentLevel) + 1;
            if (Convert.ToInt32(LevelController.highestLevel) < scene)
            {
                LevelController.highestLevel = scene.ToString();
            }
            var data = LevelController.highestLevel;
            string fileName = Application.persistentDataPath + "/playerInfo.dat";
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(fileName);
            bf.Serialize(file, data);
            file.Close();
            if (scene > Levels)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString());
            }
        }
    }
}
