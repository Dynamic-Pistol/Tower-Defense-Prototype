using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TankSlot : MonoBehaviour,IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            GameManager.CurrentSelectedTankSlot = this;
            if (eventData.clickCount == 2)
            {
                GameManager.Instance.BuyTank();
            }
        }
    }
}
