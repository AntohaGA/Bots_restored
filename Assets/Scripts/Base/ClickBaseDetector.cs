using System;
using UnityEngine;

public class ClickBaseDetector : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    public event Action OnBaseClicked;

    private void OnMouseDown()
    {
        OnBaseClicked?.Invoke();
    }
}