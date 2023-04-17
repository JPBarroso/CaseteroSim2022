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
        if(chalbase.rojo > 0)
        {
            panel.ChallengePassed = false;
            panel.rates[0] = "¡Rojo a la vista! ¡El cliente no quiere ese color!";
            panel.rates[1] = "¿Te has fijado en que puedes cambiar el color de las paredes?";

        }
        else
        {
            if (chalbase.verde >= 58)
            {
                panel.ChallengePassed = true;
                panel.rates[0] = "¡Todo verde! ¡Buen trabajo!";
                panel.rates[1] = "0";
                ChallengeSave.Instance.ChallengePased(challengeIndex);
            }
            else
            {
                panel.ChallengePassed = false;
                panel.rates[0] = "La caseta no es lo suficientemente verde. Necesitas algo más.";
                panel.rates[1] = "Las paredes también cuentan.";
            }
        }
        

        panel.descriptiontext = "Una caseta muy verde, ¡el rojo ni verlo!";

        panel.rates[2] = "0";
        panel.rates[3] = "0";
        panel.PanelSetter();
    }

}
