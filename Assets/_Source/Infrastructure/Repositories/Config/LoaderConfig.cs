using Contracts.Presentation.Loader;
using Contracts.Repositories.Loader;
using Presentation.View.Loader;
using UnityEngine;

[CreateAssetMenu(fileName = "LoaderConfig", menuName = "Scriptable/Configs/LoaderConfig")]
public class LoaderConfig : ScriptableObject, ILoaderConfig
{
    public Vector2 ForkPositionRange => _forkPositionRange;
    public float ForkLoaderSpeed => _forkLoaderSpeed;
    public float MoveLoaderSpeed => _moveLoaderSpeed;
    public float RotationLoaderSpeed => _rotationLoaderSpeed;
    public float MaxFuelValue => _maxFuelValue;
    public float MoveFuelCost => _moveFuelCost;
    public float MaxSteeringAngle => _maxSteeringAngle;
    public float SteeringSmoothTime => _steeringSmoothTime;
    public float SteeringDefaultTime => _steeringDefaultTime;
    public ILoaderView LoaderViewPrefab => _loaderViewPrefab;
    
    [SerializeField] private Vector2 _forkPositionRange;
    [SerializeField] private float _rotationLoaderSpeed;
    [SerializeField] private float _moveLoaderSpeed;
    [SerializeField] private float _forkLoaderSpeed;
    [SerializeField] private float _maxFuelValue;
    [SerializeField] private float _moveFuelCost;
    [SerializeField] private float _maxSteeringAngle;
    [SerializeField] private float _steeringSmoothTime;
    [SerializeField] private float _steeringDefaultTime;
    [SerializeField] private LoaderView _loaderViewPrefab;
}
