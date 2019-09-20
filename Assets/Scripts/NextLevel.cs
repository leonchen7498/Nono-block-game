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
            var scene = Convert.ToInt32(DragController.currentLevel) + 1;
            DragController.highestLevel = scene.ToString();
            var data = DragController.highestLevel;
            string fileName = Application.persistentDataPath + "/playerInfo.dat";
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(fileName);
            bf.Serialize(file, data);
            file.Close();
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString()) ;
        }
    }
}
