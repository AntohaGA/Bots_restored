using System;
using UnityEngine;

[RequireComponent(typeof(ClickBaseDetector))]
public class FlagPlacer : MonoBehaviour
{
    private ClickMapDetector _clickMapDetector;
    private Flag _flagPrefab;
    private Flag _currentFlag;
    private ClickBaseDetector _clickBaseDetector;

    private bool _isReadyPlacingFlag = false;

    public event Action<Vector3> FlagPlaced;

    private void Awake()
    {
        _clickBaseDetector = GetComponent<ClickBaseDetector>();
    }

    public void Init(ClickMapDetector clickMapDetector)
    {
        _flagPrefab = Resources.Load<Flag>("Prefabs/Flag");

        _clickMapDetector = clickMapDetector;
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

            FlagPlaced?.Invoke(position);
        }
    }
}