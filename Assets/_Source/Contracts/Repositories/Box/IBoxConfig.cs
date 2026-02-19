using Contracts.Presentation.Box;
using UnityEngine;

namespace Contracts.Repositories.Box
{
    public interface IBoxConfig
    {
        public Vector3 StartPosition { get; }
        public int GroundLayer { get; }
        public float SpawnHeight { get; }
        public float FallDuration { get; }
        public float RiseDuration { get; }
        public float RotationDuration { get; }
        public Vector3 AttachOffset { get; }
        public IBoxView ViewPrefab { get; }
    }
}