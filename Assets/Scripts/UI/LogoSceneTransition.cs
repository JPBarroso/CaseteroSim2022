using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoSceneTransition : MonoBehaviour
{

    private string sceneToLoad;
    private bool tPlayed;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        SetSceneName();
        SceneManager.LoadScene(sceneToLoad);

    }

    private void SetSceneName()
    {
        if (TutorialHasBeenPlayed())
        {
            sceneToLoad = "Menu";
        }
        else
        {
            sceneToLoad = "Tutorial";
        }
    }
    
    private bool TutorialHasBeenPlayed()
    {
        if (ES3.FileExists(TutorialManagePlayed.TutoFileName))
        {
            tPlayed = ES3.Load<bool>("TutorialPlayed", TutorialManagePlayed.TutoFileName);
        }
        else
        {
            tPlayed = false;
        }

        return tPlayed;
    }
    
}
