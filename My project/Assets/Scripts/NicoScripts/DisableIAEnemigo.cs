using UnityEngine;
 
public class DisableIAEnemigo : MonoBehaviour
{

    [SerializeField] GameObject objeto;
   
   //Este script solamente activa y desactiva cosas dependiendo del turno que sea

    private void Update() 
    {

            objeto.SetActive(SistemaDeTurnos.Instance.Get_IAEnemigo());

    }
}