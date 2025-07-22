using UnityEngine;
using System.Collections;
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
    public GameObject popprefab2;
    public GameObject fileprefab;
    public GameObject good;
    public GameObject bad;
    private float idleTime = 0f;
    private float idleAmplitude = 0.1f;
    private float idleFrequency = 1f;
    private Vector2 originalSpritePos;
    private Rigidbody2D spriteRb;
    public int timeLasted;

    IEnumerator Start()
    {
        protection.isTrigger = true;
        spriteRb = sprite.GetComponent<Rigidbody2D>();
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
        protection.isTrigger = false;
        originalSpritePos = spriteRb.position;
        for (float i = 0f; i < 1f; i += 0.1f)
        {
            GameObject.Find("exitgame_0").transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator TimeStart()
    {
        timeLasted = 0;
        while (!end)
        {
            timeLasted += 1;
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator Boss()
    {
        for (float pos = 3.97f; pos > 1f; pos -= 0.05f)
        {
            virus.transform.position = new Vector2(virus.transform.position.x, pos);
            yield return new WaitForSeconds(0.065f);
        }
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
        StartCoroutine(PopAtk());
        while (!end)
        {
            yield return new WaitForSeconds(10f);
            if (timeLasted > 50)
            {
                StartCoroutine(PopAtkSmall2());
            }
            else
            {
                StartCoroutine(PopAtkSmall());
            }
            yield return new WaitForSeconds(20f);
        }
    }

    IEnumerator Files()
    {
        while (!end)
        {
            FileAtk();
            yield return new WaitForSeconds(20f);
        }
    }

    IEnumerator DataAtk()
    {
        for (int i = 0; i < 100+timeLasted/2; i++)
        {
            string lalala = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+[]{}|;:'\",.<>?/\\`~";
            char x = lalala[Random.Range(0, lalala.Length)];

            float size = Random.Range(4f, 8f);

            GameObject obj = Instantiate(charprefab, virus.transform.position, Quaternion.identity);
            var tmp = obj.GetComponent<TMP_Text>();
            tmp.text = x.ToString();
            tmp.fontSize = size;
            tmp.outlineWidth = 0.5f;
            Color hexColor;
            if (ColorUtility.TryParseHtmlString("#C04060", out hexColor))
            {
                tmp.outlineColor = hexColor;
            }

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

    IEnumerator PopAtkSmall()
    {
        GameObject obj1 = Instantiate(popprefab2, new Vector3(-5f, 0f, 0f), Quaternion.identity);
        GameObject obj2 = Instantiate(popprefab2, new Vector3(0f, 0f, 0f), Quaternion.identity);
        GameObject obj3 = Instantiate(popprefab2, new Vector3(5f, 0f, 0f), Quaternion.identity);
        obj1.transform.localScale = Vector3.zero;
        obj2.transform.localScale = Vector3.zero;
        obj3.transform.localScale = Vector3.zero;
        obj1.transform.GetChild(1).gameObject.SetActive(false);
        obj2.transform.GetChild(1).gameObject.SetActive(false);
        obj3.transform.GetChild(1).gameObject.SetActive(false);
        for (float i = 0.0f; i < 0.5083974f; i += 0.1f)
        {
            obj1.transform.localScale = new Vector3(i, i, i);
            obj2.transform.localScale = new Vector3(i, i, i);
            obj3.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2f);
        if (!(obj1.transform.localScale.x > 0f))
        {
            Destroy(obj1);
        }
        if (!(obj2.transform.localScale.x > 0f))
        {
            Destroy(obj2);
        }
        if (!(obj3.transform.localScale.x > 0f))
        {
            Destroy(obj3);
        }
        obj1.transform.GetChild(1).gameObject.SetActive(true);
        obj2.transform.GetChild(1).gameObject.SetActive(true);
        obj3.transform.GetChild(1).gameObject.SetActive(true);
        obj1.GetComponent<Renderer>().enabled = false;
        obj2.GetComponent<Renderer>().enabled = false;
        obj3.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.9f);
        Destroy(obj1);
        Destroy(obj2);
        Destroy(obj3);
    }

    IEnumerator PopAtkSmall2()
    {
        GameObject obj1 = Instantiate(popprefab2, new Vector3(-5f, 0f, 0f), Quaternion.identity);
        GameObject obj2 = Instantiate(popprefab2, new Vector3(0f, 0f, 0f), Quaternion.identity);
        GameObject obj3 = Instantiate(popprefab2, new Vector3(5f, 0f, 0f), Quaternion.identity);
        GameObject obj4 = Instantiate(popprefab2, new Vector3(-2.5f, 1f, 0f), Quaternion.identity);
        GameObject obj5 = Instantiate(popprefab2, new Vector3(2.5f, 1f, 0f), Quaternion.identity);
        obj1.transform.localScale = Vector3.zero;
        obj2.transform.localScale = Vector3.zero;
        obj3.transform.localScale = Vector3.zero;
        obj4.transform.localScale = Vector3.zero;
        obj5.transform.localScale = Vector3.zero;
        obj1.transform.GetChild(1).gameObject.SetActive(false);
        obj2.transform.GetChild(1).gameObject.SetActive(false);
        obj3.transform.GetChild(1).gameObject.SetActive(false);
        obj4.transform.GetChild(1).gameObject.SetActive(false);
        obj5.transform.GetChild(1).gameObject.SetActive(false);
        for (float i = 0.0f; i < 0.5083974f; i += 0.1f)
        {
            obj1.transform.localScale = new Vector3(i, i, i);
            obj2.transform.localScale = new Vector3(i, i, i);
            obj3.transform.localScale = new Vector3(i, i, i);
            obj4.transform.localScale = new Vector3(i, i, i);
            obj5.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2f);
        if (!(obj1.transform.localScale.x > 0f))
        {
            Destroy(obj1);
        }
        if (!(obj2.transform.localScale.x > 0f))
        {
            Destroy(obj2);
        }
        if (!(obj3.transform.localScale.x > 0f))
        {
            Destroy(obj3);
        }
        if (!(obj4.transform.localScale.x > 0f))
        {
            Destroy(obj4);
        }
        if (!(obj5.transform.localScale.x > 0f))
        {
            Destroy(obj5);
        }
        obj1.transform.GetChild(1).gameObject.SetActive(true);
        obj2.transform.GetChild(1).gameObject.SetActive(true);
        obj3.transform.GetChild(1).gameObject.SetActive(true);
        obj4.transform.GetChild(1).gameObject.SetActive(true);
        obj5.transform.GetChild(1).gameObject.SetActive(true);
        obj1.GetComponent<Renderer>().enabled = false;
        obj2.GetComponent<Renderer>().enabled = false;
        obj3.GetComponent<Renderer>().enabled = false;
        obj4.GetComponent<Renderer>().enabled = false;
        obj5.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.9f);
        Destroy(obj1);
        Destroy(obj2);
        Destroy(obj3);
        Destroy(obj4);    
        Destroy(obj5);    
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

    void FileAtk()
    {
        GameObject obj = Instantiate(fileprefab, new Vector3(3.85f, 3.61f, 0), Quaternion.identity);
    }

    IEnumerator SizeInc()
    {
        for (float i = 1f; i > 0f; i -= 0.1f)
        {
            GameObject.Find("exitgame_0").transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(GameObject.Find("exitgame_0"));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current)
                {
                    position = Input.mousePosition
                };

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);

                foreach (var result in results)
                {
                    if (result.gameObject.name == "Yes")
                    {
                        Application.Quit();
#if UNITY_EDITOR
                        EditorApplication.isPlaying = false;
#endif
                    }
                    else if (result.gameObject.name == "No")
                    {
                    StartCoroutine(SizeInc());
                        good.SetActive(false);
                        bad.SetActive(true);
                        StartCoroutine(TimeStart());
                        StartCoroutine(Data());
                        StartCoroutine(Pop());
                        StartCoroutine(Files());
                    }
                }
            }
        float sx = 4.84066f * ((float)PlayerHealth / 100f);
        phealth.transform.localScale = new Vector3(sx, 0.5589254f, 1);
        Vector3 position = new Vector3(4.84066f / 2f - 10.5562f, phealth.transform.position.y, phealth.transform.position.z);
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
            if (bhit.collider == null || bhit.collider.name.Contains("_CROSS") || bhit.collider.name.Contains("_0") || bhit.collider.name.Contains("explosion") || bhit.collider.name == virus.transform.GetChild(0).gameObject.name)
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
        Vector2 newPos = spriteRb.position;

        if (movement == Vector2.zero)
        {
            idleTime += Time.fixedDeltaTime;
            float offsetY = Mathf.Sin(idleTime * idleFrequency * 2 * Mathf.PI) * idleAmplitude;
            newPos.y = originalSpritePos.y + offsetY;
        }
        else
        {
            idleTime = 0f;
            originalSpritePos = spriteRb.position;
            newPos += movement * 5f * Time.fixedDeltaTime;
        }

        spriteRb.MovePosition(newPos);
    }
}
