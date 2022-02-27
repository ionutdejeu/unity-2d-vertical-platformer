using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class OffScreenIndicatorView : MonoBehaviour
{
    [SerializeField] Color indicatorColor;
    [SerializeField] float Lifespan;

    public UnityEvent onItemVisibleOnscreen = new UnityEvent();


    void OnBecameVisible()
    {
        Debug.Log("Visible");
    }

    private void OnBecameInvisible()
    {
        Debug.Log("Invisible");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
