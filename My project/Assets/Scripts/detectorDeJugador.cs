using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectorDeJugador : MonoBehaviour
{
    void OnTriggerEnterCollider (Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Jugador"))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
