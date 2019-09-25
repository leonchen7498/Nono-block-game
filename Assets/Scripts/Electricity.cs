using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    public int Width;
    public int everyXSeconds;
    public string newTag;
    public GameObject electricityPrefab;
    public GameObject[] allElectricity;
    public GameObject teslaEnd;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Zap());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Zap()
    {
        Instantiate(teslaEnd, new Vector3(transform.position.x + 120 * (Width + 1), transform.position.y, transform.position.z), Quaternion.identity);
        for (int i = 1; i <= Width; i++)
        {
            Debug.Log(everyXSeconds + i * 0.1f);
            Debug.Log(transform.position.x + 120 * i);
            GameObject electricity = Instantiate(electricityPrefab, new Vector3(transform.position.x + 120 * i, transform.position.y, transform.position.z), Quaternion.identity);
            electricity.tag = newTag;
            electricity.GetComponent<DisableSelf>().everyXSeconds = everyXSeconds;

            yield return new WaitForSeconds(0.05f);
        }
    }
}
