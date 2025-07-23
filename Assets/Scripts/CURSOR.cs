using UnityEngine;

public class CURSOR : MonoBehaviour
{
    GameObject UI_curs;
    RectTransform rect_trans;
    public Vector2 hotspot_offset;
    
    void Start()
    {
        if(Cursor.visible)
        {
            Cursor.visible = false;
        }
        UI_curs = GameObject.Find("UICursor").transform.GetChild(0).gameObject;
        rect_trans = UI_curs.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos;
        hotspot_offset = new Vector2(5, -10);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            UI_curs.transform.parent as RectTransform,
            Input.mousePosition,
            null,
            out pos
        );
        rect_trans.anchoredPosition = pos + hotspot_offset;
    }
}
