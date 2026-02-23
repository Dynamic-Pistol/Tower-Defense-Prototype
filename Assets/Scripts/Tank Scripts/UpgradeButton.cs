using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action Click; 

    public void OnPointerClick(PointerEventData eventData)
    {
        Click?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
		(transform as RectTransform).sizeDelta = new Vector2(150, 150);
	}

    public void OnPointerExit(PointerEventData eventData)
    {
		(transform as RectTransform).sizeDelta = new Vector2(100, 100);
	}
}
