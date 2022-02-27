using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OffScreenIndicatorsSystem : MonoBehaviour
{
    List<OffScreenIndicatorView> targets = new List<OffScreenIndicatorView>();

    private void handlItemPickedup()
    {
        
    }

    // Use this for initialization
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Food");
        foreach (GameObject ob in objs)
        {
            OffScreenIndicatorView o = ob.GetComponent<OffScreenIndicatorView>();

            if (o != null)
            {
                o.onItemVisibleOnscreen.AddListener(this.handlItemPickedup);
                targets.Add(o);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
