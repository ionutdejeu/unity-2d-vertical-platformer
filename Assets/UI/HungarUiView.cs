using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HungarUiView : MonoBehaviour
{
    [SerializeField] Scrollbar scrollbar;
    // Use this for initialization
    public void SetHungarValue(int v)
    {
        scrollbar.value = v;
    }
}
