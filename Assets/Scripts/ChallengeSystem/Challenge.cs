using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour
{
    public enum ChallengeType { TIPO, LUJO, COLOR, PRESUPUESTO, }
    public enum Comparers {MAS,MENOS,}
    public ChallengeType tipoDesafio;
    public Furniture.FurnitureColor colorDesafio;
    public Furniture.FurnitureType tipoMueble;
    public Comparers comparador;
    public int valor;
    
}
