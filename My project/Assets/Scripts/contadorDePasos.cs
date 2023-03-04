using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contadorDePasos : MonoBehaviour
{
    public int pasosActuales;
    public bool enemigosSeMueven;

    void Start()
    {
        pasosActuales=0;
    }
    void Update()
    {
        if(pasosActuales>=2)
        {
            StartCoroutine(enemigosCaminan());
        }
    }
    IEnumerator enemigosCaminan()
    {
        Debug.Log("AJAJAJAJ");
            enemigosSeMueven = true;
            pasosActuales = 0;
            yield return new WaitForSeconds(0.5f);
            enemigosSeMueven = false;
        yield return null;
    }
}
