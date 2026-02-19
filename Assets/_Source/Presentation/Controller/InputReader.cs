using System;
using Contracts.Signals.Input;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Presentation.Controller
{
    public class InputReader : MonoBehaviour
    {
        [Inject] private readonly MessageBus _messageBus;
        private InputSystem _inputSystem;

        private void OnEnable()
        {
            _inputSystem = new InputSystem();
            _inputSystem.Enable();

            Subscribe();
        }

        private void Subscribe()
        {
            _inputSystem.Player.TurnEngine.performed += TurnEngine;
        }

        private void Unsubscribe()
        {
            _inputSystem.Player.TurnEngine.performed -= TurnEngine;
        }

        private void Update()
        {
            MoveInput(_inputSystem.Player.Move.ReadValue<Vector2>());
            ForkInput(_inputSystem.Player.Fork.ReadValue<float>());
        }

        private void MoveInput(Vector2 moveDirection)
        {
            _messageBus.Publish(new MoveInputSignal(moveDirection));
        }

        private void ForkInput(float axis)
        {
            _messageBus.Publish(new ForkInputSignal(axis));
        }
        
        private void TurnEngine(InputAction.CallbackContext context)
        {
            _messageBus.Publish(new EngineSetActiveSignal());
        }

        private void OnDisable()
        {
            _inputSystem.Disable();

            Unsubscribe();
        }
    }
}