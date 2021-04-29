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
                Destroy(i.transform.parent.gameObject);
            }
            foreach(PatrolPointsController j in patrolPoints)
            {
                Destroy(j.gameObject);
            }
            foreach(PortalController k in portals)
            {
                Destroy(k.gameObject);
            }
            foreach(ElevatorMonsterController l in elem)
            {
                Destroy(l.transform.parent.gameObject);
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
