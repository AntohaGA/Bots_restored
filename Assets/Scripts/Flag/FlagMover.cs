using UnityEngine;

[RequireComponent(typeof(MouseInputHandler))]
public class FlagMover : MonoBehaviour
{
    private MouseInputHandler _mouseInputHandler;
    private Base _selectedBase;
    private Transform _flagTransform;
    private Vector3 _targetFlagPosition;

    private float _flagMoveSpeed = 100f;
    private bool _isMovingFlag = false;

    private void Awake()
    {
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
            _selectedBase.FlagPlacer.ReturnFlag();
            ResetSelection();
        }
    }

    private void TrySelectBase(RaycastHit hit)
    {
        Base baseHit = hit.collider.GetComponentInParent<Base>();

        if (baseHit != null)
        {
            _selectedBase = baseHit;
            _flagTransform = _selectedBase.FlagPlacer.GetFlagTransform();
            _isMovingFlag = true;
        }
    }

    private void ResetSelection()
    {
        _selectedBase = null;
        _flagTransform = null;
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