using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class SkillSystem : MonoBehaviour, IPointerClickHandler
{
    public TankUpgrade[] availableupgrades;

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			GameManager.CurrentSelectedTank = gameObject;
			if (eventData.clickCount == 2)
			{
				if (availableupgrades.Length == 0)
					return;
				GameManager.Instance.LocateUpgradeMenu();
			}
		}
	}
}
