using Code.Scripts.Core.Enums.States.Input;
using UnityEngine;

namespace Code.Scripts.Engine.Controls.InputManager
{
    public class InputManager : Core.DesignPatterns.Singleton.Singleton<InputManager>
    {
        #region Set Up

        private ThirdView _thirdView;

        private void Start()
        {
            _thirdView = new ThirdView();
            if (!gameObject.tag.Equals("InputManager"))
            {
                gameObject.tag = "InputManager";
            }

            SetState(InputState.Third);
        }

        public void OnEnable()
        {
            SetState(InputState.Disabled);
        }
        
        #endregion

        #region Maps Handler

        [SerializeField] private InputState _state;

        public void SetState(InputState state)
        {
            _state = state;

            switch (_state)
            {
                case InputState.Third:
                    _thirdView.Walking.Enable();
                    _thirdView.Inventory.Enable();
                    _thirdView.Interactions.Enable();
                    break;
                case InputState.UI:
                    _thirdView.Walking.Disable();
                    break;
            }
        }

        #endregion

        #region Getters

        public InputState GetState()
        {
            return _state;
        }

        public Vector2 GetMove()
        {
            return _state switch
            {
                InputState.Disabled => Vector2.zero,
                InputState.Third => _thirdView.Walking.Move.ReadValue<Vector2>(),
                InputState.UI => Vector2.zero,
                _ => Vector2.zero
            };
        }
        
        public Vector2 GetLook()
        {
            return _state switch
            {
                InputState.Disabled => Vector2.zero,
                InputState.Third => _thirdView.Walking.Look.ReadValue<Vector2>(),
                InputState.UI => Vector2.zero,
                _ => Vector2.zero
            };
        }

        public bool IsWalking()
        {
            return _state switch
            {
                InputState.Disabled => false,
                InputState.Third => (GetMove() != Vector2.zero && !_thirdView.Walking.Run.IsPressed()),
                InputState.UI => false,
                _ => false
            };
        }

        public bool IsRunning()
        {
            return _state switch
            {
                InputState.Disabled => false,
                InputState.Third => (GetMove() != Vector2.zero && _thirdView.Walking.Run.IsPressed()),
                InputState.UI => false,
                _ => false
            };
        }

        public bool ToggleInventory()
        {
            return _state switch
            {
                InputState.Disabled => false,
                InputState.Third => _thirdView.Inventory.ToggleMenu.WasPressedThisFrame(),
                InputState.UI => _thirdView.Inventory.ToggleMenu.WasPressedThisFrame(),
                _ => false
            };
        }

        public bool GetInteract()
        {
            return _state switch
            {
                InputState.Disabled => false,
                InputState.Third => _thirdView.Interactions.Interact.WasPressedThisFrame(),
                InputState.UI => false,
                _ => false
            };
        }
        
        public bool GetRoll()
        {
            return _state switch
            {
                InputState.Disabled => false,
                InputState.Third => _thirdView.Walking.Roll.WasPressedThisFrame(),
                InputState.UI => false,
                _ => false
            };
        }

        #endregion
    }
}