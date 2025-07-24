using UnityEngine;

public class FollowerCamera : MonoBehaviour
{
    [SerializeField] private MouseScrollInput _mouseScrollInput;
    [SerializeField] private CameraMovementHandler _movementHandler;
    [SerializeField] private CameraZoomHandler _zoomHandler;

    private void LateUpdate()
    {
        _movementHandler.HandleMovement(transform);
        _zoomHandler.HandleZoom(transform, _mouseScrollInput.ScrollValue);
    }
}