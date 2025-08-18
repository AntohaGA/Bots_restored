using System;
using UnityEngine;

public class FlagPlacer : MonoBehaviour
{
    private Flag _flagPrefab;
    private Flag _currentFlag;

    [SerializeField] private LayerMask _baseLayerMask;
    [SerializeField] private LayerMask _groundLayerMask;

    private bool _isReadyPlacingFlag = false;

    public event Action<Flag> FlagPlaced;

    private Base _selectedBase;
    private Transform _flagTransform;
    private Camera _camera;

    public void Init()
    {
        _flagPrefab = Resources.Load<Flag>("Prefabs/Flag");
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _baseLayerMask))
            {
                Base baseHit = hit.collider.GetComponentInParent<Base>();

                if (baseHit != null)
                {
                    _selectedBase = baseHit;
                    _flagTransform = _selectedBase.FlagBase.transform;// сделать событие с передачей флага с координатами
                    Debug.Log("База выбрана: " + _selectedBase.name);
                }
            }
            else

            if (_selectedBase != null && Physics.Raycast(ray, out hit, Mathf.Infinity, _groundLayerMask))
            {
                _flagTransform.position = hit.point;
                Debug.Log("Флаг установлен в позиции: " + hit.point);
                _selectedBase = null;
                _flagTransform = null;
            }
        }
    }
    private void ToggleFlagPlacement()
    {
        _isReadyPlacingFlag = !(_isReadyPlacingFlag);
    }

    private void PlaceFlag(Vector3 position)
    {
        if (_isReadyPlacingFlag)
        {
            if (_currentFlag != null)
                Destroy(_currentFlag.gameObject);

            _currentFlag = Instantiate(_flagPrefab, position, Quaternion.identity);
            _isReadyPlacingFlag = false;

            FlagPlaced?.Invoke(_currentFlag);
        }
    }
}