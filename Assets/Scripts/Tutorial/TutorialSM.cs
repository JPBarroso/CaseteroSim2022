using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialSM : MonoBehaviour
{
    public List<string> chat = new List<string>();
    public List<GameObject> marcos = new List<GameObject>();
    [SerializeField] TextMeshProUGUI texto;
    [SerializeField] GameObject chatbox;
    [SerializeField] SceneTransitioner trans;

    int pos=0;
    public int casetatuto;

    public void Avanzar()
    {
        pos++;
        if(pos > 0)
        {
           if(marcos[pos - 1] != chatbox)
            {
                marcos[pos - 1].SetActive(false);
            }
            
        }
        else if(pos > chat.Count)
        {
            trans.CambiaEscena(casetatuto);
        }
        if (!marcos[pos].activeInHierarchy)
        {
            marcos[pos].SetActive(true);
        }
        texto.text = chat[pos];
    }
    

}
