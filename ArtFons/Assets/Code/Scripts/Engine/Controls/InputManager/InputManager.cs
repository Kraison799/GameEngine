using System;
using Code.Scripts.Core.Enums.States.Input;
using Unity.VisualScripting;
using UnityEngine;

namespace Code.Scripts.Engine.Controls.InputManager
{
    public class InputManager : Core.DesignPatterns.Singleton.Singleton<InputManager>
    {
        #region Set Up

        private FirstView _firstView;
        private ThirdView _thirdView;

        private void Start()
        {
            _firstView = new FirstView();
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

        public void OnDisable()
        {
            _thirdView.Disable();
        }
        
        #endregion

        #region Maps Handler

        private InputState _state;

        public void SetState(InputState state)
        {
            _state = state;

            switch (_state)
            {
                case InputState.Disabled:
                    DisableAll();
                    break;
                case InputState.First:
                    _firstView.Enable();
                    break;
                case InputState.Third:
                    _thirdView.Enable();
                    break;
                default:
                    DisableAll();
                    break;
            }
        }

        public void DisableAll()
        {
            _state = InputState.Disabled;
            _firstView.Disable();
            _thirdView.Disable();
        }

        #endregion

        #region Getters

        public Vector2 GetMove()
        {
            return _state switch
            {
                InputState.Disabled => Vector2.zero,
                InputState.First => _firstView.Axis.Move.ReadValue<Vector2>(),
                InputState.Third => _thirdView.Axis.Move.ReadValue<Vector2>(),
                _ => Vector2.zero
            };
        }
        
        public Vector2 GetLook()
        {
            return _state switch
            {
                InputState.Disabled => Vector2.zero,
                InputState.First => _firstView.Axis.Look.ReadValue<Vector2>(),
                InputState.Third => _thirdView.Axis.Look.ReadValue<Vector2>(),
                _ => Vector2.zero
            };
        }

        #endregion
    }
}