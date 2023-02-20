using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeSystem : MonoBehaviour
{
    public Challenge[] desafios;
    public List<FurnitureData> muebles = new List<FurnitureData>(); //Hay que añadir objetos a esta lista según se compren.
    // Start is called before the first frame update
    void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "compra");
    }
    public void compra()
    {
        //Se añaden datos del mueble a la lista.
    }
    
    bool LeerDesafioPresupuesto(Challenge desafio) //Vamos a leer lo que el jugador se ha gastado
    {
        float totalgastado = 0;
        foreach(FurnitureData item in muebles) //Por cada mueble en la lista...
        {
            totalgastado = totalgastado + item.Data.furniturePrice; //Añadimos su valor al conteo
        }
        if(desafio.comparador == Challenge.Comparers.MAS) //En este caso, el jugador tiene que haberse gastado COMO MÍNIMO una cantidad
        {
            if(totalgastado >= desafio.valor) //Por lo que si se ha gastado esa cantidad o más...
            {
                return true; //Ha completado el desafío
            }
            else //Y si no...
            {
                return false; //Lo ha fallado
            }
        }
        else //Ahora al revés
        {
            if(totalgastado < desafio.valor) //Si se ha gastado menos...
            {
                return true; //Ha sabido ahorrar, pasa el desafío
            }
            else //Y si no...
            {
                return false; //Haber estudiao.
            }
        }
    }
    bool LeerDesafioTipo(Challenge desafio)
    {
        int mesas = 0;
        int sillas = 0;
        int electronica = 0;
        int bares = 0;
        foreach(FurnitureData item in muebles)
        {
            if(item.Data.typeEnum == Furniture.FurnitureType.CHAIR)
            {
                sillas++;
            }
            else if(item.Data.typeEnum == Furniture.FurnitureType.TABLE)
            {
                mesas++;
            }
            else if(item.Data.typeEnum == Furniture.FurnitureType.BAR)
            {
                bares++;
            }
            else if(item.Data.typeEnum == Furniture.FurnitureType.ELECTRONICS)
            {
                electronica++;
            }
        } //Leemos la lista y sumamos por cada tipo de objeto.
        //Ahora vendría el if/else grande.
        return true; // por el momento
    }


}
