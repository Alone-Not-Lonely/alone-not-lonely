using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressionTracker : MonoBehaviour
{
    public List<string> alreadyVisited = new List<string>();

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(alreadyVisited.Contains(scene.name))
        {
            //load progress
            ImaginaryEntity[] imen = (ImaginaryEntity[])Resources.FindObjectsOfTypeAll(typeof(ImaginaryEntity));
            PatrolPointsController[] patrolPoints = (PatrolPointsController[])Resources.FindObjectsOfTypeAll(typeof(PatrolPointsController));
            PortalController[] portals = (PortalController[])Resources.FindObjectsOfTypeAll(typeof(PortalController));
            ElevatorMonsterController[] elem = (ElevatorMonsterController[])Resources.FindObjectsOfTypeAll(typeof(ElevatorMonsterController));
            foreach(ImaginaryEntity i in imen)
            {
                i.transform.parent.gameObject.SetActive(false);
            }
            foreach(PatrolPointsController j in patrolPoints)
            {
                j.gameObject.SetActive(false);
            }
            foreach(PortalController k in portals)
            {
                k.gameObject.SetActive(false);
            }
            foreach(ElevatorMonsterController l in elem)
            {
                l.transform.parent.gameObject.SetActive(false);
            }
        }
        else
        {
            //visiting now
            alreadyVisited.Add(scene.name);
        }
    }

    private void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
