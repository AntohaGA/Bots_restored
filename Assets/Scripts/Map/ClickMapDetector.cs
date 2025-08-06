using System;
using UnityEngine;

public class ClickMapDetector : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    public event Action<Vector3> OnMapClicked;

    private void OnMouseDown()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            OnMapClicked?.Invoke(hit.point);
        }
    }
}