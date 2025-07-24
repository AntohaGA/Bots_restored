using UnityEngine;

public class FollowerCamera : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 30f;
    [SerializeField] private float _borderThickness = 20f;
    [SerializeField] private Vector2 _minPosition;
    [SerializeField] private Vector2 _maxPosition;

    [SerializeField] private float _zoomSpeed = 10f;
    [SerializeField] private float _minZoom = 10f;
    [SerializeField] private float _maxZoom = 50f;

    private Vector3 _cursorPosition;

    void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        _cursorPosition = transform.position;

        Vector3 mouse = Input.mousePosition;

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        if (mouse.x <= _borderThickness)
            _cursorPosition.x -= _moveSpeed * Time.deltaTime;

        if (mouse.x >= screenWidth - _borderThickness)
            _cursorPosition.x += _moveSpeed * Time.deltaTime;

        if (mouse.y <= _borderThickness)
            _cursorPosition.z -= _moveSpeed * Time.deltaTime;

        if (mouse.y >= screenHeight - _borderThickness)
            _cursorPosition.z += _moveSpeed * Time.deltaTime;

        _cursorPosition.x = Mathf.Clamp(_cursorPosition.x, _minPosition.x, _maxPosition.x);
        _cursorPosition.z = Mathf.Clamp(_cursorPosition.z, _minPosition.y, _maxPosition.y);

        transform.position = new Vector3(_cursorPosition.x, transform.position.y, _cursorPosition.z);
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Approximately(scroll, 0f))
            return;

        float newY = transform.position.y - scroll * _zoomSpeed * 100f * Time.deltaTime;
        newY = Mathf.Clamp(newY, _minZoom, _maxZoom);

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}