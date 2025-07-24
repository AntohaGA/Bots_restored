using UnityEngine;

public class CameraZoomHandler : MonoBehaviour
{
    [SerializeField] private float _zoomSpeed = 30f;
    [SerializeField] private float _minZoom = 10f;
    [SerializeField] private float _maxZoom = 50f;

    public void HandleZoom(Transform cameraTransform, float scrollValue)
    {
        if (Mathf.Approximately(scrollValue, 0f))
            return;

        float newY = cameraTransform.position.y - scrollValue * _zoomSpeed * Time.deltaTime;
        newY = Mathf.Clamp(newY, _minZoom, _maxZoom);

        cameraTransform.position = new Vector3(cameraTransform.position.x, newY, cameraTransform.position.z);
    }
}