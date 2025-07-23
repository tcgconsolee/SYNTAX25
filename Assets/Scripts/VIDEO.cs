using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

public class VIDEO : MonoBehaviour
{
    private static RaycastHit2D vhit;
    public GameObject sound;
    public GameObject windowv;
    public TMP_Text radio;
    public GameObject backward;
    public GameObject forward;
    public GameObject stop;
    public GameObject play;
    public GameObject redheart;
    public GameObject heart;
    private int si;
    public AudioSource vm;
    public AudioSource staticvm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        si = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (windowv.transform.localScale.x > 0.5f)
        {
            if (radio.text == "94.1")
            {
                if (!vm.isPlaying)
                {
                    vm.Play();
                }
                if (staticvm.isPlaying)
                {
                    staticvm.Stop();
                }
            }
            else
            {
                if (!staticvm.isPlaying)
                {
                    staticvm.Play();
                }
                if (vm.isPlaying)
                {
                    vm.Stop();
                }
            }
        }
        else
        {
            if (staticvm.isPlaying)
            {
                staticvm.Stop();
            }
            if (vm.isPlaying)
            {
                vm.Stop();
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            vhit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (vhit.collider != null)
            {
                if (vhit.collider.name == heart.name)
                {
                    heart.SetActive(false);
                    redheart.SetActive(true);
                }
                else if (vhit.collider.name == redheart.name)
                {
                    redheart.SetActive(false);
                    heart.SetActive(true);
                }
                if (vhit.collider.name == play.name)
                {
                    if (radio.text == "94.1")
                    {
                        if (!vm.isPlaying)
                        {
                            vm.Play();
                        }
                    }
                }
                if (vhit.collider.name == stop.name)
                {
                    if (radio.text == "94.1")
                    {
                        if (vm.isPlaying)
                        {
                            vm.Stop();
                        }
                    }
                }
                if (vhit.collider.name == sound.name)
                {
                    if (si == 1)
                    {
                        sound.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.6f);
                        si = 2;
                    }
                    else if (si == 2)
                    {
                        sound.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                        si = 3;
                    }
                    else if (si == 3)
                    {
                        sound.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f);
                        si = 1;
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            vhit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (vhit.collider != null)
            {
                if (float.Parse(radio.text) < 60.1f)
                {
                    radio.text = "60.1";
                }
                if (float.Parse(radio.text) > 149.9f)
                {
                    radio.text = "149.9";
                }
                if (vhit.collider.name == forward.name)
                {
                    float foo = float.Parse(radio.text);
                    radio.text = (foo + 0.1f).ToString();
                }
                if (vhit.collider.name == backward.name)
                {
                    float foo = float.Parse(radio.text);
                    radio.text = (foo - 0.1f).ToString();
                }
            }
        }
    }
}
