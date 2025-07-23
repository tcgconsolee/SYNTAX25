using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEditor;
public class PHONE : MonoBehaviour
{
    public static RaycastHit2D phit;
    public TMP_InputField digits;
    public Sprite caller;
    public Sprite phone;
    public Sprite biodatatxt;
    public Sprite broken;
    public AudioSource phonecall;
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            phit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (phit.collider != null)
            {
                if (phit.collider.name == "cut_0")
                {
                    GameObject.Find("Window_phone").GetComponent<SpriteRenderer>().sprite = phone;
                    GameObject.Find("Window_phone").transform.GetChild(1).gameObject.SetActive(true);
                    GameObject.Find("Window_phone").transform.GetChild(2).gameObject.SetActive(false);
                    if (phonecall.isPlaying)
                    {
                        phonecall.Stop();
                    }
                }
            }
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
                if (result.gameObject.name.Contains("_key"))
                {
                    if (digits.text.Length > 5)
                    {
                        break;
                    }
                    digits.text = digits.text + result.gameObject.name.Split("_")[0];
                    break;
                }
                if (result.gameObject.name.Contains("kef"))
                {
                    if (digits.text.Length == 0)
                    {
                        break;
                    }
                    if (result.gameObject.name == "-_kef")
                    {
                        digits.text = digits.text.Substring(0, digits.text.Length - 1);
                    }
                    else if (result.gameObject.name == "C_kef")
                    {
                        digits.text = "";
                    }
                    else if (result.gameObject.name == "call_kef" || result.gameObject.name == "callplus_kef")
                    {
                        if (digits.text == "000000")
                        {
                            GameObject.Find("Window_phone").GetComponent<SpriteRenderer>().sprite = caller;
                            GameObject.Find("Window_phone").transform.GetChild(1).gameObject.SetActive(false);
                            GameObject.Find("Window_phone").transform.GetChild(2).gameObject.SetActive(true);
                            phonecall.Play();
                        }
                        else if (digits.text == "463546" && GameObject.Find("desktop").GetComponent<SpriteRenderer>().sprite == broken)
                        {
                            GameObject.Find("Window_notepad").GetComponent<SpriteRenderer>().sprite = biodatatxt;
                            StartCoroutine(GameObject.Find("DESKTOP").GetComponent<DESKTOP>().OpenWin(GameObject.Find("Window_notepad")));
                            GameObject.Find("DESKTOP").GetComponent<DESKTOP>().BringWindowToFront(GameObject.Find("Window_notepad"));
                        }
                    }
                    break;
                }
            }
        }
    }
}
