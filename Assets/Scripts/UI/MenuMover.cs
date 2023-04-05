using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMover : MonoBehaviour
{
    [SerializeField] GameObject menu;
    Animator anim;
    GraphicRaycaster ray;
    // Start is called before the first frame update
    void Start()
    {
        ray = this.GetComponent<GraphicRaycaster>();
        anim = menu.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void nuevaCaseta()
    {
        anim.SetTrigger("Nuevo");
    }

    public void volver()
    {
        anim.SetTrigger("Volver");
    }
}
