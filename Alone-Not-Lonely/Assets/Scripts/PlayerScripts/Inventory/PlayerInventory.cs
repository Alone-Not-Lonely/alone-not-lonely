using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    List<Item> items;
    public List<int> puzzlePieces;
    public Canvas _canvas;
    [SerializeField]
    private Animator puzzAnim;
    [SerializeField]
    private Text puzzText;
    private int numPuzz = 0;
    public GameObject keyUI;
    public GameObject keyTemplate;
    
    public static PlayerInventory instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
        items = new List<Item>();
        puzzlePieces = new List<int>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        KeyBaring[] puzzlePieceHolders = FindObjectsOfType<KeyBaring>();
        Debug.Log(puzzlePieceHolders.Length + " piece holders");
        foreach(KeyBaring p in puzzlePieceHolders)
        {
            Debug.Log("ID: " + p.ID);
            if(puzzlePieces.Contains(p.ID))
            {
                Debug.Log("Removed for duplicate");
                p.gameObject.SetActive(false);
            }
        }
    }

    private void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //adds an item's key to the list
    //and performs any feedback for pickup
    public void addItem(Item item)
    {
        Debug.Log("adding item");
        items.Add(item);
        Debug.Log("Item added, starting coroutine");
        //feedback here
        //StartCoroutine("feedback", "picked up: " + item.key);
        if (item.key.Contains("Piece"))//puzzle pieces must be named "Something Piece
        {
            StartCoroutine("puzzleGet");
        }

        if (item.key.Contains("Key"))//puzzle pieces must be named "Something Piece
        {
            StartCoroutine("keyGet", item);
        }
    }

    public void addPuzzlePiece(int puzzID)
    {
        puzzlePieces.Add(puzzID);
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

    IEnumerator keyGet(Item item)
    {
        GameObject key = Instantiate(keyTemplate);
        /*key.transform.parent = keyUI.transform;*/ key.transform.SetParent(keyUI.transform, false);
        key.name = item.key;
        //key.transform.GetChild(1).GetComponent<Text>().text = item.key;
        key.transform.GetChild(1).GetComponent<Text>().text = "";
        key.transform.GetChild(0).GetComponent<Image>().color = item.GetComponent<MeshRenderer>().material.color;  //item.key, 
        yield return new WaitForSeconds(0);
    }

    //and performs any feedback for pickup
    public void removeItem(Item item)
    {
        items.Remove(item);

        //feedback here
        //StartCoroutine("feedback", "used up: " + item.key);
        if (item.key.Contains("Key"))//puzzle pieces must be named "Something Piece
        {
            StartCoroutine("keyUsed", item.key);
        }
    }

    IEnumerator keyUsed(string name)
    {

        GameObject usedKey = keyUI.transform.Find(name).gameObject;
        Destroy(usedKey);
        yield return new WaitForSeconds(0);
    }

    //Checks to see if every required item's key exists in the player's items
    public bool checkContents(List<string> requirements)
    {
        foreach (string req in requirements)
        {
            foreach(Item i in items)
            {
                if(i.key == req)
                {
                    return true;
                }
            }
            /*if (!items.Contains(req.name))
            {
                return false;
            }*/
        }
        return false;
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