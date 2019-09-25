using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSelf : MonoBehaviour
{

    public float everyXSeconds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    void Enable()
    {
        gameObject.SetActive(true);
    }

    void OnEnable()
    {
        Invoke("Disable", everyXSeconds);
    }

    void OnDisable()
    {
        Invoke("Enable", everyXSeconds);
    }
}
