using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Map : MonoBehaviour
{
    private Renderer _renderer;
    private float _borderThickness = 2;
    private float _ySpawnPosition = 1.3f;

    public Vector2 MinBounds { get; private set; }
    public Vector2 MaxBounds { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        CalculateBounds();
    }

    public Vector3 GetSpawnPosition()
    {
        float x = Random.Range(MinBounds.x, MaxBounds.x);
        float z = Random.Range(MinBounds.y, MaxBounds.y);
        float y = _ySpawnPosition;

        return new Vector3(x, y, z);
    }

    private void CalculateBounds()
    {
        Bounds bounds = _renderer.bounds;
        MinBounds = new Vector2(bounds.min.x + _borderThickness, bounds.min.z + _borderThickness);
        MaxBounds = new Vector2(bounds.max.x - _borderThickness, bounds.max.z - _borderThickness);
    }
}