using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentanasController : MonoBehaviour
{
	
    [SerializeField] GameObject MenuPausa;



    //Controladores de escenas
    public void ventana_Pausa_ON()
    {
        //inicia la ventana del menu de pausa (desactivada)
        MenuPausa.SetActive(true);

    }

    public void ventana_Pausa_OF()
    {
        //inicia la ventana del menu de pausa (desactivada)
        MenuPausa.SetActive(false);

    }

    public void ventana_Inventario()
    {
        //Inicia la ventana de inventario (escena)
    }

    public void Ventana_Ataque()
    {
        //Inicia la ventana de ataque (escena)
    }

    public void Ventana_Exploracion()
    {
        //Inicia la ventana de exploracion (depende de la escena)
    }

}