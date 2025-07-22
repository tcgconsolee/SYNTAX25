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
    // Update is called once per frame
    void Start()
    {
        cat.interactable = false;
        ls.onEndEdit.AddListener(LS);
        cat.onEndEdit.AddListener(CAT);
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
            cat.text = text + "\n" + "lY2hvY29sYXRlbG92ZXJzODQ=";
            cat.interactable = false;
        }
    }
}
