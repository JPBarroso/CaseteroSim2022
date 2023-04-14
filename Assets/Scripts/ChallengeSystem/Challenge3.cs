using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge3 : MonoBehaviour
{
    //El tercer reto es artístico. Muy artístico. Tan artístico que o está activado Arte I y hay por encima de 10 puntos de arte o adiós.
    [SerializeField] ChallengeBase chalbase;
    [SerializeField] ChallengePanel panel;
    [SerializeField] private int challengeIndex;
    [SerializeField] GameObject configCorrecta;

    
    // Start is called before the first frame update
    void Start()
    {
        chalbase.Calculate();
        if (configCorrecta.activeInHierarchy)
        {
            if(chalbase.artValue >= 10)
            {
                panel.ChallengePassed = true;
                panel.rates[0] = "¡Bien hecho! ¡Has elegido estupendamente!";
                panel.rates[1] = "Tu valor de arte está en" + chalbase.artValue + ".";
            }
            else
            {
                panel.ChallengePassed = false;
                panel.rates[0] = "Necesitas algo más de mobiliario.";
                panel.rates[1] = "Esta vez, la secillez es tu enemiga";
                //No hay suficiente arte
            }
        }
        else
        {
            panel.ChallengePassed = false;
            panel.rates[0] = "La caseta es muy simple.";
            panel.rates[1] = "Necesitamos algo más elaborado";
            //La caseta no es la correcta
        }
        

        panel.descriptiontext = "Arte por todas partes";

        panel.rates[2] = "0";
        panel.rates[3] = "0";
        panel.PanelSetter();
    }

}
