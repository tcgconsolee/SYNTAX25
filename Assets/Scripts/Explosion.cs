using UnityEngine;

public class Explosion : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        FindObjectOfType<Shake>().StartCoroutine(FindObjectOfType<Shake>().ShakeC(0.2f, 0.3f));
        if (collider.gameObject.name == "sprite_0")
        {
            GameObject.Find("BOSSM").GetComponent<BOSSM>().PlayerHealth -= 20;
        }
    }
}