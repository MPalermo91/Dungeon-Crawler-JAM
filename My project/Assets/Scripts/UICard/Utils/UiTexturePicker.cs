using System.Linq;
using Extensions;
using UnityEngine;
using System.Collections.Generic;


namespace Tools.UI.Card
{
    /// <summary>
    ///     Picks a Texture randomly when it Awakes.
    /// </summary>
    public class UiTexturePicker : MonoBehaviour
    {
        //[SerializeField] Sprite[] Sprites; //de prueba
        [SerializeField] SpriteRenderer MyRenderer { get; set; }
        [SerializeField] GameObject cartaPadre;
        public bool isPlayer = true;

        [SerializeField] Sprite[] posiblesCartas;


        void Awake()
        {

            MyRenderer = GetComponent<SpriteRenderer>();
            if(isPlayer)
            {
                //Esto tiene que cambiar dependiendo del mazo y de que cantidad de cartas pueda llevar
                //El mazo: tenes entre todas las cartas que fuiste recolectando

                //if (Sprites.Length > 0)
                //MyRenderer.sprite = Sprites.ToList().RandomItem(); // de prueba
                //MyRenderer.sprite = agarra una carta
                //Suma un punto
                MazoManager.Instance.ContadorDeCartas();

                MyRenderer.sprite = MazoManager.Instance.Get_SpriteActual();


                PosiblesCartas();
                

                //Esto agarra un random de todo el mazo
                //Hay que cambiarlo por agarrar una lista que va cambiando
            }
            else
            {
                MazoEnemigoManager.Instance.ContadorDeCartas();
                MyRenderer.sprite = MazoEnemigoManager.Instance.Get_SpriteActual();
                PosiblesCartas();
            }
            

        }


        private void PosiblesCartas()
        {
            if (MyRenderer.sprite == posiblesCartas[0])
                {
                    //Debug.Log("toco demonio: hace 4 de daño");
                    int daño = 4;
                    cartaPadre.GetComponent<Tools.UI.Card.UiCardComponent>().Set_Daño(daño);
                }
                else if (MyRenderer.sprite == posiblesCartas[1])
                {
                    //Debug.Log("toco EvilGod: hace 1 de daño");
                    int daño = 1;
                    cartaPadre.GetComponent<Tools.UI.Card.UiCardComponent>().Set_Daño(daño);
                }
                else if (MyRenderer.sprite == posiblesCartas[2])
                {
                    //Debug.Log("toco EvilGod: hace 1 de daño");
                    int daño = 5;
                    cartaPadre.GetComponent<Tools.UI.Card.UiCardComponent>().Set_Daño(daño);
                }
        }


        
    }
}