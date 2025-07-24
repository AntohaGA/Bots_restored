using UnityEngine;

public class MouseScrollInput : MonoBehaviour
{
    [SerializeField] private float _zoomSpeed = 10f;

    public float ScrollValue { get; private set; }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ScrollValue = scroll * _zoomSpeed;
    }
}