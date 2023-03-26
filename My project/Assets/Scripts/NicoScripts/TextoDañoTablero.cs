using UnityEngine;
using TMPro;
 
public class TextoDañoTablero : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_Object;
    [SerializeField] GameObject textoCompleto;
    [SerializeField] bool isPlayer;
   
    void Start()
    {
          m_Object.GetComponent<TMPro.TextMeshProUGUI>().text = "Inició sin problemas";
    }

    private void Update() 
    {
        if (isPlayer)
        {
            m_Object.GetComponent<TMPro.TextMeshProUGUI>().text = SistemaDeTurnos.Instance.Get_DañoCarta().ToString();
            textoCompleto.SetActive(!SistemaDeTurnos.Instance.Get_TurnoPlayer());
        }
        else
        {
            m_Object.GetComponent<TMPro.TextMeshProUGUI>().text = SistemaDeTurnos.Instance.Get_DañoCarta().ToString();
            textoCompleto.SetActive(SistemaDeTurnos.Instance.Get_TurnoPlayer());
        }
    }
}