using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEditor;
public class TERMINAL : MonoBehaviour
{
    public TMP_InputField ls;
    public TMP_InputField cat;
    public TMP_InputField decode;
    public TMP_InputField other;
    public Sprite phishing;
    public GameObject urgent;
    public GameObject classified;
    public GameObject Click;
    public GameObject notif;
    // Update is called once per frame
    void Start()
    {
        decode.interactable = false;
        cat.interactable = false;
        other.interactable = false;
        ls.onEndEdit.AddListener(LS);
        cat.onEndEdit.AddListener(CAT);
        decode.onEndEdit.AddListener(DECODE);
    }
    void LS(string text)
    {
        if (text == "ls")
        {
            ls.text = text + "\n" + "penguinlovekfc.txt";
            ls.interactable = false;
            cat.interactable = true;
            cat.ActivateInputField();
        }
    }

    void CAT(string text)
    {
        if (text == "cat penguinlovekfc.txt")
        {
            cat.text = text + "\n" + "Y2hvY29sYXRlbG92ZXJzODQucG5n";
            cat.interactable = false;
            decode.interactable = true;
            decode.ActivateInputField();
        }
    }

    void DECODE(string text)
    {
        if (text == "decode Y2hvY29sYXRlbG92ZXJzODQucG5n")
        {
            decode.text = text + "\n" + "chocolatelovers84.png";
            decode.interactable = false;
            other.interactable = true;
            other.ActivateInputField();
            GameObject.Find("Window_mail").GetComponent<SpriteRenderer>().sprite = phishing;
            urgent.SetActive(true);
            Click.SetActive(true);
            classified.SetActive(true);
            StartCoroutine(Notification());
        }
    }
    IEnumerator Notification()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(GameObject.Find("DESKTOP").GetComponent<DESKTOP>().OpenWin(notif));
        GameObject.Find("DESKTOP").GetComponent<DESKTOP>().BringWindowToFront(notif);
    }
}
