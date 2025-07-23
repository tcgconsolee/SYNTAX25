using System;
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
    public Sprite nwiki;
    public TMP_Text uploaded;
    public Sprite cracked;

    public GameObject flicker;

    public GameObject orvelia1;
    public GameObject orvelia2;
    public GameObject orvelia3;
    public GameObject orvelia4;

    public GameObject true1;
    public GameObject true2;
    public GameObject true3;
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
        if (text == "biodata.txt")
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
        if (!(GameObject.Find("desktop").GetComponent<SpriteRenderer>().sprite == cracked))
        {
            if (text.ToLower() == "kernel augustus")
            {
                StartCoroutine(Flicker(GameObject.Find("DESKTOP").GetComponent<DESKTOP>().NotepadChange));
            }
        }
        else
        if (text == "orvelia.gov/upload")
        {
            uploaded.gameObject.SetActive(true);
            inp3.gameObject.SetActive(false);
            inp4.gameObject.SetActive(true);
            inp1.gameObject.SetActive(false);

            GameObject.Find("Window_browser").GetComponent<SpriteRenderer>().sprite = nupload;
        }
        else
        if (text == "wikidrains.org")
        {
            uploaded.gameObject.SetActive(true);
            inp3.gameObject.SetActive(false);
            inp4.gameObject.SetActive(true);
            inp1.gameObject.SetActive(false);

            GameObject.Find("Window_browser").GetComponent<SpriteRenderer>().sprite = nwiki;
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


    IEnumerator Flicker(Action callback)
    {
        yield return new WaitForSeconds(1f);
        flicker.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        flicker.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        flicker.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        flicker.SetActive(false);
        callback?.Invoke();
    }
    // Update is called once per frame
    IEnumerator Orvelia()
    {
        orvelia1.SetActive(true);
        yield return new WaitForSeconds(4f);
        orvelia1.SetActive(false);
        orvelia2.SetActive(true);
        yield return new WaitForSeconds(4f);
        orvelia2.SetActive(false);
        orvelia3.SetActive(true);
        yield return new WaitForSeconds(4f);
        orvelia3.SetActive(false);
        orvelia4.SetActive(true);
        yield return new WaitForSeconds(4f);
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
    IEnumerator True()
    {
        true1.SetActive(true);
        yield return new WaitForSeconds(4f);
        true1.SetActive(false);
        true2.SetActive(true);
        yield return new WaitForSeconds(4f);
        true2.SetActive(false);
        true3.SetActive(true);
        yield return new WaitForSeconds(4f);
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
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
                if (result.gameObject == uploaded.gameObject)
                {
                    if (uploaded.color.a == 1f)
                    {
                        uploaded.text = "sent";
                        if (GameObject.Find("Window_browser").GetComponent<SpriteRenderer>().sprite == nwiki)
                        {
                            StartCoroutine(True());
                        }
                        else if (GameObject.Find("Window_browser").GetComponent<SpriteRenderer>().sprite == nupload)
                        {
                            StartCoroutine(Orvelia());
                        }
                    }
                }
            }
        }
    }
}
