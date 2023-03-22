 using UnityEngine;
// using System.Collections;
 
 public class SistemaDeTurnos : MonoBehaviour
 {

    public static SistemaDeTurnos Instance { get; private set; }

     public bool turnoPlayer = true;
     public bool puedeAgarrarCarta = true;

 
    private void Awake() 
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


     public bool Get_AgarrarCarta ()
     {
        return puedeAgarrarCarta;
     }

     public void Set_AgarrarCartaFalse()
     {
        puedeAgarrarCarta = false;
     }

     /*
        if(AgarrarCarta)
        {
            //agarra carta
            puedeAgarrarCarta = false;
        }
     */

     public bool Get_TurnoPlayer ()
     {
        if(turnoPlayer)
        {
            Debug.Log("el turno es: JUGADOR");
        }
        else
        {
            Debug.Log("el turno es: ENEMIGO");

        }
        
        return turnoPlayer;
     }

     public void TerminarTurnoPlayer()
     {
        turnoPlayer = false;
        //acciona la IA enemiga
     }

     

     // mientras el turno sea true el juegador va a poder jugar sus cartas, si es una carta de combate el turno termina automaticamente
     //si no es carta de combate no pasa nada.
     //solo puede agarrar una carta por turno.
     //un boton acciona para terminar el turno, al terminarlo el enemigo juega su turno y acciona la IA,
/*     private void Update() 
     {
        if(!turnoPlayer)
        {
            scriptMano.DisableCards();
        }
     }*/


     /// aca va la IA y sus cosas

     //Una vez que termina la IA acciona el movimiento del player: 
     public void TerminarTurnoEnemigo()
     {
        turnoPlayer = true;
        puedeAgarrarCarta = true;


     }
 }