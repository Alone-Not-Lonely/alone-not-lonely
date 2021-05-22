using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImaginaryEntity : MonoBehaviour
{
    private Transform pT;
    private SpriteRenderer _render;
    public float visDist = 5f;

    private void Start()
    {
        Debug.Log(Player.instance + " Player");
        pT = Player.instance.transform;
        _render = GetComponent<SpriteRenderer>();
        _render.color = Color.clear;
    }
    private void Update()
    {

        float dist = Vector3.Distance(transform.position, pT.position);
        float closeness = ((visDist / (dist)));
        _render.color = new Color(closeness,closeness,closeness,closeness);

    }

    // Start is called before the first frame update
    public void gannonVoiceDie()
    {
        Destroy(this.gameObject);
    }

}
