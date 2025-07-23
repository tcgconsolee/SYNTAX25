using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEditor;

public class MASTER : MonoBehaviour
{
    public GameObject opobj;
    public GameObject op;
    private static RaycastHit2D ehit;
    private OPENING openingsrc;
    private bool opdone;
    public GameObject cur;
    public GameObject curobj;
    public GameObject log;
    public TMP_Text clock;
    public GameObject clock_par;
    public GameObject logobj;
    private LOGIN loginsrc;
    public bool logdone;
    public GameObject desk;
    public GameObject exit;
    public TMP_InputField clockinput;

    public GameObject file;
    public TMP_InputField finput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        op.SetActive(true);
        cur.SetActive(false);
        log.SetActive(false);
        desk.SetActive(false);
        clock_par.SetActive(false);
        Cursor.visible = false;

        logdone = false;
        opdone = false;

        loginsrc = logobj.GetComponent<LOGIN>();
        openingsrc = opobj.GetComponent<OPENING>();

        clockinput.onEndEdit.AddListener(TextChange);

        finput.onEndEdit.AddListener(fCheck);
    }
    void fCheck(string text)
    {
        if (text == "chocolatelovers84.png")
        {
            file.SetActive(true);
        }
        else
        {
            file.SetActive(false);
        }
    }

    void TextChange(string time)
    {
        clock.text = time;
    }

    // Update is called once per frame
    void Update()
    {
            if (clock.text == "19:42")
            {
                file.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                file.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
            }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ehit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (ehit.collider != null && ehit.collider.name == exit.name)
            {
                Application.Quit();
#if UNITY_EDITOR
                    EditorApplication.isPlaying = false;
#endif
            }
        }
        if (openingsrc.opfinished && opdone == false)
        {
            op.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(OpFadeOut());
            opdone = true;
            cur.SetActive(true);
            log.SetActive(true);
            clock_par.SetActive(true);
        }
        if (loginsrc.logfinished && logdone == false)
        {
            logdone = true;
            desk.SetActive(true);
            StartCoroutine(slide_up());
        }
    }
    IEnumerator OpFadeOut()
    {
        float alpha = 1;
        for (float i = -0.01f; alpha > i; alpha -= 0.01f)
        {
            op.GetComponent<SpriteRenderer>().color = new Color(0.03921569f, 0f, 0.05882353f, alpha);
            yield return new WaitForSeconds(0.02f);
        }
        Destroy(op);
        Destroy(openingsrc);
    }
    IEnumerator slide_up()
    {
        for (float i = log.transform.position.y; log.transform.position.y < 70f;i++)
        {
            log.transform.position = new Vector2(log.transform.position.x,i);
            yield return new WaitForSeconds(0.02f);
        }
        // Destroy(log);
        // Destroy(loginsrc);
    }
}
