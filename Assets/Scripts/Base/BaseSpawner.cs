using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private BoxKeeper _boxKeeper;
    [SerializeField] private ClickMapDetector _clickMapDetector;

    private Base _basePrefab;
    
    private void Awake()
    {
        _basePrefab = Resources.Load<Base>("Prefabs/Basa");   
    }

    private void Start()
    {
        SpawnFirstBase();
    }

    public void SpawnBase(Vector3 position)
    {
        Base baseInstance = Instantiate(_basePrefab, position, Quaternion.identity);
        baseInstance.InitDependencies(_boxKeeper, _clickMapDetector, this);
        baseInstance.Initialize();
    }

    private void SpawnFirstBase()
    {
        Vector3 positionFirstBase = new Vector3(0, 0, 0);
        SpawnBase(positionFirstBase);
    }
}