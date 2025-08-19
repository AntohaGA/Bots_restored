using System;
using UnityEngine;

public class FlagPlacer : MonoBehaviour
{
    private Flag _flagPrefab;
    private Flag _flag;
    private Transform _flagTransform;
    private Vector3 _targetFlagPosition;

    private float _flagMoveSpeed = 100f;

    public event Action<Flag> FlagPlased;

    public bool IsMovingFlag { get; set; } = false;

    private void Awake()
    {
        _flagPrefab = Resources.Load<Flag>("Prefabs/Flag");
        _flag = Instantiate(_flagPrefab, transform.position, Quaternion.identity);
        _flagTransform = _flag.transform;
    }

    public void MoveFlag(Vector3 targetPosition)
    {
        _targetFlagPosition = targetPosition;
        _flagTransform.position = Vector3.MoveTowards(_flagTransform.position, _targetFlagPosition, _flagMoveSpeed * Time.deltaTime);
    }

    public bool TryPlaceFlagOnMap(RaycastHit hit)
    {
        Map map = hit.collider.GetComponentInParent<Map>();

        if (map != null)
        {
            _targetFlagPosition = hit.point;
            _flagTransform.position = _targetFlagPosition;
            IsMovingFlag = false;
            FlagPlased?.Invoke(_flag);

            return true;
        }

        return false;
    }
}