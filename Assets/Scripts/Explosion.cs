using UnityEngine;

public class Explosion : MonoBehaviour
{
    public ParticleSystem ps;
    void Start()
    {
        ps.Play();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "sprite_0")
        {
            GameObject.Find("BOSSM").GetComponent<BOSSM>().PlayerHealth -= 20;
            Instantiate(ps, transform.position, Quaternion.identity).Play();
            FindObjectOfType<Shake>().StartCoroutine(FindObjectOfType<Shake>().ShakeC(0.2f, 0.3f));
        }
    }
}