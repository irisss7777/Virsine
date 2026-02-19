using Contracts.Presentation.Loader;
using UnityEngine;

namespace Contracts.Repositories.Loader
{
    public interface ILoaderConfig
    {
        public Vector2 ForkPositionRange { get; }
        public float ForkLoaderSpeed { get; }
        public float MoveLoaderSpeed { get; }
        public float RotationLoaderSpeed { get; }
        public float MaxFuelValue { get; }
        public float MoveFuelCost { get; }
        public float MaxSteeringAngle { get; }
        public float SteeringSmoothTime { get; }
        public float SteeringDefaultTime { get; }
        
        public ILoaderView LoaderViewPrefab { get; }
    }
}