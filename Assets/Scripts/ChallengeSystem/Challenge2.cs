using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge2 : MonoBehaviour
{
    //El segundo reto requiere TODO VERDE. NADA DE ROJO.
    [SerializeField] ChallengeBase chalbase;
    [SerializeField] ChallengePanel panel;
    [SerializeField] private int challengeIndex;

    
    // Start is called before the first frame update
    void Start()
    {
        chalbase.Calculate();
        if(chalbase.verde >= 60 && chalbase.rojo <= 0)
        {
            panel.ChallengePassed = true;
            panel.rates[0] = "¡Todo verde! ¡Buen trabajo!";
            panel.rates[1] = "Buen trabajo con esas paredes";
        }
        else
        {
            panel.ChallengePassed = false;
            panel.rates[0] = "El color es incorrecto. Podrías haberlo hecho mejor.";
            panel.rates[1] = "Las paredes también cuentan.";
        }

        panel.descriptiontext = "Una caseta muy verde, ¡el rojo ni verlo!";

        panel.rates[2] = "0";
        panel.rates[3] = "0";
        panel.PanelSetter();
    }

}
