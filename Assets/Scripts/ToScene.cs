using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToScene : MonoBehaviour
{
    public string scene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp()
    {
        if (scene == "LevelSelect" || scene == "MainMenu")
        {
            if (MainMenuMusic.instance != null && !MainMenuMusic.instance.GetComponent<AudioSource>().isPlaying)
            {
                MainMenuMusic.instance.GetComponent<AudioSource>().Play();
            }
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}
