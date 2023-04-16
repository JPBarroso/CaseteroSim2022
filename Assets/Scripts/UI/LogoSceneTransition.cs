using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoSceneTransition : MonoBehaviour
{

    private string sceneToLoad;
    private int sceneIndexToLoad;
    private bool tPlayed;
    [SerializeField] GameObject fader;
    private Animator anim;
    [SerializeField] GameObject tutotext;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        anim = fader.GetComponent<Animator>();
        if (TutorialHasBeenPlayed())
        {
            tutotext.SetActive(false);
        }
        else
        {
            tutotext.SetActive(true);
        }
        yield return new WaitForSeconds(3f);
        SetSceneName();
        //SceneManager.LoadScene(sceneToLoad);
        StartCoroutine(waiter(sceneIndexToLoad));
    }

    private void SetSceneName()
    {
        if (TutorialHasBeenPlayed())
        {
            sceneToLoad = "Menu";
            sceneIndexToLoad = 3;
        }
        else
        {
            sceneToLoad = "Tutorial";
            sceneIndexToLoad = 1;
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
    IEnumerator waiter(int escena)
    {
        anim.SetTrigger("Fade");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(escena);
    }

}
