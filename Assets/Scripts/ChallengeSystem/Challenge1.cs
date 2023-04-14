using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge1 : MonoBehaviour
{
    //El primer reto requiere lujo <=0 y asientos >=10. Vamos a referenciar al calculador y al panel.
    [SerializeField] ChallengeBase chalbase;
    [SerializeField] ChallengePanel panel;
    [SerializeField] private int challengeIndex;

    
    // Start is called before the first frame update
    void Start()
    {
        chalbase.Calculate();
        if(chalbase.chairs >=10 && chalbase.luxValue <= 0)
        {
            panel.ChallengePassed = true;
            panel.rates[0] = "Tu valor de sencillez es de " + Mathf.Abs(chalbase.luxValue) + ". �Bien hecho!";
            
            //Guardamos el numero de desafio para desbloquearlo si lo completamos
            ChallengeSave.Instance.ChallengePased(challengeIndex);
        }
        else
        {
            panel.ChallengePassed = false;
            panel.rates[0] = "Tu valor de lujo es de " + Mathf.Abs(chalbase.luxValue) + ". Puedes hacerlo mejor.";
        }
        panel.descriptiontext = "Una caseta sencilla, que siente a 10 personas como m�nimo";
        panel.rates[1] = "Y puedes sentar a " + chalbase.chairs + " personas.";
        panel.rates[2] = "0";
        panel.rates[3] = "0";
        panel.PanelSetter();
    }

}
