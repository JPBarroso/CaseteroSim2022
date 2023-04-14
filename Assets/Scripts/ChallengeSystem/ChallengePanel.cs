using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChallengePanel : MonoBehaviour
{
    [Header("Pasado o no")]
    public bool ChallengePassed = false;
    [SerializeField] TextMeshProUGUI completion;
    [SerializeField] GameObject shareButton,retryButton;

    [Header("Descripcion")]
    public string descriptiontext;
    [SerializeField] TextMeshProUGUI TMdescription;

    [Header("Condiciones")]
    public string[] rates;
    [SerializeField] TextMeshProUGUI[] ratings;
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void PanelSetter()
    {
        ButtonSetter(ChallengePassed);
        DescriptionSetter(descriptiontext);
        RatingSetter(rates);
    }

    public void ButtonSetter(bool pass)
    {
        if (pass)
        {
            retryButton.SetActive(false);
            completion.text = "Desaf�o completado!";
        }
        else
        {
            shareButton.SetActive(false);
            completion.text = "�Desaf�o fracasado!";
        }
    }
    public void DescriptionSetter(string description)
    {
        TMdescription.text = description;
    }

    public void RatingSetter(string[] ratingstrings)
    {
        for (int i = 0; i < ratingstrings.Length; i++)
        {
            if(ratingstrings[i] == "0") //Al llamar a la funci�n, ponemos un 0 en las frases que no necesitemos
            {
                ratings[i].gameObject.SetActive(false);
            }
            else
            {
                ratings[i].text = ratingstrings[i];
            }
        }
    }
}

