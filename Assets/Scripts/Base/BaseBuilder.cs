using UnityEngine;

public class BaseBuilder : MonoBehaviour
{
    [SerializeField] private Base _prefab;

    public void Build(Vector3 position, BoxKeeper boxKeeper, BaseBuilder builder, PoolBoxes poolBoxes, Map map)
    {
        Base newBase = Instantiate(_prefab,position, Quaternion.identity);
        newBase.Init(boxKeeper, builder, poolBoxes, map);
    }
}