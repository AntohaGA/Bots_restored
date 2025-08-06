using UnityEngine;

public class BaseBuilder : MonoBehaviour
{
    [SerializeField] private Base _prefabBase;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void BildBase(Vector3 position)
    {
        Instantiate(_prefabBase, position, Quaternion.identity);
    }
}