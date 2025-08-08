using System;
using UnityEngine;

[RequireComponent(typeof(ClickBaseDetector))]
public class MapFlagPlacer : MonoBehaviour
{
    [SerializeField] private Flag _flagPrefab;

    private Map _map;
    private Flag _currentFlag;
    private ClickBaseDetector _clickBaseDetector;
    private ClickMapDetector _clickMapDetector;

    private bool _isReadyPlacingFlag = false;

    public event Action<Vector3> FlagPlaced;

    private void Awake()
    {
        _clickBaseDetector = GetComponent<ClickBaseDetector>();
    }

    public void Init(Map map)
    {
        _map = map;
        _clickMapDetector = _map.GetComponent<ClickMapDetector>();
        _clickMapDetector.OnMapClicked += PlaceFlag;
    }

    private void OnEnable()
    {
        _clickBaseDetector.OnBaseClicked += ToggleFlagPlacement;
    }

    private void OnDisable()
    {
        _clickBaseDetector.OnBaseClicked -= ToggleFlagPlacement;
        _clickMapDetector.OnMapClicked -= PlaceFlag;
    }

    private void ToggleFlagPlacement()
    {
        _isReadyPlacingFlag = (!_isReadyPlacingFlag);
    }

    private void PlaceFlag(Vector3 position)
    {
        if (_isReadyPlacingFlag)
        {
            if (_currentFlag != null)
                Destroy(_currentFlag.gameObject);

            _currentFlag = Instantiate(_flagPrefab, position, Quaternion.identity);
            _isReadyPlacingFlag = false;

            FlagPlaced?.Invoke(position);
        }
    }
}