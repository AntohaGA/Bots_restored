using System;
using UnityEngine;

public class ClickBaseDetector : MonoBehaviour
{
    public event Action OnBaseClicked;

    private void OnMouseDown()
    {
        OnBaseClicked?.Invoke();
    }
}