using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoJugador : MonoBehaviour
{
    public bool movimientoInstantaneo, estaRotando, estaCaminando;
    public float velocidadRotacion, velocidadMovimiento, tiempoRotacion, anguloRotacion, distanciaMovimiento;
    public Transform posicionFinal;
public float intervalo = 0.02f; // intervalo de tiempo entre movimientos


    // Start is called before the first frame update
    void Start()
    {
        movimientoInstantaneo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w") && !estaCaminando)
        {
            if(movimientoInstantaneo)
            {
                transform.Translate(Vector3.forward * distanciaMovimiento);
            }
            else
            {
                StartCoroutine(MoverJugador());
            }
            print("Me muevo adelante, pa");
        }
        if (Input.GetKeyDown("a") && !estaRotando)
        {
            if(movimientoInstantaneo)
            {
                transform.Rotate(0, -90, 0);
            }
            else
            {
                estaRotando = true; //el objeto está girando
                StartCoroutine(RotarJugador(-90));
            }
        }
        if (Input.GetKeyDown("d") && !estaRotando)
        {
            if(movimientoInstantaneo)
            {
                transform.Rotate(0, 90, 0);
            }
            else
            {
                estaRotando = true; //el objeto está girando
                StartCoroutine(RotarJugador(90));
            }
        }
    }

    IEnumerator MoverJugador() 
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
    print("Angulo objetivo:"+anguloObjetivo);
    float tolerancia = 10f;
    velocidadRotacion = 350f;
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
