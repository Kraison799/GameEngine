using System;
using Code.Scripts.Core.DesignPatterns.Singleton;
using Code.Scripts.Core.Enums.States.Camera;
using UnityEngine;

namespace Code.Scripts.Engine.Controls.CameraManager
{
    public class CameraManager : Singleton<CameraManager>
    {
        #region Set Up

        private Camera _camera;
        private Animator _animator;
        private CameraState _state;

        public void Start()
        {
            _camera = Camera.main;
            _animator = GetComponent<Animator>();
            _state = CameraState.None;
        }

        #endregion

        #region Transitions

        public void TransitionTo(CameraState state)
        {
            _state = state;
            switch (_state)
            {
                case CameraState.None:
                    _animator.Play("Default");
                    break;
                case CameraState.ThirdV:
                    _animator.Play("ThirdView");
                    break;
            }
        }

        #endregion
    }
}