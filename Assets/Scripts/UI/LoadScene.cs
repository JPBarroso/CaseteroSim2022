using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{


    public void LoadNewGame(string sceneName)
    {
        ES3.DeleteFile(SaveAndLoadManager.FileName);
        LoadSceneByName(sceneName);
    }

    public void ContinueGame(string sceneName)
    {
        if (ES3.FileExists(SaveAndLoadManager.FileName))
        {
            LoadSceneByName(sceneName);
        }
        
    }
    
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
    
    
}
