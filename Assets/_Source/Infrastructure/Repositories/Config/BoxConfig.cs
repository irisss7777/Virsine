using Contracts.Presentation.Box;
using Contracts.Repositories.Box;
using Presentation.View.Box;
using UnityEngine;

namespace Infrastructure.Repositories.Config
{
    [CreateAssetMenu(fileName = "BoxConfig", menuName = "Scriptable/Configs/BoxConfig")]
    public class BoxConfig : ScriptableObject, IBoxConfig
    {
        public Vector3 StartPosition => _startPosition;
        public int GroundLayer => _groundLayer;
        public float SpawnHeight => _spawnHeight;
        public float FallDuration => _fallDuration;
        public float RiseDuration => _riseDuration;
        public float RotationDuration => _rotationDuration;
        public Vector3 AttachOffset => _attachOffset;
        public IBoxView ViewPrefab => _boxPrefab;

        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private int _groundLayer;
        [SerializeField] private float _spawnHeight;
        [SerializeField] private float _fallDuration = 5;
        [SerializeField] private float _riseDuration = 5;
        [SerializeField] private float _rotationDuration = 5;
        [SerializeField] private Vector3 _attachOffset;
        [SerializeField] private BoxView _boxPrefab;
    }
}