using UnityEngine;
using TMPro;

public class Dropping_file : MonoBehaviour
{
    private Rigidbody2D rb;
    public AudioSource hitaudio;
    public GameObject boss;

    void Start()
    {
        boss = GameObject.Find("BOSSM");
        hitaudio = GameObject.Find("HIT").GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "protection" || collider.gameObject.name == "Data(Clone)" || collider.gameObject.name.Contains("corrupted") || collider.gameObject.name == "top_0") { return; }
        else
        if (collider.gameObject.name == "sprite_0")
        {
            hitaudio.Play();
            boss.GetComponent<BOSSM>().PlayerHealth -= 5;
            FindObjectOfType<Shake>().StartCoroutine(FindObjectOfType<Shake>().ShakeC(0.1f, 0.2f));
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}