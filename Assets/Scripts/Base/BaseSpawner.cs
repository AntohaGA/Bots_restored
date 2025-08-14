using UnityEngine;
using UnityEngine.UIElements;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private BoxKeeper _boxKeeper;
    [SerializeField] private ClickMapDetector _clickMapDetector;

    private Base _basePrefab;
    
    private void Awake()
    {
        _basePrefab = Resources.Load<Base>("Prefabs/Basa");

        SpawnFirstBase();
    }

    public void SpawnBase(Vector3 position, Bot bot)
    {
        Base baseInstance = Instantiate(_basePrefab, position, Quaternion.identity);
        baseInstance.InitDependencies(_boxKeeper, _clickMapDetector, this);
        baseInstance.Initialize(bot);
    }

    private void SpawnFirstBase()
    {
        Bot bot = null;

        Base baseInstance = Instantiate(_basePrefab, new(0, 0, 0), Quaternion.identity);
        baseInstance.InitDependencies(_boxKeeper, _clickMapDetector, this);
        baseInstance.Initialize(bot);
    }
}