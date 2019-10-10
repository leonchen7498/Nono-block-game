using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    public enum direction {Up, Left, Down, Right}
    public direction facing;
    public float waitTime;
    public GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, 0, 90 * (int)facing);
        InvokeRepeating("Pew", waitTime, waitTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Pew()
    {
        GameObject newProjectile = null;
        switch((int) facing) {
            case 0:
                newProjectile = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 120, -90), Quaternion.identity);
                break;
            case 1:
                newProjectile = Instantiate(projectile, new Vector3(transform.position.x - 120, transform.position.y, -90), Quaternion.identity);
                break;
            case 2:
                newProjectile = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y - 120, -90), Quaternion.identity);
                break;
            case 3:
                newProjectile = Instantiate(projectile, new Vector3(transform.position.x + 120, transform.position.y, -90), Quaternion.identity);
                break;
        }
        
        newProjectile.transform.Rotate(0, 0, 90 * (int) facing);
        newProjectile.GetComponent<Projectile>().facing = (Projectile.direction)(int)facing;
    }
}
