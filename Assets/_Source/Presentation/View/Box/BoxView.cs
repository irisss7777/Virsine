using System;
using Contracts.Presentation.Box;
using UnityEngine;

namespace Presentation.View.Box
{
    [RequireComponent(typeof(Rigidbody))]
    public class BoxView : MonoBehaviour, IBoxView
    {
        public bool PickupMod { get; private set; }
        public Rigidbody Rigidbody => GetComponent<Rigidbody>();

        [SerializeField] private GameObject _collidersObject;
        
        public event Action<bool, Collider> OnTriggerChanged;
        public event Action OnDestroyBox;

        public void SetPickupMod(bool pickupMod) => PickupMod = pickupMod;

        public void SetColliderEnable(bool isEnable) =>
            _collidersObject.SetActive(isEnable);
        
        public void DestroyBox() =>
            OnDestroyBox?.Invoke();

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerChanged?.Invoke(true, other);
        }

        private void OnTriggerExit(Collider other)
        {
            OnTriggerChanged?.Invoke(false, other);

        }
    }
}