using UnityEngine;

public class Explosion : MonoBehaviour
{

    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "sprite_0")
        {
            GameObject.Find("BOSSM").GetComponent<BOSSM>().PlayerHealth -= 20;
        }
    }
}