using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropObject : MonoBehaviour
{

    Vector3 MousePosition;

    Vector3 GetMousePosition()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        MousePosition = Input.mousePosition - GetMousePosition();
    }

    private void OnMouseDrag()
    {
        Vector3 controlPos = Input.mousePosition - MousePosition;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(controlPos.x, Camera.main.WorldToScreenPoint(transform.position).y, Camera.main.WorldToScreenPoint(transform.position).z));
    }
}
