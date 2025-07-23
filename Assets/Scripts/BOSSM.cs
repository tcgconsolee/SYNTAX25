using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.SceneManagement;

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
    public AudioSource audio;
    public GameObject healthbars;
    public AudioSource winoalert;
    public AudioSource exoaudio;
    public AudioSource winoopen;

    public GameObject fail;
    public GameObject endblack;
    public AudioSource ring;
    public GameObject bosscut;
    IEnumerator Start()
    {
        bosscut.SetActive(true);
        yield return new WaitForSeconds(4f);
        bosscut.SetActive(false);
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
        winoopen.Play();
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
        for (int i = 0; i < 70 + timeLasted / 4; i++)
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
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator FileRain()
    {
        int waves = 3;
        for (int wave = 0; wave < waves; wave++)
        {
            float screenWidth = 20f;
            int gapCount = 50 + (timeLasted / 40);
            float gapWidth = 2f;
            List<float> gapPositions = new List<float>();
            for (int i = 0; i < gapCount; i++)
            {
                gapPositions.Add(Random.Range(-screenWidth / 2 + gapWidth, screenWidth / 2 - gapWidth));
            }
            for (float x = -screenWidth / 2; x < screenWidth / 2; x += 0.5f)
            {
                bool inGap = false;
                foreach (float gapPos in gapPositions)
                {
                    if (x > gapPos - gapWidth / 2 && x < gapPos + gapWidth / 2)
                    {
                        inGap = true;
                        break;
                    }
                }
                if (!inGap)
                {
                    GameObject file = Instantiate(fileprefab, new Vector3(x, 5f, 0), Quaternion.identity);
                    StartCoroutine(FileFall(file, 3f + (timeLasted / 40f)));
                }
            }
            yield return new WaitForSeconds(1.5f);
        }
    }

    IEnumerator FileFall(GameObject file, float speed)
    {
        while (file != null && file.transform.position.y > -6f)
        {
            file.transform.position += Vector3.down * speed * Time.deltaTime;
            yield return null;
        }
        if (file != null) Destroy(file);
    }

    IEnumerator PopAtk()
    {
        GameObject obj = Instantiate(popprefab, virus.transform.position, Quaternion.identity);
        winoalert.Play();
        obj.transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        if (!(obj.transform.localScale.x > 0.9f))
        {
            Destroy(obj);
            StopCoroutine(PopAtk());
            yield break;
        }
        obj.transform.GetChild(1).gameObject.SetActive(true);
        exoaudio.Play();
        obj.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.9f);
        Destroy(obj);
    }

    IEnumerator PopAtkSmall()
    {
        GameObject obj1 = Instantiate(popprefab2, new Vector3(-5f, 0f, 0f), Quaternion.identity);
        GameObject obj2 = Instantiate(popprefab2, new Vector3(0f, 0f, 0f), Quaternion.identity);
        GameObject obj3 = Instantiate(popprefab2, new Vector3(5f, 0f, 0f), Quaternion.identity);
        winoalert.Play();
        winoalert.Play();
        winoalert.Play();
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
        exoaudio.Play();
        exoaudio.Play();
        exoaudio.Play();
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
        winoalert.Play();
        winoalert.Play();
        winoalert.Play();
        winoalert.Play();
        winoalert.Play();
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
        exoaudio.Play();
        exoaudio.Play();
        exoaudio.Play();
        exoaudio.Play();
        exoaudio.Play();
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
    IEnumerator PopAtkSmallEnd()
    {
        GameObject obj1 = Instantiate(popprefab2, new Vector3(-5f, 0f, 0f), Quaternion.identity);
        GameObject obj2 = Instantiate(popprefab2, new Vector3(0f, 0f, 0f), Quaternion.identity);
        GameObject obj3 = Instantiate(popprefab2, new Vector3(5f, 0f, 0f), Quaternion.identity);
        GameObject obj4 = Instantiate(popprefab2, new Vector3(-2.5f, 1f, 0f), Quaternion.identity);
        GameObject obj5 = Instantiate(popprefab2, new Vector3(2.5f, 1f, 0f), Quaternion.identity);
        GameObject obj6 = Instantiate(popprefab2, new Vector3(-5f, 2.5f, 0f), Quaternion.identity);
        GameObject obj7 = Instantiate(popprefab2, new Vector3(0f, 2.5f, 0f), Quaternion.identity);
        GameObject obj8 = Instantiate(popprefab2, new Vector3(5f, 2.5f, 0f), Quaternion.identity);
        GameObject obj9 = Instantiate(popprefab2, new Vector3(-5f, -2.5f, 0f), Quaternion.identity);
        GameObject obj10 = Instantiate(popprefab2, new Vector3(0f, -2.5f, 0f), Quaternion.identity);
        GameObject obj11 = Instantiate(popprefab2, new Vector3(5f, -2.5f, 0f), Quaternion.identity);
        winoalert.Play();
        winoalert.Play();
        winoalert.Play();
        winoalert.Play();
        winoalert.Play();
        obj1.transform.localScale = Vector3.zero;
        obj2.transform.localScale = Vector3.zero;
        obj3.transform.localScale = Vector3.zero;
        obj4.transform.localScale = Vector3.zero;
        obj5.transform.localScale = Vector3.zero;
        obj6.transform.localScale = Vector3.zero;
        obj7.transform.localScale = Vector3.zero;
        obj8.transform.localScale = Vector3.zero;
        obj9.transform.localScale = Vector3.zero;
        obj10.transform.localScale = Vector3.zero;
        obj11.transform.localScale = Vector3.zero;
        obj1.transform.GetChild(1).gameObject.SetActive(false);
        obj2.transform.GetChild(1).gameObject.SetActive(false);
        obj3.transform.GetChild(1).gameObject.SetActive(false);
        obj4.transform.GetChild(1).gameObject.SetActive(false);
        obj5.transform.GetChild(1).gameObject.SetActive(false);
        obj6.transform.GetChild(1).gameObject.SetActive(false);
        obj7.transform.GetChild(1).gameObject.SetActive(false);
        obj8.transform.GetChild(1).gameObject.SetActive(false);
        obj9.transform.GetChild(1).gameObject.SetActive(false);
        obj10.transform.GetChild(1).gameObject.SetActive(false);
        obj11.transform.GetChild(1).gameObject.SetActive(false);
        for (float i = 0.0f; i < 0.5083974f; i += 0.1f)
        {
            obj1.transform.localScale = new Vector3(i, i, i);
            obj2.transform.localScale = new Vector3(i, i, i);
            obj3.transform.localScale = new Vector3(i, i, i);
            obj4.transform.localScale = new Vector3(i, i, i);
            obj5.transform.localScale = new Vector3(i, i, i);
            obj6.transform.localScale = new Vector3(i, i, i);
            obj7.transform.localScale = new Vector3(i, i, i);
            obj8.transform.localScale = new Vector3(i, i, i);
            obj9.transform.localScale = new Vector3(i, i, i);
            obj10.transform.localScale = new Vector3(i, i, i);
            obj11.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2f);
        obj1.transform.GetChild(1).gameObject.SetActive(true);
        obj2.transform.GetChild(1).gameObject.SetActive(true);
        obj3.transform.GetChild(1).gameObject.SetActive(true);
        obj4.transform.GetChild(1).gameObject.SetActive(true);
        obj5.transform.GetChild(1).gameObject.SetActive(true);
        obj6.transform.GetChild(1).gameObject.SetActive(true);
        obj7.transform.GetChild(1).gameObject.SetActive(true);
        obj8.transform.GetChild(1).gameObject.SetActive(true);
        obj9.transform.GetChild(1).gameObject.SetActive(true);
        obj10.transform.GetChild(1).gameObject.SetActive(true);
        obj11.transform.GetChild(1).gameObject.SetActive(true);
        exoaudio.Play();
        exoaudio.Play();
        exoaudio.Play();
        exoaudio.Play();
        exoaudio.Play();
        obj1.GetComponent<Renderer>().enabled = false;
        obj2.GetComponent<Renderer>().enabled = false;
        obj3.GetComponent<Renderer>().enabled = false;
        obj4.GetComponent<Renderer>().enabled = false;
        obj5.GetComponent<Renderer>().enabled = false;
        obj6.GetComponent<Renderer>().enabled = false;
        obj7.GetComponent<Renderer>().enabled = false;
        obj8.GetComponent<Renderer>().enabled = false;
        obj9.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.9f);
        endblack.SetActive(true);
        ring.Play();
        Destroy(obj1);
        Destroy(obj2);
        Destroy(obj3);
        Destroy(obj4);
        Destroy(obj5);
        Destroy(obj6);
        Destroy(obj7);
        Destroy(obj8);
        Destroy(obj9);
        Destroy(obj10);
        Destroy(obj11);
    }

    IEnumerator CollapseCircleSequence()
    {
        int collapseCount = Random.Range(5, 8);
        for (int collapse = 0; collapse < collapseCount; collapse++)
        {
            yield return StartCoroutine(CollapseCircle());
            yield return new WaitForSeconds(0.6f);
        }
    }

    IEnumerator CollapseCircle()
    {
        float teleportX = Random.Range(0, 2) == 0 ? -3f : 3f;
        float teleportY = Random.Range(-1f, 1f);
        sprite.transform.position = new Vector2(teleportX, teleportY);
        StartCoroutine(CameraShake(0.3f, 0.2f));
        yield return new WaitForSeconds(0.2f);
        int charCount = 30;
        float radius = 3.5f;
        List<GameObject> circle = new List<GameObject>();
        Vector2 playerPos = sprite.transform.position;
        string lalala = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+[]{}|;:'\",.<>?/\\`~";
        int gapStart = Random.Range(0, charCount);
        int gapSize = 5;
        for (int i = 0; i < charCount; i++)
        {
            if (i >= gapStart && i < gapStart + gapSize)
                continue;
            float angle = (i / (float)charCount) * 360f * Mathf.Deg2Rad;
            Vector2 pos = playerPos + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            char x = lalala[Random.Range(0, lalala.Length)];
            GameObject obj = Instantiate(charprefab, pos, Quaternion.identity);
            var tmp = obj.GetComponent<TMP_Text>();
            tmp.text = x.ToString();
            tmp.fontSize = Random.Range(8f, 12f);
            tmp.outlineWidth = 0.5f;
            Color hexColor;
            if (ColorUtility.TryParseHtmlString("#C04060", out hexColor))
            {
                tmp.outlineColor = hexColor;
            }
            obj.transform.localScale *= Random.Range(0.8f, 1.2f);
            obj.transform.Rotate(Vector3.forward * Random.Range(-180f, 180f));
            circle.Add(obj);
        }
        for (int flash = 0; flash < 2; flash++)
        {
            foreach (var obj in circle)
            {
                if (obj != null)
                {
                    var tmp = obj.GetComponent<TMP_Text>();
                    tmp.color = flash % 2 == 0 ? Color.red : Color.white;
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
        float collapseTime = 0.6f;
        float elapsed = 0f;
        while (elapsed < collapseTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / collapseTime;
            int objIndex = 0;
            for (int i = 0; i < charCount; i++)
            {
                if (i >= gapStart && i < gapStart + gapSize)
                    continue;
                if (objIndex < circle.Count && circle[objIndex] != null)
                {
                    float angle = (i / (float)charCount) * 360f * Mathf.Deg2Rad;
                    Vector2 startPos = playerPos + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                    circle[objIndex].transform.position = Vector2.Lerp(startPos, playerPos, t);
                    var tmp = circle[objIndex].GetComponent<TMP_Text>();
                    tmp.color = new Color(tmp.color.r, tmp.color.g, tmp.color.b, 1f - t);
                    if (t > 0.9f)
                    {
                        Destroy(circle[objIndex]);
                        circle[objIndex] = null;
                    }
                    objIndex++;
                }
            }
            yield return null;
        }
        foreach (var obj in circle)
        {
            if (obj != null) Destroy(obj);
        }
    }
    IEnumerator Ending()
    {
        healthbars.SetActive(false);
        audio.Stop();
        StartCoroutine(PopAtkSmallEnd());
        StartCoroutine(PopAtkSmallEnd());
        yield return new WaitForSeconds(9f);
        SceneManager.LoadScene("ENDING_SCENE");
    }
    IEnumerator LaserSweep()
    {
        int laserLength = 40;
        List<GameObject> laser = new List<GameObject>();
        Vector2 startPos = virus.transform.position;
        string lalala = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+[]{}|;:'\",.<>?/\\`~";
        Vector2 toPlayer = sprite.transform.position - virus.transform.position;
        float angle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg;
        for (int i = 0; i < laserLength; i++)
        {
            float distance = i * 0.3f;
            Vector2 pos = startPos + new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * distance;
            char x = lalala[Random.Range(0, lalala.Length)];
            GameObject obj = Instantiate(charprefab, pos, Quaternion.identity);
            var tmp = obj.GetComponent<TMP_Text>();
            tmp.text = x.ToString();
            tmp.fontSize = 6f;
            tmp.color = new Color(1f, 0.5f, 0.5f, 0.5f);
            tmp.outlineWidth = 0.5f;
            Color hexColor;
            if (ColorUtility.TryParseHtmlString("#FF8080", out hexColor))
            {
                tmp.outlineColor = hexColor;
            }
            laser.Add(obj);
        }
        for (int flash = 0; flash < 3; flash++)
        {
            foreach (var obj in laser)
            {
                if (obj != null)
                {
                    obj.SetActive(flash % 2 == 0);
                }
            }
            yield return new WaitForSeconds(0.15f);
        }
        foreach (var obj in laser)
        {
            if (obj != null)
            {
                obj.SetActive(true);
                var tmp = obj.GetComponent<TMP_Text>();
                tmp.color = Color.red;
                tmp.fontSize = 10f;
                Color hexColor;
                if (ColorUtility.TryParseHtmlString("#C04060", out hexColor))
                {
                    tmp.outlineColor = hexColor;
                }
            }
        }
        float sweepTime = 1.3f;
        float sweepAngle = 240f;
        float elapsed = 0f;
        while (elapsed < sweepTime)
        {
            elapsed += Time.deltaTime;
            float currentAngle = angle - (sweepAngle / 2) + (sweepAngle * (elapsed / sweepTime));
            for (int i = 0; i < laser.Count; i++)
            {
                if (laser[i] != null)
                {
                    float distance = i * 0.3f;
                    Vector2 pos = startPos + new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad)) * distance;
                    laser[i].transform.position = pos;
                }
            }
            yield return null;
        }
        foreach (var obj in laser)
        {
            if (obj != null) Destroy(obj);
        }
    }

    IEnumerator FileWorm()
    {
        StartCoroutine(FileWormSingle(true));
        yield return new WaitForSeconds(2f);
        StartCoroutine(FileWormSingle(false));
        yield return new WaitForSeconds(3f);
    }

    IEnumerator FileWormSingle(bool fromLeft)
    {
        int segments = 8 + (timeLasted / 40);
        List<GameObject> worm = new List<GameObject>();
        float startY = Random.Range(-2f, 2f);
        float startX = fromLeft ? -10f : 10f;
        float direction = fromLeft ? 1f : -1f;
        for (int i = 0; i < segments; i++)
        {
            GameObject segment = Instantiate(fileprefab, new Vector3(startX - (direction * i * 0.5f), startY, 0), Quaternion.identity);
            segment.transform.localScale = Vector3.one * 0.7f;
            worm.Add(segment);
        }
        float moveSpeed = 4f + (timeLasted / 50f);
        float waveAmplitude = 1.5f;
        float waveFrequency = 2f;
        float moveTime = 0f;
        while (true)
        {
            moveTime += Time.deltaTime;
            bool allOffScreen = true;
            for (int i = 0; i < worm.Count; i++)
            {
                if (worm[i] != null)
                {
                    float x = startX + (direction * moveTime * moveSpeed) + (direction * i * 0.5f);
                    float y = startY + Mathf.Sin((moveTime * waveFrequency + i * 0.3f) * Mathf.PI) * waveAmplitude;
                    worm[i].transform.position = new Vector3(x, y, 0);
                    if ((fromLeft && x < 12f) || (!fromLeft && x > -12f))
                    {
                        allOffScreen = false;
                    }
                }
            }
            if (allOffScreen) break;
            yield return null;
        }
        foreach (var segment in worm)
        {
            if (segment != null)
            {
                Destroy(segment);
            }
        }
    }

    IEnumerator CleanupStrayObjects()
    {
        while (!end)
        {
            yield return new WaitForSeconds(5f);
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (var obj in allObjects)
            {
                if (obj != null && obj.GetComponent<TMP_Text>() != null)
                {
                    Vector3 pos = obj.transform.position;
                    if (Mathf.Abs(pos.x) > 15f || Mathf.Abs(pos.y) > 10f)
                    {
                        Destroy(obj);
                    }
                }
            }
        }
    }

    IEnumerator AttackController()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(CleanupStrayObjects());
        while (!end)
        {
            if (timeLasted < 15)
            {
                yield return StartCoroutine(DataAtk());
                yield return new WaitForSeconds(1f);
                for (int i = 0; i < 2; i++)
                {
                    yield return StartCoroutine(CollapseCircle());
                    yield return new WaitForSeconds(0.5f);
                }
                yield return new WaitForSeconds(1f);
                yield return StartCoroutine(PopAtk());
                yield return new WaitForSeconds(2f);
                yield return StartCoroutine(FileRain());
                yield return new WaitForSeconds(3f);
            }
            else if (timeLasted < 30)
            {
                yield return StartCoroutine(LaserSweep());
                yield return new WaitForSeconds(1f);
                yield return StartCoroutine(DataAtk());
                yield return new WaitForSeconds(1f);
                for (int i = 0; i < 3; i++)
                {
                    yield return StartCoroutine(CollapseCircle());
                    yield return new WaitForSeconds(0.4f);
                }
                yield return new WaitForSeconds(1f);
                yield return StartCoroutine(FileWorm());
                yield return new WaitForSeconds(2f);
                yield return StartCoroutine(PopAtkSmall());
                yield return new WaitForSeconds(2f);
            }
            else
            {
                yield return StartCoroutine(DataAtk());
                yield return new WaitForSeconds(1.5f);
                for (int i = 0; i < 4; i++)
                {
                    yield return StartCoroutine(CollapseCircle());
                    yield return new WaitForSeconds(0.3f);
                }
                yield return new WaitForSeconds(1f);
                yield return StartCoroutine(LaserSweep());
                yield return new WaitForSeconds(1f);
                yield return StartCoroutine(PopAtkSmall2());
                yield return new WaitForSeconds(1f);
                yield return StartCoroutine(FileWorm());
                yield return new WaitForSeconds(2f);
                yield return StartCoroutine(FileRain());
                yield return new WaitForSeconds(2f);
            }
        }
    }

    IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 originalPos = Camera.main.transform.position;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            Camera.main.transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = originalPos;
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
        if (timeLasted > 120)
        {
            if (!end)
            {
                end = true;
                StartCoroutine(Ending());  
            }
        }
        if (PlayerHealth <= 0 && !end)
        {
            end = true;
            healthbars.SetActive(false);
            fail.SetActive(true);
        }
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
                    end = true;
                    healthbars.SetActive(false);
                    fail.SetActive(true);
                }
                else if (result.gameObject.name == "No")
                {
                    StartCoroutine(SizeInc());
                    good.SetActive(false);
                    bad.SetActive(true);
                    StartCoroutine(TimeStart());
                    audio.Play();
                    StartCoroutine(AttackController());
                    healthbars.SetActive(true);
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
            if (end)
            {
                if (bhit.collider != null && bhit.collider.name == "Try again_0")
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                else if (bhit.collider != null && bhit.collider.name == "Exit_0")
                {
                    Application.Quit();
#if UNITY_EDITOR
                    EditorApplication.isPlaying = false;
#endif
                }
            }
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

    public void TakeDamage(int damage)
    {
        PlayerHealth -= damage;
        StartCoroutine(CameraShake(0.2f, 0.15f));
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
