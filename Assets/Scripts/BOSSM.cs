using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEditor;


public class BOSSM : MonoBehaviour
{
    public TMP_Text clock;
    public GameObject exit;
    public static RaycastHit2D bhit;
    private Vector2 worldPoint;
    private GameObject dragobj;
    private bool draggingobj = false;
    private bool dragging = false;
    public BoxCollider2D top;
    private Vector2 movement;
    public GameObject sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        for (float pos = -10.44f; pos < -7f; pos += 0.1f)
        {
            sprite.transform.position = new Vector2(pos, sprite.transform.position.y);
            yield return new WaitForSeconds(0.015f);
        }
        yield return new WaitForSeconds(1f);
        top.isTrigger = true;
        yield return new WaitForSeconds(1f);
        top.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bhit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (bhit.collider != null && bhit.collider.name == exit.name)
            {
                Application.Quit();
#if UNITY_EDITOR
                    EditorApplication.isPlaying = false;
#endif
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (bhit.collider == null || bhit.collider.name.Contains("_CROSS") || bhit.collider.name.Contains("_0"))
            {
                dragobj = null;
                draggingobj = false;
                dragging = false;
            }
            else
            {
                dragobj = bhit.collider.gameObject;
                draggingobj = true;
                dragging = false;
            }
        }
        if (Input.GetMouseButton(0) && dragobj != null && draggingobj)
        {
            Vector2 currentm = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float distance = Vector2.Distance(currentm, worldPoint);

            if (!dragging && distance > 0.1f)
            {
                dragging = true;
            }

            if (dragging)
            {
                dragobj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * 80);
            }
        }
        clock.text = System.DateTime.Now.ToString().Split("2025")[1].Split(":")[0] + ":" + System.DateTime.Now.ToString().Split("2025")[1].Split(":")[1];
    }
    void FixedUpdate()
    {
        sprite.GetComponent<Rigidbody2D>().MovePosition(sprite.GetComponent<Rigidbody2D>().position + movement * 5f * Time.fixedDeltaTime);
    }
}
