using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerKill : MonoBehaviour
    {
        private bool dead;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "player" && !dead)
            {
                other.gameObject.GetComponent<PlayerMovement>().body.bodyType = RigidbodyType2D.Static;
                other.gameObject.GetComponent<PlayerMovement>().animator.SetTrigger("dead");
                dead = true;
                StartCoroutine(killPlayer());
            }
        }

        IEnumerator killPlayer()
        {
            yield return new WaitForSeconds(1);
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }

        private void OnDestroy()
        {
            if (dead)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            }
        }
    }
}