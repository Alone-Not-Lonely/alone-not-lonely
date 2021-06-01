using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeTableSpritesManager : MonoBehaviour
{
    GameObject[] puzzlePiecesOnTable;
    Player _player = Player.instance;
    PlayerInventory pIn;

    Vector3 desiredEndPosition = new Vector3(-0.730000019f,3.31999993f,-7.5f);
    AudioSource completeSound;

    public static CoffeeTableSpritesManager instance;
    private void Awake() {
        CoffeeTableSpritesManager[] objs = (CoffeeTableSpritesManager[])FindObjectsOfType<CoffeeTableSpritesManager>();

        if (objs.Length > 1)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            //DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    void Start()
    {
        puzzlePiecesOnTable = new GameObject[12];
        int count = 0;
        foreach(Transform child in this.transform)
        {
            if(child.gameObject.GetComponent<Image>())
            {
                puzzlePiecesOnTable[count] = child.gameObject;
                count++;
            }
        }
        pIn = PlayerInventory.instance;
        //UpdatePuzzleState();
        int ctr = 0;
        foreach(GameObject piece in puzzlePiecesOnTable)
        {
            if(FinalPuzzleManager.instance.puzzlePiecesOnTable[ctr].GetComponent<DragDrop>().piecePlaced)
            {
                piece.SetActive(true);
            }
            else
            {
                piece.SetActive(false);
            }
            ctr++;
        }
    }

    public void UpdatePuzzleState()
    {
        int ctr = 0;
        foreach(GameObject piece in puzzlePiecesOnTable)
        {
            if(FinalPuzzleManager.instance.puzzlePiecesOnTable[ctr].GetComponent<DragDrop>().piecePlaced)
            {
                piece.SetActive(true);
            }
            else
            {
                piece.SetActive(false);
            }
            ctr++;
        }
    }
}
