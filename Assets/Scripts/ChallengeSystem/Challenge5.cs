using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge5 : MonoBehaviour
{
    //El cuarto reto requiere montar a sala de conciertos con capacidad para 15 personas, entretenimiento y electrónica.
    [SerializeField] ChallengeBase chalbase;
    [SerializeField] ChallengePanel panel;
    [SerializeField] private int challengeIndex;

    
    // Start is called before the first frame update
    void Start()
    {
        chalbase.Calculate();
        if(chalbase.chairs >= 15)
        {
            if(chalbase.electronics >= 4)
            {
                if(chalbase.luxValue > 10)
                {
                    if(chalbase.bars > 3)
                    {
                        panel.ChallengePassed = true;
                        panel.rates[0] = "¡Espectacular! Tu valor de lujo es " + Mathf.Abs(chalbase.luxValue) + "." ;
                        panel.rates[1] = "Puedes sentar a " + chalbase.chairs + " personas.";
                        panel.rates[2] = "Tu valor de electrónica es " + chalbase.electronics + " y el de lujo, " + Mathf.Abs(chalbase.luxValue) + ".";
                        ChallengeSave.Instance.ChallengePased(challengeIndex);
                    }
                    else
                    {
                        panel.ChallengePassed = false;
                        panel.rates[0] = "Esta caseta no está muy bien equipada. Intenta colocar barras, guitarras y cosas así.";
                        panel.rates[1] = "Todo lo demás está bien.";
                        panel.rates[2] = "0";
                        //No hay suficientes elementos de bar
                    }
                }
                else
                {
                    panel.ChallengePassed = false;
                    panel.rates[0] = "Esta caseta se ve un poco simple. Necesita algo más de distinción.";
                    panel.rates[1] = "Vigila que no tengas nada que reduzca tu valor de lujo.";
                    panel.rates[2] = "0";
                    //No hay suficiente lujo
                }
            }
            else
            {
                panel.ChallengePassed = false;
                panel.rates[0] = "Así no se puede dar un concierto. ¡Necesitas más electrónica!";
                panel.rates[1] = "Al artista tiene que podérsele ver y oír bien.";
                panel.rates[2] = "0";
                //No hay suficiente electrónica
            }

        }
        else
        {
            panel.ChallengePassed = false;
            panel.rates[0] = "No hay suficiente capacidad. Necesitas más asientos.";
            panel.rates[1] = "0";
            panel.rates[2] = "0";
            //No hay suficientes sillas
        }
        

        panel.descriptiontext = "Una caseta para dar conciertos (15px).";

        panel.rates[3] = "0";
        panel.PanelSetter();
    }

}
