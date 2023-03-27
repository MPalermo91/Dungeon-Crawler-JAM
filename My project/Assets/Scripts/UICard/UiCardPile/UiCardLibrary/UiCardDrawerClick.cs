using UnityEngine;
using UnityEngine.EventSystems;

namespace Tools.UI.Card
{
    [RequireComponent(typeof(IMouseInput))]
    public class UiCardDrawerClick : MonoBehaviour
    {
        UiPlayerHandUtils CardDrawer { get; set; }
        IMouseInput Input { get; set; }

        void Awake()
        {
            CardDrawer = transform.parent.GetComponentInChildren<UiPlayerHandUtils>();
            Input = GetComponent<IMouseInput>();
            Input.OnPointerClick += DrawCard;
        }
        [SerializeField] bool isPlayer;
        void DrawCard(PointerEventData obj) 
        {
            if (isPlayer)
            {
                CardDrawer.DrawCard();
            }
        }
    }

    
}