 using System;
 using UnityEngine;
 [ExecuteInEditMode]
 public class MatrixSetup : MonoBehaviour
 {
     private void OnEnable ()
     {
         Camera.onPreCull += SetupMatrices;
     }
 
     private void OnDisable ()
     {
         Camera.onPreCull -= SetupMatrices;
     }
 
     private void SetupMatrices (Camera cam)
     {
         //Just make sure you have a custom matrix property on your material called _InvProjectionMatrix and use that instead
         Shader.SetGlobalMatrix ("_InvProjectionMatrix", cam.projectionMatrix.inverse);
     }
 }
