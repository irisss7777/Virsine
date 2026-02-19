using UnityEngine;

namespace Contracts.Signals.Input
{
    public struct MoveInputSignal
    {
        public MoveInputSignal(Vector2 moveDirection)
        {
            MoveDirection = moveDirection;
        }

        public Vector2 MoveDirection { get; private set; }
    }
}