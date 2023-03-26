using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEnemigo : MonoBehaviour
{
    [SerializeField] GameObject handUtils;
    [SerializeField] GameObject handUtils2;

    //Sistema de vida
    

    //primero agarra carta
    //segundo tira carta random (no seteado porque no hay tiempo)

    private void OnEnable() 
    {

    StartCoroutine(JugarCartas());
           
    }

    IEnumerator JugarCartas()
    {
        yield return new WaitForSeconds(1f);
        handUtils.GetComponent<Tools.UI.Card.UiPlayerHandUtils>().DrawCardEnemigo();
        
        yield return new WaitForSeconds(2f);
        handUtils.GetComponent<Tools.UI.Card.UiPlayerHandUtils>().PlayCard();

        if (SistemaDeTurnos.Instance.Get_ValorTurno() == 1)
        {
            handUtils2.GetComponent<Tools.UI.Card.UiPlayerHand>().EnableCards();    
        }
        

        SistemaDeTurnos.Instance.TerminarTurnoEnemigo();
        yield return null;
    }

    

}
