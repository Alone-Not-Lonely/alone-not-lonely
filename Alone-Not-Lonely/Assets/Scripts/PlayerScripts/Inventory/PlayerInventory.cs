using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    List<Item> items;
    public Canvas _canvas;
    [SerializeField]
    private Animator puzzAnim;
    [SerializeField]
    private Text puzzText;
    private int numPuzz = 0;

    void Awake()
    {
        items = new List<Item>();
        //DontDestroyOnLoad(this.gameObject);//Keep persistant
    }

    //adds an item's key to the list
    //and performs any feedback for pickup
    public void addItem(Item item)
    {
        Debug.Log("adding item");
        items.Add(item);
        Debug.Log("Item added, starting coroutine");
        //feedback here
        StartCoroutine("feedback", "picked up: " + item.key);
        if (item.key.Contains("Piece"))//puzzle pieces must be named "Something Piece
        {
            StartCoroutine("puzzleGet");
        }
    }

    //Makes visible and audible effects for key collection
    IEnumerator puzzleGet()
    {
        puzzAnim.SetBool("Gotten Key", true);
        //sound effect here
        yield return new WaitForSeconds(.3f);
        //puzzAnim.speed = 0;
        numPuzz++;
        puzzText.text = numPuzz.ToString();
        //yield return new WaitForSeconds(.2f);
        //puzzAnim.speed = 1;
        puzzAnim.SetBool("Gotten Key", false);
    }

    //and performs any feedback for pickup
    public void removeItem(Item item)
    {
        items.Remove(item);

        //feedback here
        StartCoroutine("feedback", "used up: " + item.key);
    }

    //Checks to see if every required item's key exists in the player's items
    public bool checkContents(List<Item> requirements)
    {
        foreach (Item req in requirements)
        {
            if (!items.Contains(req))
            {
                return false;
            }
        }
        return true;
    }

    //currently creates a text that will disappear after 2 seconds
    //A known bug of this is layering texts, but its just a temp implementation
    IEnumerator feedback(string message)
    {
        Text _text = _canvas.gameObject.AddComponent<Text>();
        _text.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        _text.fontSize = 40;
        _text.text = message;
        yield return new WaitForSeconds(2);

        Destroy(_text);

    }
}