using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private BoxKeeper _boxKeeper;

    private Base _basePrefab;
    
    private void Start()
    {
        _basePrefab = Resources.Load<Base>("Prefabs/Basa");
    } 

    public void Spawn(Vector3 position, Bot bot)
    {
        Base baseInstance = Instantiate(_basePrefab, position, Quaternion.identity);
        baseInstance.InitDependencies(_boxKeeper, this);
        baseInstance.Initialize(bot);
    }
}