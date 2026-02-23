using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 MinClamp,MaxClamp;
    [SerializeField]
    private float EdgeScrollSize, ScrollSpeed;
    
    void Update()
	{
		EdgeScroll();
        MoveKeyboard();
		#region Clamping Camera Position
		Vector2 Clamp(Vector2 value,Vector2 min,Vector2 max)
        {
            value.x = Mathf.Clamp(value.x, min.x, max.x);
            value.y = Mathf.Clamp(value.y, min.y, max.y);
            return value;
        }
        transform.position = Clamp(transform.position, MinClamp, MaxClamp);
        #endregion
	}


    private void EdgeScroll()
    {
        Vector2 mousepos = Mouse.current.position.ReadValue();
        Vector2 movedir = Vector2.zero;
        if (mousepos.x < EdgeScrollSize) movedir.x -= ScrollSpeed * Time.deltaTime;
        if (mousepos.y < EdgeScrollSize) movedir.y -= ScrollSpeed * Time.deltaTime;
        if (mousepos.x > Screen.width - EdgeScrollSize) movedir.x += ScrollSpeed * Time.deltaTime;
        if (mousepos.y > Screen.height - EdgeScrollSize) movedir.y += ScrollSpeed * Time.deltaTime;
        transform.position += (Vector3)movedir;
    }

    private void MoveKeyboard()
    {
        Keyboard keyboard = Keyboard.current;
        Vector2 movedir = Vector2.zero;
        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
        {
            movedir.y += 6;
        }
        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
        {
            movedir.y -= 6;
		}
		if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
		{
			movedir.x += 6;
		}
		if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
        {
            movedir.x -= 6;
        }
        transform.Translate(movedir * Time.deltaTime);
    }
}
