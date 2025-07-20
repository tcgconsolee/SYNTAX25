using UnityEngine;
using System.Collections;
using UnityEngine;
using TMPro;

public class OPENING : MonoBehaviour
{
    Component txt;
    public bool opfinished;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        opfinished = false;
        txt = GameObject.Find("grptext").GetComponent<Component>();
        for (int i = 0; i < txt.transform.childCount; i++)
        {
            txt.transform.GetChild(i).gameObject.SetActive(false);
        }
        txt.transform.localScale = new Vector2(Screen.width / 1920.0f, Screen.width / 1920.0f);
        for (int i = 0; i < txt.transform.childCount; i++)
        {
            var txtpos = txt.transform.position;
            txt.transform.position = new Vector2(txtpos.x, txtpos.y + (50f * Screen.width / 1920.0f));
            txt.transform.GetChild(i).gameObject.SetActive(true);
            if (i ==0 || i == 15 || i == 20 || i == 21 || i == 24 || i == 26)
            {
                yield return new WaitForSeconds(0.8f);
            }
            else
            {
                yield return new WaitForSeconds(.15f);
            }
            if (i == txt.transform.childCount - 1)
            {
                yield return new WaitForSeconds(1.25f);
                opfinished = true;
            }
        }
    }

    void Update()
    {
        if (opfinished)
        {
            txt.transform.position = new Vector2(80.0f * Screen.width / 1920.0f, 1800.0f * Screen.width / 1920.0f);
        }
        if (txt.transform.position.y > (1800.0f * Screen.width / 1920.0f))
        {
            return;
        }
    }
}
