using UnityEngine;
using System.Collections;
using UnityEngine;
using TMPro;

public class MASTER : MonoBehaviour
{
    public GameObject opobj;
    public GameObject op;
    private OPENING openingsrc;
    private bool opdone;
    public GameObject log;
    public TMP_Text clock;
    public GameObject clock_par;
    public GameObject logobj;
    private LOGIN loginsrc;
    private bool logdone;
    public GameObject desk;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        op.SetActive(true);
        log.SetActive(false);
        desk.SetActive(false);
        clock_par.SetActive(false);

        logdone = false;
        opdone = false;

        loginsrc = logobj.GetComponent<LOGIN>();
        openingsrc = opobj.GetComponent<OPENING>();
    }

    // Update is called once per frame
    void Update()
    {
        clock.text = System.DateTime.Now.ToString().Split("2025")[1].Split(":")[0] + ":" + System.DateTime.Now.ToString().Split("2025")[1].Split(":")[1];
        if (openingsrc.opfinished && opdone == false)
        {
            op.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(fade_out());
            opdone = true;
            log.SetActive(true);
            clock_par.SetActive(true);
        }
        if (loginsrc.logfinished && logdone == false)
        {
            logdone = true;
            desk.SetActive(true);
            StartCoroutine(slide_up());
        }
    }
    IEnumerator fade_out()
    {
        float alpha = 1;
        for (float i = -0.01f; alpha > i; alpha -= 0.01f)
        {
            op.GetComponent<SpriteRenderer>().color = new Color(0.03921569f, 0f, 0.05882353f, alpha);
            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator slide_up()
    {
        for (float i = log.transform.position.y; log.transform.position.y < 70f;i++)
        {
            log.transform.position = new Vector2(log.transform.position.x,i);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
