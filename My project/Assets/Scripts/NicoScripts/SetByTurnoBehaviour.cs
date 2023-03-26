using UnityEngine;
 
public class SetByTurnoBehaviour : MonoBehaviour
{

    [SerializeField] GameObject objeto;
    [SerializeField] bool isPlayer;
   
   //Este script solamente activa y desactiva cosas dependiendo del turno que sea

    private void Update() 
    {
        if (isPlayer)
        {

            objeto.SetActive(SistemaDeTurnos.Instance.Get_TurnoPlayer());

        }
        else
        {
            objeto.SetActive(!SistemaDeTurnos.Instance.Get_TurnoPlayer());
        }
    }
}