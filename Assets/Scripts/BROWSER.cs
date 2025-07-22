using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEditor;

public class BROWSER : MonoBehaviour
{

    public TMP_InputField inp1;
    public TMP_InputField inp2;
    public TMP_InputField inp3;
    public TMP_InputField inp4;
    public Sprite bsprite;
    public Sprite nsurf;
    public Sprite nforgot;
    public Sprite nupload;
    public TMP_Text uploaded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inp1.onEndEdit.AddListener(Change);
        inp2.onEndEdit.AddListener(Change);
        inp3.onEndEdit.AddListener(Change);

        inp4.onEndEdit.AddListener(Upload);
    }
    void Upload(string text)
    {
        if (text == "chocolatelovers84.png")
        {
            uploaded.color = new Color(0f, 0f, 0f, 1f);
        }
        else
        {
            uploaded.color = new Color(0f, 0f, 0f, 0.6f);
        }
    }
    void Change(string text)
    {
        inp1.text = text;
        inp2.text = text;
        inp3.text = text;

        if (text == "")
        {
            uploaded.gameObject.SetActive(false);
            inp3.gameObject.SetActive(false);
            inp1.gameObject.SetActive(true);
            inp4.gameObject.SetActive(false);

            GameObject.Find("Window_browser").GetComponent<SpriteRenderer>().sprite = nsurf;
        }
        else
        if (text == "orvelia.gov/forgot")
        {
            uploaded.gameObject.SetActive(false);
            inp3.gameObject.SetActive(false);
            inp4.gameObject.SetActive(false);
            inp1.gameObject.SetActive(false);

            GameObject.Find("Window_browser").GetComponent<SpriteRenderer>().sprite = nforgot;
        }
        else
        if (text == "orvelia.gov/upload" || text.ToLower() == "kernel augustus")
        {
            uploaded.gameObject.SetActive(true);
            inp3.gameObject.SetActive(false);
            inp4.gameObject.SetActive(true);
            inp1.gameObject.SetActive(false);

            inp2.text = "orvelia.gov/upload";

            GameObject.Find("Window_browser").GetComponent<SpriteRenderer>().sprite = nupload;
        }
        else
        {
            uploaded.gameObject.SetActive(false);
            inp3.gameObject.SetActive(true);
            inp4.gameObject.SetActive(false);
            inp1.gameObject.SetActive(false);

            GameObject.Find("Window_browser").GetComponent<SpriteRenderer>().sprite = bsprite;
        }
    }

    // Update is called once per frame
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
                if (result.gameObject == uploaded.gameObject)
                {
                    if (uploaded.color.a == 1f)
                    {
                        uploaded.text = "sent";
                    }
                }
            }
        }
    }
}
