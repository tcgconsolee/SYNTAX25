using UnityEngine;
using System.Collections;
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
    public GameObject forgot;

    public Sprite bw2;
    public GameObject bwinput;
    public TMP_InputField bwinput2;

    private int windowOrder = 0; // Tracks current top layer order

    void Start()
    {
        alphaVictim = null;
        icons.SetActive(false);
    }

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
                    GameObject win = GameObject.Find(name);
                    BringWindowToFront(win);
                    StartCoroutine(OpenWin(win));
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

                // If it's part of a window, bring whole window forward
                Transform parentWin = GetWindowParent(dragobj.transform);
                if (parentWin != null)
                {
                    BringWindowToFront(parentWin.gameObject);
                }
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
                Vector3 newPos = new Vector3(currentm.x, currentm.y, dragobj.transform.position.z);
                Transform parentWin = GetWindowParent(dragobj.transform);
                if (parentWin != null)
                {
                    parentWin.position = newPos;
                }
                else
                {
                    dragobj.transform.position = newPos;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            draggingobj = false;
            dragobj = null;
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
                if (result.gameObject == forgot)
                {
                    StartCoroutine(CloseWin(GameObject.Find("Window_mail")));
                    StartCoroutine(OpenWin(GameObject.Find("Window_browser")));
                    GameObject.Find("Window_browser").GetComponent<SpriteRenderer>().sprite = bw2;
                    bwinput.SetActive(false);
                    bwinput2.text = "orvelia.gov/forgot";
                    break;
                }
            }
        }

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

    IEnumerator OpenWin(GameObject obj)
    {
        obj.transform.localScale = Vector3.zero;
        for (float i = 0.0f; i < 2.1f; i += 0.2f)
        {
            obj.transform.localScale = new Vector3(i / 2, i / 2, i / 2);
            yield return new WaitForSeconds(0.02f);
        }

        if (obj.name == "Window_terminal")
        {
            StartCoroutine(DelayedActivateInput());
        }
    }

    IEnumerator CloseWin(GameObject obj)
    {
        for (float i = 2f; i > -0.1f; i -= 0.2f)
        {
            obj.transform.localScale = new Vector3(i / 2, i / 2, i / 2);
            yield return new WaitForSeconds(0.02f);
        }
        obj.transform.localScale = Vector3.zero;
    }

    IEnumerator DelayedActivateInput()
    {
        yield return null;
        input.onValueChanged.RemoveListener(tInput);
        input.ActivateInputField();
        input.onValueChanged.AddListener(tInput);
    }

    void tInput(string yn)
    {
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

    void BringWindowToFront(GameObject win)
    {
        windowOrder += 2; // Parent and children get different values
        SetSortingOrderRecursive(win.transform, windowOrder);
    }

    void SetSortingOrderRecursive(Transform root, int baseOrder)
    {
        SpriteRenderer sr = root.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingOrder = baseOrder;
        }

        Canvas canvas = root.GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.overrideSorting = true;
            canvas.sortingOrder = baseOrder;
        }

        foreach (Transform child in root)
        {
            SetSortingOrderRecursive(child, baseOrder + 1);
        }
    }

    Transform GetWindowParent(Transform obj)
    {
        while (obj != null)
        {
            if (obj.name.StartsWith("Window_"))
                return obj;
            obj = obj.parent;
        }
        return null;
    }
}
