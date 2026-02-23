using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TankBase : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform previousbuildslot;
	private Transform currentbuildslot;
    private BoxCollider2D col2d;
    private Vector2 offset;

    private void Start()
    {
        col2d = GetComponent<BoxCollider2D>();
	}

	public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            offset = transform.position - eventData.pressEventCamera.ScreenToWorldPoint(eventData.position);
        previousbuildslot = currentbuildslot;
    }

    public void OnDrag(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			Vector3 plannedpos = (Vector2)eventData.pressEventCamera.ScreenToWorldPoint(eventData.position) + offset;
            plannedpos.z = 0;
            transform.position = plannedpos;
		}
	}

    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentbuildslot.childCount == 0 && col2d.IsTouching(currentbuildslot.GetComponent<BoxCollider2D>()))
		{
			transform.position = currentbuildslot.position;
			transform.parent = currentbuildslot;
		}
        else
        {
            transform.position = previousbuildslot.position;
            transform.parent = previousbuildslot;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Build Slot"))
        {
            currentbuildslot = collision.transform;
        }
    }

}
