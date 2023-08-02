using System;
using Code.Scripts.Core.Enums.Animations;
using Code.Scripts.Engine.Controls.InputManager;
using UnityEngine;

namespace Code.Scripts.Engine.Player.Animations
{
    public class AnimationManager : MonoBehaviour
    {
        [SerializeField] private MoveState _moveState;
        private Animator _animator;

        protected void Awake()
        {
            _moveState = MoveState.Idle;
            _animator = transform.GetComponent<Animator>();
        }

        protected void Update()
        {
            SetMoveState();
        }

        private void MoveAnimation()
        {
            switch (_moveState)
            {
                case MoveState.Idle:
                    _animator.SetTrigger("Idle");
                    break;
                case MoveState.Walk:
                    _animator.SetTrigger("Walk");
                    break;
                case MoveState.Run:
                    _animator.SetTrigger("Run");
                    break;
            }
        }

        private void SetMoveState()
        {
            if (InputManager.Instance.IsWalking() && _moveState != MoveState.Walk)
            {
                _moveState = MoveState.Walk;
                MoveAnimation();
                return;
            }
            
            if (InputManager.Instance.IsRunning() && _moveState != MoveState.Run)
            {
                _moveState = MoveState.Run;
                MoveAnimation();
                return;
            }

            if (!InputManager.Instance.IsWalking() && !InputManager.Instance.IsRunning() && _moveState != MoveState.Idle)
            {
                _moveState = MoveState.Idle;
                MoveAnimation();
            }
        }
    }
}
