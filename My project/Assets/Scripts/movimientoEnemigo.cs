using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoEnemigo : MonoBehaviour
{
    public bool movimientoInstantaneo, estaRotando, estaCaminando;
    public float velocidadRotacion = 350f, velocidadMovimiento, tiempoRotacion, anguloRotacion, distanciaMovimiento;
    public movimientoWallcheck posicionFinal;
    public contadorDePasos contadorPasos;
    public float timer = 3.0f; // tiempo en segundos
    
    public float intervalo = 0.02f; // intervalo de tiempo entre movimientos

    public bool enemigoPuedeRotar;// variable para saber si se está contando
    private int layerJugador, diferenciaDePasos;
    private string estadoBusqueda; 
    private bool siguientePasoMueve;


    // Start is called before the first frame update
    void Start()
    {
        siguientePasoMueve = false;
        estadoBusqueda = "Rondando";
        enemigoPuedeRotar = true;
        GameObject go = GameObject.Find("ContadorDePasos");
        contadorPasos = (contadorDePasos) go.GetComponent(typeof(contadorDePasos));
        layerJugador = LayerMask.NameToLayer("Jugador");
    }

    // Update is called once per frame
    void Update()
    {


    switch(estadoBusqueda)
    {

        //Modo de caminata libre del personaje, ronda por el mapa hasta dar con el jugador.
        case "Rondando":
        //sistema de deteccion del jugador
        RaycastHit hit;
        if (!estaRotando && (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)))
        {
        //var right45 = (transform.forward + transform.right).normalized;
        //var left45 = (transform.forward - transform.right).normalized;
            if (hit.transform.gameObject.layer == layerJugador) 
            {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    //Debug.DrawRay(transform.position, right45 * hit.distance, Color.yellow);
                    //Debug.DrawRay(transform.position, left45 * hit.distance, Color.yellow);
                    Debug.Log("Te encontre!!");
                    Debug.Log(Mathf.Ceil(hit.distance));
                    diferenciaDePasos = contadorPasos.pasosActuales;
                    estadoBusqueda = "Detectado";
            } 
        }
        
        //sistema de caminata del enemigo
        //rotacion
        
        if (enemigoPuedeRotar && posicionFinal.puedeCaminar) //Que el enemigo aguarde x segundos antes de girar en caso de que tenga via libre para caminar.
        {
            timer -= Time.deltaTime; // restamos el tiempo transcurrido desde el último frame
            if (timer <= 0.0f && !estaCaminando) 
            {
                enemigoPuedeRotar = false; // detenemos el contador
                timer= 8.0f;
                int randomNumber = Random.Range(0, 2); // genera un número aleatorio entre 0 y 1 (incluyendo ambos)
                if (randomNumber == 0) 
                {
                    estaRotando = true;
                    StartCoroutine(RotarJugador(90));
                    enemigoPuedeRotar = true;// arrancamos el contador
                }
                else 
                {
                    estaRotando = true;
                    StartCoroutine(RotarJugador(-90));
                    enemigoPuedeRotar = true;// arrancamos el contador
                }
            }
            else if(timer <= 0.0f && estaCaminando)
            {
                timer = 3.0f;
            }
        }
        else if (enemigoPuedeRotar && !posicionFinal.puedeCaminar && !estaCaminando && !estaRotando)  //Que el enemigo gire inmediatamente si encuentra una pared en su camino.
        {
            enemigoPuedeRotar = false; // detenemos el contador
            timer= 5.0f;
            int randomNumber = Random.Range(0, 2); // genera un número aleatorio entre 0 y 1 (incluyendo ambos)
            if (randomNumber == 0) 
            {
                estaRotando = true;
                StartCoroutine(RotarJugador(90));
                enemigoPuedeRotar = true;// arrancamos el contador
            }
            else 
            {
                estaRotando = true;
                StartCoroutine(RotarJugador(-90));
                enemigoPuedeRotar = true;// arrancamos el contador
            }
        }
        //caminata hacia adelante
        if (contadorPasos.enemigosSeMueven && !estaCaminando && posicionFinal.puedeCaminar && !estaRotando)
        {
            if(movimientoInstantaneo)
            {
                transform.Translate(Vector3.forward * distanciaMovimiento);
            }
            else
            {
                int randomNumber = Random.Range(0, 100);
                if (randomNumber < 80) 
                {
                    StartCoroutine(MoverEnemigo());
                }
                else
                {
                    siguientePasoMueve = true; 
                }
            }
            timer=5.0f;
            enemigoPuedeRotar = true;// arrancamos el contador
        }
        if (siguientePasoMueve && contadorPasos.pasosActuales == 1 && !estaRotando)
        {
            siguientePasoMueve = false;
            StartCoroutine(MoverEnemigo());
        }

        break;//Aqui termina este CASE "Rondando"
        

        //El enemigo detecto al jugador, ajusta sus funciones para pasar al modo de caza.
        case "Detectado":
        Debug.Log("Modo deteccion!!");
        if(contadorPasos.pasosActuales != diferenciaDePasos)
        {
            if(posicionFinal.puedeCaminar)
            {
                StartCoroutine(MoverEnemigo());
                Debug.Log("Cambio estado");
                estadoBusqueda = "Rondando";
            }
            else
            {
                Debug.Log("Cambio estado");
                estadoBusqueda = "Rondando";
            }
        }
        break;//Aqui termina este CASE "Detectado"

        //El enemigo empieza a moverse hasta el jugador, intentando alcanzarlo para iniciar un combate.
        case "Cazando":
        break;

        case null:
        Debug.Log("Algo fallo en el case switch de enemigos.");
        break;
    }
         
    }

    IEnumerator MoverEnemigo() 
    {
    estaCaminando = true; // el objeto se está moviendo
    float distanciaRestante = distanciaMovimiento; // distancia restante que debe moverse
    while (distanciaRestante > 0.0f) { // mientras todavía quede distancia por mover
        float distanciaActual = Mathf.Min(velocidadMovimiento * intervalo, distanciaRestante); // calcula la distancia que se debe mover en este intervalo
        transform.Translate(Vector3.forward * distanciaActual); // mueve el objeto hacia adelante
        distanciaRestante -= distanciaActual; // actualiza la distancia restante
        yield return new WaitForSeconds(intervalo); // espera el intervalo de tiempo
    }
    estaCaminando = false; // el objeto ha terminado de moverse
    }


    IEnumerator RotarJugador(float anguloRotacion) 
    {
    float anguloObjetivo = transform.eulerAngles.y + anguloRotacion; //calcula el ángulo final de rotación
    if (anguloObjetivo < 0) {
        anguloObjetivo += 360; //asegura que el ángulo esté en el rango [0, 360) grados, por que de lo contrario se rompe el while de abajo
    }
    float tolerancia = 10f;
    while (Mathf.Abs(transform.eulerAngles.y - anguloObjetivo) > tolerancia) 
    { //mientras el objeto no haya alcanzado el ángulo final de rotación
    //print(Mathf.Abs(transform.eulerAngles.y - anguloObjetivo));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, anguloObjetivo, 0), velocidadRotacion * Time.deltaTime); //aplica la rotación suave
        yield return null; //espera al siguiente frame
    }
    transform.rotation = Quaternion.Euler(0, anguloObjetivo, 0);
    estaRotando = false; //el objeto ha terminado de girar
    yield return null; //espera al siguiente frame
    }
}
