using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpriteAnimatorEditor : Editor
{
    [UnityEditor.MenuItem("2DAnimator/Create Anim")]
    public static void CreateAnim ()
    {
        AnimationClip clip = new AnimationClip();
        clip.SetCurve("", typeof(Transform), "position.x", AnimationCurve.EaseInOut(0, 0, 2, 10));
        clip.SetCurve("", typeof(Transform), "position.y", AnimationCurve.EaseInOut(0, 10, 2, 0));
        clip.SetCurve("", typeof(Transform), "position.z", AnimationCurve.EaseInOut(0, 5, 2, 2));
        AssetDatabase.CreateAsset(clip, "Assets/Test.anim");
    }

}
