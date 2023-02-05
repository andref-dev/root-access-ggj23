using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteButton : MonoBehaviour
{
    public UnityEvent OnPress;

    void OnMouseDown()
    {
        if (WindowController.Instance.ShowingIniciar)
            return;

        OnPress.Invoke();
    }
}
