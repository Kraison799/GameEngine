using Code.Scripts.Core.Enums.Animations;
using Code.Scripts.Core.Settings.Animations;
using Code.Scripts.Engine.Controls.InputManager;
using UnityEngine;

namespace Code.Scripts.Engine.Player.Animations
{
    public class AnimationManager : MonoBehaviour
    {
        private AnimState _state;
        private Animator _animator;
        private float _currentCooldown;
        private AnimationsSettings _animations;

        protected void Awake()
        {
            _state = AnimState.Idle;
            _animator = GetComponent<Animator>();
            
        }

        protected void Update()
        {
            _currentCooldown -= Time.deltaTime;
            
            if (_currentCooldown <= 0.0f) SetState();
        }

        private void PlayAnimation()
        {
            switch (_state)
            {
                case AnimState.Idle:
                    _animator.SetTrigger("Idle");
                    break;
                case AnimState.Walk:
                    _animator.SetTrigger("Walk");
                    break;
                case AnimState.Run:
                    _animator.SetTrigger("Run");
                    break;
                case AnimState.Roll:
                    _animator.SetTrigger("Roll");
                    break;
            }
        }

        private void SetState()
        {
            if (InputManager.Instance.GetRoll())
            {
                _state = AnimState.Roll;
                _currentCooldown = AnimationsSettings.Instance.GetAnim("Roll").GetFixedCooldown() - 0.25f;
                PlayAnimation();
                return;
            }
            
            if (InputManager.Instance.IsWalking() && _state != AnimState.Walk)
            {
                _state = AnimState.Walk;
                PlayAnimation();
                return;
            }
            
            if (InputManager.Instance.IsRunning() && _state != AnimState.Run)
            {
                _state = AnimState.Run;
                PlayAnimation();
                return;
            }

            if (!InputManager.Instance.IsWalking() && !InputManager.Instance.IsRunning() && _state != AnimState.Idle)
            {
                _state = AnimState.Idle;
                PlayAnimation();
                return;
            }
        }
    }
}
