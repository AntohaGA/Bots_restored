using UnityEngine;

public class MouseInputHandler : MonoBehaviour
{
    private const float MaxDistanceCast = 100f;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public bool IsLeftClickDown()
    {
        return Input.GetMouseButtonDown(0);
    }

    public bool TryRaycast(out RaycastHit hit)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        return Physics.Raycast(ray, out hit, MaxDistanceCast);
    }
}