using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMusic : MonoBehaviour
{
    public static BGMusic instance;
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
        Debug.Log(thisscene.buildIndex);
        if(thisscene.buildIndex != 3 && thisscene.buildIndex != 6)
        {
            Destroy(this.gameObject);
        }
    }
}
