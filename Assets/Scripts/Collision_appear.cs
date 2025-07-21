using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEditor;


public class Collision_appear : MonoBehaviour
{
    public GameObject object_p;

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(Enter());
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        StartCoroutine(Exit());
    }
    IEnumerator Enter()
    {
        float alpha = 0f;
        for (float i = 0.41f; alpha < i; alpha += 0.01f)
        {
            object_p.GetComponent<SpriteRenderer>().color = new Color(0.7207546f, 0.1835884f, 0.1835884f, alpha);
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator Exit()
    {
        float alpha = 0.4f;
        for (float i = -0.01f; alpha > i; alpha -= 0.01f)
        {
            object_p.GetComponent<SpriteRenderer>().color = new Color(0.7207546f, 0.1835884f, 0.1835884f, alpha);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
