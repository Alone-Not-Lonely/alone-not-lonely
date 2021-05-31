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
    public static FinalPuzzleManager instance;
    private void Awake() {
        FinalPuzzleManager[] objs = (FinalPuzzleManager[])FindObjectsOfType<FinalPuzzleManager>();

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
            if(child.gameObject.GetComponent<PuzzlePiece>())
            {
                puzzlePiecesOnTable[count] = child.gameObject;
                count++;
            }
        }
        pIn = PlayerInventory.instance;
        //UpdatePuzzleState();
        foreach(GameObject piece in puzzlePiecesOnTable)
        {
            piece.SetActive(false);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "GroundFloor")
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

    private void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void UpdatePuzzleState()
    {
        int ctr = 0;
        foreach(GameObject piece in puzzlePiecesOnTable)
        {
            //Debug.Log(pIn);
            if(pIn.puzzlePieces.Contains(piece.GetComponent<PuzzlePiece>().ID))
            {
                piece.SetActive(true);
                if(piece.GetComponent<DragDrop>().piecePlaced)
                {
                    ctr ++;
                }
            }
            else
            {
                piece.SetActive(false);
            }
        }
        if(ctr == 12)
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
