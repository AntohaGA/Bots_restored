using UnityEngine;

[RequireComponent(typeof(MouseInputHandler))]
public class FlagPlacer : MonoBehaviour
{
    private MouseInputHandler _mouseInputHandler;
    private Flag _flagPrefab;
    private Flag _currentFlag;
    private Base _selectedBase;
    private Transform _flagTransform;
    private Vector3 _targetFlagPosition;

    private float _flagMoveSpeed = 100f;
    private bool _isMovingFlag = false;

    private void Awake()
    {
        _flagPrefab = Resources.Load<Flag>("Prefabs/Flag");
        _mouseInputHandler = GetComponent<MouseInputHandler>();
    }

    private void Update()
    {
        if (_mouseInputHandler.IsLeftClickDown())
        {
            HandleClick();
        }

        if (_isMovingFlag && _flagTransform != null)
        {
            MoveFlag();
        }
    }

    private void HandleClick()
    {
        if (_mouseInputHandler.TryRaycast(out RaycastHit hit))
        {
            if (_isMovingFlag)
            {
                TryPlaceFlagOnMap(hit);
            }
            else
            {
                TrySelectBase(hit);
            }
        }
    }

    private void TryPlaceFlagOnMap(RaycastHit hit)
    {
        Map map = hit.collider.GetComponentInParent<Map>();

        if (map != null)
        {
            _targetFlagPosition = hit.point;
            _flagTransform.position = _targetFlagPosition;
            _isMovingFlag = false;
            _selectedBase.ToggleBuildStatus();
            ResetSelection();
        }
    }

    private void TrySelectBase(RaycastHit hit)
    {
        Base baseHit = hit.collider.GetComponentInParent<Base>();

        if (baseHit != null)
        {
            _selectedBase = baseHit;

            if (_selectedBase.FlagBase == null)
            {
                _currentFlag = Instantiate(_flagPrefab, hit.point, Quaternion.identity);
                _selectedBase.FlagBase = _currentFlag;
            }

            _flagTransform = _selectedBase.FlagBase.transform;
            _isMovingFlag = true;
        }
    }

    private void ResetSelection()
    {
        _selectedBase = null;
        _flagTransform = null;
        _currentFlag = null;
    }

    private void MoveFlag()
    {
        if (_flagTransform == null)
            return;

        if (_mouseInputHandler.TryRaycast(out RaycastHit hit))
        {
            Map map = hit.collider.GetComponentInParent<Map>();

            if (map != null)
            {
                _targetFlagPosition = hit.point;
                _flagTransform.position = Vector3.MoveTowards(_flagTransform.position,
                                                                            _targetFlagPosition, _flagMoveSpeed * Time.deltaTime);
            }
        }
    }
}