using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEditor;

public class DESKTOP : MonoBehaviour
{
    public static RaycastHit2D dhit;
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f;
    private Collider2D alphaVictim;
    private Vector2 worldPoint;
    private GameObject dragobj;
    private bool draggingobj = false;
    private bool dragging = false;
    public TMP_InputField input;
    public GameObject icons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        alphaVictim = null;
        icons.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dhit = Physics2D.Raycast(worldPoint, Vector2.zero);
            float timeSinceLastClick = Time.time - lastClickTime;
            if (timeSinceLastClick <= doubleClickThreshold)
            {
                if (dhit.collider != null && dhit.collider.name.Contains("_ICON"))
                {
                    var name = "Window_" + dhit.collider.name.Split("_ICON")[0];
                    StartCoroutine(OpenWin(GameObject.Find(name)));
                }
            }

            lastClickTime = Time.time;
            if (dhit.collider == null || dhit.collider.name.Contains("_CROSS") || dhit.collider.name.Contains("_0"))
            {
                dragobj = null;
                draggingobj = false;
                dragging = false;
            }
            else
            {
                dragobj = dhit.collider.gameObject;
                draggingobj = true;
                dragging = false;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dhit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (dhit.collider != null && dhit.collider.name.Contains("_CROSS"))
            {
                var name = "Window_" + dhit.collider.name.Split("_CROSS")[0];
                StartCoroutine(CloseWin(GameObject.Find(name)));
            }
            else if (dhit.collider != null && dhit.collider.name.Contains("_ICON"))
            {
                dhit.collider.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.6f);
                alphaVictim = dhit.collider;
            }
            if (alphaVictim != null && dhit.collider != alphaVictim)
            {
                alphaVictim.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
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
                dragobj.transform.position = new Vector3(currentm.x, currentm.y, dragobj.transform.position.z);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            draggingobj = false;
            dragobj = null;
        }
    }

    IEnumerator OpenWin(GameObject obj)
    {
        obj.transform.localScale = new Vector3(0, 0, 0);
        for (float i = 0.0f; i < 2.1f; i += 0.2f)
        {
            obj.transform.localScale = new Vector3(i / 2, i / 2, i / 2);
            yield return new WaitForSeconds(0.02f);
        }
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            obj.transform.GetChild(i).gameObject.SetActive(true);
        }
        if (obj.name == "Window_terminal")
        {
            StartCoroutine(DelayedActivateInput());  
        }
    }

    IEnumerator DelayedActivateInput()
    {
        yield return null;
        input.onValueChanged.RemoveListener(tInput);
        input.ActivateInputField();
        input.onValueChanged.AddListener(tInput);
    }
    void tInput(string yn) {
        if (yn == "y")
        {
            StartCoroutine(CloseWin(GameObject.Find("Window_terminal")));
            GameObject.Find("terminal_ICON").SetActive(false);
            icons.SetActive(true);
        }
        else if (yn == "n")
        {
            Application.Quit();
            #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
            #endif
        }
        else
        {
            input.text = "";
        }
    }
    IEnumerator CloseWin(GameObject obj)
    {
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            obj.transform.GetChild(i).gameObject.SetActive(false);
        }
        obj.transform.localScale = new Vector3(0, 0, 0);
        for (float i = 2f; i > -0.1f; i -= 0.2f)
        {
            obj.transform.localScale = new Vector3(i / 2, i / 2, i / 2);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
