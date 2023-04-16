using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMusicTuto : MonoBehaviour
{
    public static BGMusicTuto instance;
    private void Awake()
    {
        SceneManager.activeSceneChanged += SceneDestruction;
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }

        
    }
    void SceneDestruction(Scene actual, Scene previa)
    {
        Scene thisscene;
        thisscene = SceneManager.GetActiveScene();
        if (thisscene.buildIndex == 1 || thisscene.buildIndex == 2)
        {
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
}
