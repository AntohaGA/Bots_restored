using UnityEngine;

public class MouseScrollInput : MonoBehaviour
{
    private const string Axis = "Mouse ScrollWheel";

    private float _zoomSpeed = 25f;

    public float ScrollValue { get; private set; }

    private void Update()
    {
        float scroll = Input.GetAxis(Axis);
        ScrollValue = scroll * _zoomSpeed;
    }
}