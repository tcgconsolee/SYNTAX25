using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using TMPro;

public class OPENING : MonoBehaviour
{
    Component txt;
    public bool opfinished;
    public GameObject opencut;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "risk.txt");
    private WIN_NOTIF wn_al;
    private bool cutfinished = false;
    
    public AudioSource intro;
    private bool IsRisky(WIN_NOTIF wn_al)
    {
        if (!File.Exists(filePath))
        {
            wn_al.TriggerAlert("RISK", "You're meddling with something you really shouldn't. But, if you're foolish, you can still proceed. Maybe check your desktop, you might find something...");
            File.WriteAllText(filePath, "Are you SURE you're willing to take this risk? This is not your world afterall...\nType yes in the line below if you are.\nno");
            return false;
        }

        string[] lines = File.ReadLines(filePath).Take(3).ToArray();
        if (lines.Length >= 3 && lines[2].ToLower().Contains("yes"))
        {
            return true;
        }

        if (lines.Length >= 3 && (!(lines[2].ToLower().Contains("yes") || lines[2].ToLower().Contains("no"))))
        {
            // TODO: bossfight scene switching shenanigans
        }
        wn_al.TriggerAlert("RISK", "What? Still here?");
        return false;
    }
    
    IEnumerator Start()
    {
        wn_al = transform.parent.GetComponentInChildren<WIN_NOTIF>();
        if(!IsRisky(wn_al))
        {
            // TODO: Toast alert code goes here
            Application.Quit();
            #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
            #endif
        }
        txt = GameObject.Find("grptext").GetComponent<Component>();
        txt.gameObject.SetActive(false);
        opencut.SetActive(true);
        yield return new WaitForSeconds(10f);
        opencut.SetActive(false);
        txt.gameObject.SetActive(true);
        intro.Play();
        cutfinished = true;
        opfinished = false;
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
        if (!cutfinished) return;
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
