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
            boss.GetComponent<BOSSM>().PlayerHealth -= 2;
        }
        if (collider.gameObject.name.Contains("corrupted"))
        {
            if (Random.Range(0f, 1f) > 0.95f) {
                Destroy(collider.gameObject);
            }
        }
        if (Random.Range(0f, 1f) > 0.3f)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector2 collisionDirection = (transform.position - collider.transform.position).normalized;
            rb.linearVelocity = Vector2.Reflect(rb.linearVelocity, collisionDirection);
        }
    }
}