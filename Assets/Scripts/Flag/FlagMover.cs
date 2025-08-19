using UnityEngine;

[RequireComponent(typeof(MouseInputHandler))]
public class FlagMover : MonoBehaviour
{
    private MouseInputHandler _mouseInputHandler;
    private FlagPlacer _flagPlacer;

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

        if (_flagPlacer != null && _flagPlacer.IsMovingFlag)
        {
            if (_mouseInputHandler.TryRaycast(out RaycastHit hit))
            {
                Map map = hit.collider.GetComponentInParent<Map>();

                if (map != null)
                {
                    _flagPlacer.MoveFlag(hit.point);
                }
            }
        }
    }

    private void HandleClick()
    {
        if (_mouseInputHandler.TryRaycast(out RaycastHit hit))
        {
            if (_flagPlacer != null && _flagPlacer.IsMovingFlag)
            {
                if (_flagPlacer.TryPlaceFlagOnMap(hit))
                {
                    _flagPlacer = null;
                }
            }
            else
            {
                TrySelectBase(hit);
            }
        }
    }

    private void TrySelectBase(RaycastHit hit)
    {
        Base baseHit = hit.collider.GetComponentInParent<Base>();

        if (baseHit != null)
        {
            _flagPlacer = baseHit.FlagPlacer;
            _flagPlacer.IsMovingFlag = true;
        }
    }
}