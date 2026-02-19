using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Presentation.View.Box
{
    public class DropZoneView : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out BoxView box))
                WaitForDestroyBox(box).Forget();
        }

        private async UniTask WaitForDestroyBox(BoxView box)
        {
            while (box.PickupMod)
            {
                await UniTask.Yield();
            }
            
            box.DestroyBox();
        }
    }
}