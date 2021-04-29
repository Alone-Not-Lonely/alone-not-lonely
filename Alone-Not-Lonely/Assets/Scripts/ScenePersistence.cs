using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersistence : MonoBehaviour
{
    private void Awake() {
        ScenePersistence[] objs = (ScenePersistence[])Resources.FindObjectsOfTypeAll(typeof(ScenePersistence));
        Debug.Log(this.name);

        if (objs.Length > 2)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(this.gameObject);//Keep persistent
    }
}
