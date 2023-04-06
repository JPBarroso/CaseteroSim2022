using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    [SerializeField] GameObject fader;
    Animator anim;

    //Me estoy dando cuenta que igual necesitamos otro script para toda la logica de crear nuevo juego
    //Pero estoy triste
    [SerializeField] private GameObject securityPanel;
    private int lastIndex;
    // Start is called before the first frame update
    void Start()
    {
        anim = fader.GetComponent<Animator>();
        if (!fader.activeInHierarchy)
        {
            fader.SetActive(true);
        }
    }

    public void CambiaEscena(int escena)//Llamar a la corr y cambiar escena
    {
        StartCoroutine(waiter(escena));
    }

    public void ChangeToNewScene(int scene)//Si no existen datos, por seguridad borramos y llamamos cambio escena
    {
        lastIndex = scene;//Este int lo guardo para el security panel
        if (!ES3.FileExists(SaveAndLoadManager.FileName))
        {
            SetNewGame();
            StartCoroutine(waiter(scene));
        }
        else
        {
            securityPanel.SetActive(true);
        }

    }

    //Metodo que ponemos al panel de seguridad
    public void ContinueToNewGameInSecurityPanel()//Si tenemos datos guardados llamamos al sec panel y podemos init caseta desde aqui
    {
        SetNewGame();
        StartCoroutine(waiter(lastIndex));
    }
    
    public void ContinueGame()//Metodo para continuar
    {
        if (ES3.FileExists(SaveAndLoadManager.FileName))
        {
            int sceneIndex = ES3.Load<int>("sceneIndex", SaveAndLoadManager.FileName);
            StartCoroutine(waiter(sceneIndex));
        }
        
    }

    IEnumerator waiter(int escena)
    {
        anim.SetTrigger("Fade");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(escena);
    }

    //Esto no debería de estar aqui, no tiene sentido pero como estamos usando este script para
    //cambiar de escenas voy a dejarlo por aqui por ahora. Esto iria cuando cargamos escenas nuevas
    private void SetNewGame()
    {
        ES3.DeleteFile(SaveAndLoadManager.FileName);
    }
}
