using System.Linq;
using Extensions;
using UnityEngine;

namespace Tools.UI.Card
{
    /// <summary>
    ///     Picks a Texture randomly when it Awakes.
    /// </summary>
    public class UiTexturePicker : MonoBehaviour
    {
        //[SerializeField] Sprite[] Sprites;
        [SerializeField] SpriteRenderer MyRenderer { get; set; }

        void Awake()
        {
            MyRenderer = GetComponent<SpriteRenderer>();



            //Esto tiene que cambiar dependiendo del mazo y de que cantidad de cartas pueda llevar
            //El mazo: tenes entre todas las cartas que fuiste recolectando

            /*if (Sprites.Length > 0)
                MyRenderer.sprite = Sprites.ToList().RandomItem();*/
                MyRenderer.sprite = MazoManager.Instance.Get_Mazo().RandomItem();
        }
    }
}