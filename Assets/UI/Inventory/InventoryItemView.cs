using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemView : MonoBehaviour
{
    public Text textName;
    public Image sprite;

    protected InventoryItem item; 
    // Start is called before the first frame update
    void Start()
    {
        if (item != null)
        {
            HidrateView(item);
        }   
    }

    public void HidrateView(InventoryItem i)
    {
        item = i;
        if (textName != null) textName.text = item.name;
        if (sprite != null) sprite.sprite = item.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
