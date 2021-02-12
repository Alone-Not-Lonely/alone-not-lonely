using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    List<string> items;
    public Image keyImage;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);//Keep persistant
        items = new List<string>();
        keyImage.enabled = false;
    }

    //adds an item's key to the list
    //and performs any feedback for pickup
    public void addItem(Item item)
    {
        items.Add(item.key);
        keyImage.sprite = item.representation;
        keyImage.enabled = true;
        //feedback here
    }

    //Checks to see if every required item's key exists in the player's items
    public bool checkContents(List<string> requirements)
    {
        foreach (string req in requirements)
        {
            if (!items.Contains(req))
            {
                return false;
            }
        }

        return true;
    }
}
