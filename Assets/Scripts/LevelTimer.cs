using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public int seconds;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Tick", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan t = TimeSpan.FromSeconds(seconds);
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = t.ToString(@"mm\:ss");
    }

    void Tick()
    {
        seconds -= 1;

        if ( seconds == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }
}
