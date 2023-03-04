using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoWallcheck : MonoBehaviour
{
    public bool puedeCaminar, siguienteMovimientoAtaca;
    private int sujeto;
    public GameObject enemigo;

    void Start()
    {
        GameObject esteWallCheck = transform.parent.gameObject;
        sujeto = esteWallCheck.gameObject.layer;
    }
    void OnTriggerStay(Collider other)
     {
        switch(sujeto)
        {
            case 9: //enemigo
            if (other.gameObject.layer == LayerMask.NameToLayer("Suelo")) 
            {
                puedeCaminar = true;
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Pared") || other.gameObject.layer == LayerMask.NameToLayer("Enemigo"))
            {
                puedeCaminar = false;
            }
            break;

            case 7: //jugador
            
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemigo"))
            {
                puedeCaminar = true;
                siguienteMovimientoAtaca = true;
                enemigo = other.gameObject;
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Pared"))
            {
                puedeCaminar = false;
                siguienteMovimientoAtaca = false;
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Suelo")) 
            {
                puedeCaminar = true;
            }
            break;
        }
     }
}
