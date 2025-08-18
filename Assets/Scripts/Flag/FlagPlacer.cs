using UnityEngine;

public class FlagPlacer : MonoBehaviour
{
    private const float MaxDistanceCast = 100f;

    private Flag _flagPrefab;
    private Flag _currentFlag;
    private Base _selectedBase;
    private Transform _flagTransform;
    private Camera _camera;
    private Vector3 _targetFlagPosition;

    private float _flagMoveSpeed = 50f;
    private bool _isMovingFlag = false;

    private void Awake()
    {
        _flagPrefab = Resources.Load<Flag>("Prefabs/Flag");
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, MaxDistanceCast))
        {
            Base baseHit = hit.collider.GetComponentInParent<Base>();

            if (baseHit != null)
            {
                _selectedBase = baseHit;

                if(_selectedBase.FlagBase == null)
                {
                    _currentFlag = Instantiate(_flagPrefab, hit.transform.position, Quaternion.identity);
                    _selectedBase.FlagBase = _currentFlag;
                }

                _flagTransform = _selectedBase.FlagBase.transform;
                _isMovingFlag = true;
            }
        }

        if (_isMovingFlag && Physics.Raycast(ray, out hit, MaxDistanceCast))
        {
            Map map = hit.collider.GetComponentInParent<Map>();

            if (map != null)
            {
                _targetFlagPosition = hit.point;
                _flagTransform.position = _targetFlagPosition;
                _isMovingFlag = false;
                _selectedBase.ToggleBuildStatus();
                _selectedBase = null;
                _flagTransform = null;
            }
        }
    }

    private void MoveFlag()
    {
        if (_flagTransform == null)
            return;

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, MaxDistanceCast))
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