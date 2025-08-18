using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolObject<T> : MonoBehaviour, IEnumerable<T> where T : MonoBehaviour
{
    protected const int DefaultPoolCapacity = 10;
    protected const int DefaultPoolMaxSize = 100;

    protected T _prefab;

    private int _poolCapacity;
    private int _poolMaxSize;
    private List<T> _activeObjects;

    public int ActiveCount => _activeObjects.Count;

    public ObjectPool<T> Pool { get; private set; }

    public void Init(T prefab)
    {
        Init(DefaultPoolCapacity, DefaultPoolMaxSize, prefab);
    }

    public void Init(int poolCapacity, int poolMaxSize, T prefab)
    {
        _poolCapacity = poolCapacity;
        _poolMaxSize = poolMaxSize;
        _prefab = prefab;
        Pool = new ObjectPool<T>(CreateInstance, TakeFromPool, ReturnToPool,
                                  DestroyInstance, true, _poolCapacity, _poolMaxSize);
        _activeObjects = new List<T>();
    }

    public T GetInstance()
    {
        return Pool.Get();
    }

    public void ReturnInstance(T poolObject)
    {
        Pool.Release(poolObject);
    }

    private T CreateInstance()
    {
        return Instantiate(_prefab);
    }

    private void TakeFromPool(T poolObject)
    {
        if (!_activeObjects.Contains(poolObject))
            _activeObjects.Add(poolObject);

        poolObject.gameObject.SetActive(true);
    }

    private void ReturnToPool(T poolObject)
    {
        poolObject.gameObject.SetActive(false);
        _activeObjects.Remove(poolObject);
    }

    private void DestroyInstance(T poolObject)
    {
        Destroy(poolObject.gameObject);
    }

    public void ClearPool()
    {
        foreach (var activeObject in new List<T>(_activeObjects))
        {
            Pool.Release(activeObject);
        }

        Pool.Clear();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _activeObjects.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}