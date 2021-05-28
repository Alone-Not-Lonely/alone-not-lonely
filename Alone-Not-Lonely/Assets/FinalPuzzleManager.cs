using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPuzzleManager : MonoBehaviour
{
    GameObject[] puzzlePiecesOnTable;
    Player _player = Player.instance;
    PlayerInventory pIn;

    Vector3 desiredEndPosition = new Vector3(-0.730000019f,3.31999993f,-7.5f);
    void Start()
    {
        puzzlePiecesOnTable = new GameObject[12];
        int count = 0;
        foreach(Transform child in this.transform)
        {
            puzzlePiecesOnTable[count] = child.gameObject;
            count++;
        }
        pIn = PlayerInventory.instance;
        //UpdatePuzzleState();
        foreach(GameObject piece in puzzlePiecesOnTable)
        {
            piece.SetActive(false);
        }
    }

    public void UpdatePuzzleState()
    {
        foreach(GameObject piece in puzzlePiecesOnTable)
        {
            //Debug.Log(pIn);
            if(pIn.puzzlePieces.Contains(piece.GetComponent<PuzzlePiece>().ID))
            {
                piece.SetActive(true);
            }
            else
            {
                piece.SetActive(false);
            }
        }
        if(pIn.puzzlePieces.Count == 12)
        {
            StartCoroutine("DramaticZoomOut");
        }
    }

    IEnumerator DramaticZoomOut()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("EndCutscene");
    }

    
}
