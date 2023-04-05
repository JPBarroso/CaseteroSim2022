using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    [SerializeField] GameObject fader;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = fader.GetComponent<Animator>();
        if (!fader)
        {
            fader.SetActive(true);
        }
    }

    public void CambiaEscena(int escena)
    {
        StartCoroutine(waiter(escena));
    }

    IEnumerator waiter(int escena)
    {
        anim.SetTrigger("Fade");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(escena);
    }
}
