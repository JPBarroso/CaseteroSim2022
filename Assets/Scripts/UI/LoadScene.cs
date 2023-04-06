using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{


    public void LoadNewGame(string sceneName)
    {
        ES3.DeleteFile(SaveAndLoadManager.FileName);
        LoadSceneByName(sceneName);
    }

    public void ContinueGame()
    {
        if (ES3.FileExists(SaveAndLoadManager.FileName))
        {
            LoadSceneByIndex(ES3.Load<int>("sceneIndex", SaveAndLoadManager.FileName));
        }
        
    }
    
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
    
    
}
