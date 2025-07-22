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
    public GameObject virus;
    public CircleCollider2D protection;
    public GameObject charprefab;
    public int PlayerHealth = 100;
    public bool end = false;
    public GameObject phealth;
    public GameObject popprefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        protection.isTrigger=true;
        for (float pos = -10.44f; pos < -7f; pos += 0.1f)
        {
            sprite.transform.position = new Vector2(pos, sprite.transform.position.y);
            yield return new WaitForSeconds(0.015f);
        }
        StartCoroutine(Boss());
        yield return new WaitForSeconds(1f);
        top.isTrigger = true;
        yield return new WaitForSeconds(1f);
        top.isTrigger = false;
        protection.isTrigger=false;
    }

    IEnumerator Boss()
    {
        for (float pos = 3.97f; pos > 1f; pos -= 0.05f)
        {
            virus.transform.position = new Vector2(virus.transform.position.x, pos);
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(Data());
        StartCoroutine(Pop());
    }
    IEnumerator Data()
    {
        while (!end)
        {
            yield return new WaitForSeconds(5f);
            StartCoroutine(DataAtk());
            yield return new WaitForSeconds(5f);
        }
    }
    IEnumerator Pop()
    {
        while (!end)
        {
            StartCoroutine(PopAtk());
            yield return new WaitForSeconds(30f);
        }
    }
    IEnumerator DataAtk()
    {
        for (int i = 0; i < 100; i++)
        {
            string lalala = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+[]{}|;:'\",.<>?/\\`~";
            char x = lalala[Random.Range(0, lalala.Length)];

            float size = Random.Range(4f, 8f);

            GameObject obj = Instantiate(charprefab, virus.transform.position, Quaternion.identity);
            obj.GetComponent<TMP_Text>().text = x.ToString();
            obj.GetComponent<TMP_Text>().fontSize = size;

            obj.transform.localScale *= Random.Range(0.8f, 1.5f);
            obj.transform.Rotate(Vector3.forward * Random.Range(-180f, 180f));

            Vector2 dir = Random.insideUnitCircle.normalized;
            obj.GetComponent<Flying_char>().Launch(dir, Random.Range(3f, 7f));
            yield return new WaitForSeconds(0.005f);
        }
    }
    IEnumerator PopAtk()
    {
        GameObject obj = Instantiate(popprefab, virus.transform.position, Quaternion.identity);
        obj.transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        if (!(obj.transform.localScale.x > 0.9f))
        {
            Destroy(obj);
            StopCoroutine(PopAtk());
            yield break;
        }
        obj.transform.GetChild(1).gameObject.SetActive(true);
        obj.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.9f);
        Destroy(obj);
    }

    IEnumerator Popupclose()
    {
        var obj = GameObject.Find("Window_suspension(Clone)");
        for (float i = 2f; i > -0.1f; i -= 0.2f)
        {
            obj.transform.localScale = new Vector3(i / 2, i / 2, i / 2);
            yield return new WaitForSeconds(0.02f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float sx = 4.84066f * ((float)PlayerHealth / 100f);
        phealth.transform.localScale = new Vector3(sx, 0.5589254f, 1);
        Vector3 position = new Vector3(4.84066f / 2f -10.5562f, phealth.transform.position.y, phealth.transform.position.z);
        phealth.transform.position = position + new Vector3(sx / 2f, 0f, 0f);
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bhit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (bhit.collider != null && bhit.collider.name == "suspension_CROSS")
            {
                StartCoroutine(Popupclose());                
            }
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
            if (bhit.collider == null || bhit.collider.name.Contains("_CROSS") || bhit.collider.name.Contains("_0") || bhit.collider.name == virus.transform.GetChild(0).gameObject.name)
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
