using UnityEngine;

public class ExplosionSmall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        FindObjectOfType<Shake>().StartCoroutine(FindObjectOfType<Shake>().ShakeC(0.1f, 0.2f));
        if (collider.gameObject.name == "sprite_0")
        {
            GameObject.Find("BOSSM").GetComponent<BOSSM>().PlayerHealth -= 10;
        }
    }
}