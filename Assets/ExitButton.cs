using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour {

    public void ExitNow()
    {
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

}