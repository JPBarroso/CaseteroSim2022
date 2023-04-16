using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMusicTuto : MonoBehaviour
{
    public static BGMusicTuto instance1;
    private void Awake()
    {
        SceneManager.activeSceneChanged += SceneDestruction;

        if (instance1 != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance1 = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }
    
    
    void SceneDestruction(Scene actual, Scene otra)
    {
        Scene thisscene;
        thisscene = SceneManager.GetActiveScene();
        if (thisscene.buildIndex == 1 || thisscene.buildIndex == 2)
        {
            return;
        }
        else
        {
            SceneManager.activeSceneChanged -= SceneDestruction;
            Destroy(this.gameObject);
        }
        
    }
}
