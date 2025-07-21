using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class LOGIN : MonoBehaviour
{
    public GameObject alert_win;
    public static RaycastHit2D hit;
    public GameObject cross;
    public TMP_InputField inputField;
    public bool logfinished;

    IEnumerator Alert()
    {
        alert_win.transform.localScale = new Vector3(0, 0, 0);
        for (float i = 0.0f; i < 2.1f; i += 0.2f)
        {
            alert_win.transform.localScale = new Vector3(i / 2, i / 2, i / 2);
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator Close()
    {
        alert_win.transform.localScale = new Vector3(0, 0, 0);
        for (float i = 2f; i > -0.1f; i -= 0.2f)
        {
            alert_win.transform.localScale = new Vector3(i / 2, i / 2, i / 2);
            yield return new WaitForSeconds(0.02f);
        }
    }
    void Transmit(string name)
    {
        if (name.ToLower() == "user")
        {
            inputField.gameObject.SetActive(false);
            logfinished = true;
        }
        else
        {
            StartCoroutine(Alert());
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        logfinished = false;
        inputField.onEndEdit.AddListener(Transmit);
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
                if (result.gameObject == cross)
                {
                    StartCoroutine(Close());
                    break;
                }
            }
        }
    }
}
