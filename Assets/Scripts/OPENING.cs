using UnityEngine;
using System.Collections;

public class OPENING : MonoBehaviour
{
    Component txt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        txt = GameObject.Find("grptext").GetComponent<Component>();
        for (int i = 0; i < txt.transform.childCount; i++) {
            txt.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < txt.transform.childCount; i++) {
            txt.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(.35f);
        }
    }

    void Update()
    {
        if (txt.transform.position.y >900.0)
        {
            return;
        }
        var txtpos = txt.transform.position;
        txt.transform.position = new Vector2(txtpos.x,txtpos.y+0.1f);
    }
}
