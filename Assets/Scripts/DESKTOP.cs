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

    public TMP_InputField minput;
    public TMP_InputField minput2;
    public Sprite msprite;

    private int windowOrder = 0;
    public Sprite tsprite;

    public GameObject properties;
    public GameObject metadata;
    public Sprite kernel;
    public Sprite code;

    public AudioSource openwindow;
    public GameObject urgent1;
    public GameObject classified1;
    public GameObject click1;

    public Sprite urgents;
    public Sprite classifieds;
    public bool tdone = false;
    public bool mdone = false;
    void Start()
    {
        alphaVictim = null;
        icons.SetActive(false);

        minput.onEndEdit.AddListener(mCheck);
        minput2.onEndEdit.AddListener(mCheck);

    }
    void mCheck(string text)
    {
        if (minput2.text == "georje.granfrere@orvelia.gov" && minput.text == "dontbeevil7")
        {
            GameObject.Find("Window_mail").GetComponent<SpriteRenderer>().sprite = msprite;
            minput.gameObject.SetActive(false);
            minput2.gameObject.SetActive(false);
            GameObject.Find("forgot").SetActive(false);
            GameObject.Find("mail_CROSS").transform.localPosition = new Vector3(4.5f, 3.45f, 0);
            BoxCollider2D box = GameObject.Find("Window_mail").GetComponent<BoxCollider2D>();
            box.offset = new Vector2(0, 3.52f);
            mdone = true;
        } 
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

                Transform parentWin = GetWindowParent(dragobj.transform);
                if (parentWin != null)
                {
                    BringWindowToFront(parentWin.gameObject);
                }
            }

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

        if (alphaVictim != null && dhit.collider != alphaVictim)
        {
            alphaVictim.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }

        if (Input.GetMouseButtonDown(1))
        {
            worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dhit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (dhit.collider != null && dhit.collider.name == "chocolatelovers84_0")
            {
                properties.SetActive(true);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dhit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (dhit.collider != null)
            {
                if (dhit.collider.name == properties.name)
                {
                    StartCoroutine(OpenWin(metadata));
                    BringWindowToFront(metadata);
                }
                if (dhit.collider.name == "chocolatelovers84_0")
                {
                    if (dhit.collider.gameObject.GetComponent<SpriteRenderer>().color.a == 1f)
                    {
                        GameObject window = GameObject.Find("Window_img");
                        window.GetComponent<SpriteRenderer>().sprite = kernel;
                        window.transform.GetChild(1).gameObject.SetActive(false);
                        window.transform.GetChild(2).gameObject.SetActive(false);
                        window.transform.GetChild(3).gameObject.SetActive(false);
                        StartCoroutine(OpenWin(window));
                        BringWindowToFront(window);
                    }
                }
                if (dhit.collider.name == "urgent_0")
                {
                    GameObject.Find("Window_mail").GetComponent<SpriteRenderer>().sprite = urgents;
                    click1.SetActive(true);
                    urgent1.SetActive(true);
                    classified1.SetActive(true);
                }
                else if (dhit.collider.name == "classified_0")
                {
                    GameObject.Find("Window_mail").GetComponent<SpriteRenderer>().sprite = classifieds;
                    click1.SetActive(false);
                    urgent1.SetActive(true);
                    classified1.SetActive(true);
                }
                if (dhit.collider.name == "Click_0")
                {
                    FindObjectOfType<Shake>().StartCoroutine(FindObjectOfType<Shake>().ShakeC(0.1f, 0.2f));
                }
            }
            properties.SetActive(false);
        }
    }

    public IEnumerator OpenWin(GameObject obj)
    {
        obj.transform.localScale = Vector3.zero;
        openwindow.Play();
        for (float i = 0.0f; i < 2.1f; i += 0.4f)
        {
            obj.transform.localScale = new Vector3(i / 2, i / 2, i / 2);
            yield return new WaitForSeconds(0.1f);
        }
        BringWindowToFront(obj);

        if (obj.name == "Window_terminal")
        {
            StartCoroutine(DelayedActivateInput());
        }
    }

    IEnumerator CloseWin(GameObject obj)
    {
        for (float i = 2f; i > -0.1f; i -= 0.4f)
        {
            obj.transform.localScale = new Vector3(i / 2, i / 2, i / 2);
            yield return new WaitForSeconds(0.1f);
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
    public void NotepadChange()
    {
        GameObject.Find("Window_notepad").GetComponent<SpriteRenderer>().sprite = code;
        StartCoroutine(OpenWin(GameObject.Find("Window_notepad")));
        BringWindowToFront(GameObject.Find("Window_notepad"));
    }
    void tInput(string yn)
    {
        if (yn == "y")
        {
            StartCoroutine(CloseWin(GameObject.Find("Window_terminal")));
            icons.SetActive(true);
            GameObject.Find("Window_terminal").GetComponent<SpriteRenderer>().sprite = tsprite;
            GameObject.Find("Window_terminal").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("Window_terminal").transform.GetChild(1).gameObject.SetActive(true);
            tdone = true;
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

    public void BringWindowToFront(GameObject win)
    {
        windowOrder += 2;
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
