using UnityEngine;
using TMPro;

public class Flying_char : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject boss;

    void Start()
    {
        boss = GameObject.Find("BOSSM");
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "protection" || collider.gameObject.name == "Data(Clone)") return;

        if (collider.gameObject.name == "sprite_0")
        {
            boss.GetComponent<BOSSM>().PlayerHealth -= 1;
        }
        Destroy(gameObject);
    }
}