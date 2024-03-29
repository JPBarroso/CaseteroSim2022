using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChallengeChat : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI texto;
    [SerializeField] List<string> mensajes = new List<string>();
    [SerializeField] GameObject panel;
    int pos = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(aparecer());
        texto.text = mensajes[pos];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Avanzar()
    {
        pos++;
        if(pos >= mensajes.Count)
        {
            panel.SetActive(false);
        }
        else
        {
            texto.text = mensajes[pos];
        }
    }
    IEnumerator aparecer()
    {
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<Animator>().enabled = true;
    }
}
