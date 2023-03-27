 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
 public class SistemaDeTurnos : MonoBehaviour
 {

    public static SistemaDeTurnos Instance { get; private set; }

     public bool turnoPlayer = true;
     public bool puedeAgarrarCarta = true;
     private int dañoAlEnemigo;
     [SerializeField] GameObject handUtils;


 
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

     public bool Get_TurnoPlayer ()
     {

        return turnoPlayer;
     }


    // sistema de combate puro
    // se tira la carta, es carta de combate?
    // si es asi, cuanto quita
    // le quita daño al enemigo


    public int[] choqueDeDaños =  new int[2];
    int temp = 0;

    public void DañoAlEnemigo(GameObject card)
    {
        if (temp == 0)
        {
            //tira la carta, es daño al enemigo (dependiendo de quien sea el que tire, se que esta mal explicado pero asi escaló la funcion)
            //Debug.Log("se tiró la carta:" + card.name);
            dañoAlEnemigo = card.GetComponent<Tools.UI.Card.UiCardComponent>().Get_Daño();
    
            choqueDeDaños[0] = dañoAlEnemigo;
    
            Debug.Log("El daño que hace es: " + dañoAlEnemigo);
    
            //hay que añadir el daño a un texto y que se vea, ademas un texto en la propia carta
            //cambiar todos los gameObjects porque luego desaparecen y crean errores
            temp++;
        }
        else if (temp == 1)
        {
            temp = 0;
            dañoAlEnemigo = card.GetComponent<Tools.UI.Card.UiCardComponent>().Get_Daño();
            choqueDeDaños[1] = dañoAlEnemigo;
            Debug.Log("El daño que hace es: " + dañoAlEnemigo);

        }
    }
    public int Get_DañoCarta()
    {
        return dañoAlEnemigo;
    }



    private void ProcesarDaño ()
    {
        if (valorTurno == 0)
        {
            if (choqueDeDaños[0] > choqueDeDaños[1])
            {
    
                int dañoTotal = choqueDeDaños[0] - choqueDeDaños[1];
                Debug.Log("ganó el player, hizo: " + dañoTotal + " de daño");
                SistemaDeVida.Instance.HacerDañoEnemigo(dañoTotal);
                
            }
            else if(choqueDeDaños[0] < choqueDeDaños[1])
            {
                int dañoTotal = choqueDeDaños[1] - choqueDeDaños[0];
                Debug.Log("ganó el Enemigo, hizo: " + dañoTotal + " de daño");
                SistemaDeVida.Instance.HacerDañoPlayer(dañoTotal);
            }
            else if(choqueDeDaños[0] == choqueDeDaños[1])
            {
                Debug.Log("Hicieron el mismo daño, se repelieron");    
            }
        }
        else if (valorTurno == 1)
        {
            if (choqueDeDaños[0] > choqueDeDaños[1])
            {
    
                int dañoTotal = choqueDeDaños[0] - choqueDeDaños[1];
                Debug.Log("ganó el Enemigo, hizo: " + dañoTotal + " de daño");
                SistemaDeVida.Instance.HacerDañoPlayer(dañoTotal);
                
            }
            else if(choqueDeDaños[0] < choqueDeDaños[1])
            {
                int dañoTotal = choqueDeDaños[1] - choqueDeDaños[0];
                Debug.Log("ganó el Player, hizo: " + dañoTotal + " de daño");
                SistemaDeVida.Instance.HacerDañoEnemigo(dañoTotal);
            }
            else if(choqueDeDaños[0] == choqueDeDaños[1])
            {
                Debug.Log("Hicieron el mismo daño, se repelieron");    
            }
        }
    }

    
    //Intercalar turnos



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



    int valorTurno = 0;
    bool estadoIA;

    public void TerminarTurnoEnemigo()
    {
        if (valorTurno == 0)
        {
            StartCoroutine(AnalizarTurnoE());
        }
        else if (valorTurno == 1)
        {
            Set_IAEnemigo(false);
            turnoPlayer = true;
            puedeAgarrarCarta = true;
        }
    }
    public void TerminarTurnoPlayer()
    {
        if (valorTurno == 0)
        {
            turnoPlayer = false;
            Set_IAEnemigo(true);
        }
        else if (valorTurno == 1)
        {
            StartCoroutine(AnalizarTurnoP());
            //turnoPlayer = false;
            valorTurno = 0;
            //Set_IAEnemigo(true);
        }
    }
    //valorTurno = 0 arranca el jugador, termina el enemigo
    //valorTurno = 1 arranca el enemigo, termina el jugador

    private IEnumerator AnalizarTurnoE()
    {

            ProcesarDaño();
            yield return new WaitForSeconds(1f);
            Set_IAEnemigo(false);
            valorTurno = 1;
            yield return new WaitForSeconds(0.5f);
            Set_IAEnemigo(true);
            yield return null;
    }

    private IEnumerator AnalizarTurnoP()
    {
            ProcesarDaño();
            yield return new WaitForSeconds(2f);
            turnoPlayer = false;
            yield return new WaitForSeconds(2f);
            turnoPlayer = true;
            puedeAgarrarCarta = true;
            handUtils.GetComponent<Tools.UI.Card.UiPlayerHand>().EnableCards(); 

            yield return null;
    }


    public void Set_IAEnemigo(bool estado)
    {
        estadoIA = estado;
    }

    public bool Get_IAEnemigo()
    {
        return estadoIA;
    }

    public int Get_ValorTurno()
    {
        return valorTurno;
    }

    //hacer el sistema de ABBA, turno player enemigo, enemigo player, player enemigo...
    //Sistema de vida



 }