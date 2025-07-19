using UnityEngine;

public class MASTER : MonoBehaviour
{
    public GameObject opobj;
    private OPENING openingsrc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        openingsrc = opobj.GetComponent<OPENING>();
    }

    // Update is called once per frame
    void Update()
    {
        if (openingsrc.opfinished)
        {
            // login page show
        }
    }
}
