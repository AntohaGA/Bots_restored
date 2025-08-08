using UnityEngine;

[RequireComponent(typeof(MouseScrollInput))]
public class FollowerCamera : MonoBehaviour
{
    private MouseScrollInput _mouseScrollInput;
    private CameraMovementHandler _movementHandler;
    private CameraZoomHandler _zoomHandler;

    private void Awake()
    {
        _mouseScrollInput = GetComponent<MouseScrollInput>();
        _zoomHandler = new CameraZoomHandler();
        _movementHandler = new CameraMovementHandler();
    }

    private void LateUpdate()
    {
        _movementHandler.HandleMovement(transform);
        _zoomHandler.HandleZoom(transform, _mouseScrollInput.ScrollValue);
    }
}