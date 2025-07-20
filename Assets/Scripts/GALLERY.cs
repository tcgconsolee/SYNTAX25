using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

public class GALLERY : MonoBehaviour
{
    private static RaycastHit2D ghit;
    public GameObject minus;
    public GameObject plus;
    public GameObject imgs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ghit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (ghit.collider != null)
            {
                if (ghit.collider.name == plus.name)
                {
                    if (imgs.transform.localScale.x < 1.5f)
                    {
                        Vector3 scaleChange = new Vector3(0.25f, 0.25f, 0f);
                        Vector3 positionOffset = new Vector3(scaleChange.x *4f, -scaleChange.y*2f , 0f);
                        imgs.transform.localScale += scaleChange;
                        imgs.transform.position += positionOffset;
                    }
                }
                else if (ghit.collider.name == minus.name)
                {
                    if (imgs.transform.localScale.x > 1f)
                    {
                        Vector3 scaleChange = new Vector3(0.25f, 0.25f, 0f);
                        Vector3 positionOffset = new Vector3(scaleChange.x *4f, -scaleChange.y *2f, 0f);
                        imgs.transform.localScale -= scaleChange;
                        imgs.transform.position -= positionOffset;
                    }
                }
            }
        }
    }
}
