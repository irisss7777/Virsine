using UnityEngine;

namespace Contracts.Presentation.Loader
{
    public interface ILoaderPresenter
    {
        public void Move(Vector2 moveDirection, float moveSpeed, float rotateSpeed);
        public void Rotate(float rotateDirection);
        public void SetEngineState(bool active);
        public void MoveFork(float moveDirection);
        public void DisplayFuel(float current, float max);
    }
}