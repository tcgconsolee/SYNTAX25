using System.Runtime.InteropServices;
using UnityEngine;

public class WIN_NOTIF : MonoBehaviour
{
    [DllImport("SyntaxToastHelper.dll", CharSet = CharSet.Unicode)]        
    public static extern void ShowNativeAlert(string title, string message);

    public void TriggerAlert(string title, string message)
    {
        ShowNativeAlert(title, message);
    }
}
