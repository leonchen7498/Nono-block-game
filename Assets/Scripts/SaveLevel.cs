using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts {
    public class SaveLevel : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var data = DragController.highestLevel;
            string fileName = Application.persistentDataPath + "/playerInfo.dat";
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(fileName);
            bf.Serialize(file, data);
            file.Close();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
