using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge4 : MonoBehaviour
{
    //El cuarto reto requiere montar un bar.
    [SerializeField] ChallengeBase chalbase;
    [SerializeField] ChallengePanel panel;
    [SerializeField] private int challengeIndex;

    
    // Start is called before the first frame update
    void Start()
    {
        chalbase.Calculate();
       if(chalbase.bars >= 5)
        {
            if(chalbase.chairs >= 12)
            {
                if(chalbase.tables > 3)
                {
                    panel.ChallengePassed = true;
                    panel.rates[0] = "Tienes " + chalbase.bars + " artículos de bar. ¡Genial!";
                    panel.rates[1] = "Además, puedes sentar a " + chalbase.chairs + " personas.";
                    panel.rates[2] = "Y lo más importante: tienes " + chalbase.tables + " mesas preparadas";
                }
                else
                {
                    panel.ChallengePassed = false;
                    panel.rates[0] = "Tu bar está bien equipado.";
                    panel.rates[1] = "Además, puedes sentar a " + chalbase.chairs + " personas.";
                    panel.rates[2] = "Sin embargo, necesitas al menos una mesa para cada cuatro sillas.";
                    //No hay suficientes mesas
                }
            }
            else
            {
                panel.ChallengePassed = false;
                panel.rates[0] = "Tu bar está bien equipado.";
                panel.rates[1] = "Pero necesitas más espacio para sentar a gente.";
                panel.rates[2] = "Ahora mismo sólo puedes sentar a " + chalbase.chairs + " personas.";
                //No hay suficientes sillas
            }

        }
        else
        {
            panel.ChallengePassed = false;
            panel.rates[0] = "Como bar, deja un poco que desear.";
            panel.rates[1] = "Necesitas algo más de mobiliario para servir y entretener.";
            panel.rates[2] = "Y no olvides que además necesitas mesas y sillas.";
            //No hay suficientes artículos de bar
        }
        

        panel.descriptiontext = "Un bar que es una barbaridad (12px).";

        panel.rates[3] = "0";
        panel.PanelSetter();
    }

}
