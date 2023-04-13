using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManagePlayed : MonoBehaviour
{


    private bool tutorialHasBeenPlayed;
    public static string TutoFileName => "SaveTutorialFile" + ".es3";
    
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene current)
    {
        tutorialHasBeenPlayed = true;
        ES3.Save("TutorialPlayed", tutorialHasBeenPlayed, TutoFileName);
    }
    
}
