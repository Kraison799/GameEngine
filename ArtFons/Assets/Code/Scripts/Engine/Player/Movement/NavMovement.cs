using System;
using Code.Scripts.Engine.Controls.InputManager;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Scripts.Engine.Player.Movement
{
    public class NavMovement: MonoBehaviour
    {
        #region Set Up

        [SerializeField] private float _sensibilityX = 2.0f;
        [SerializeField] private float _sensibilityY = 2.0f;
        [SerializeField] private float _speed = 8.0f;
        [SerializeField] private float _acceleration = 16.0f;
        [SerializeField] private float _runningBoost = 1.5f;

        private NavMeshAgent _navMeshAgent;
        private Transform _camTransform;

        public void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _camTransform = Camera.main.transform;
        }
        
        #endregion
        
        #region Movement

        public void Update()
        {
            Move();
        }

        private void Move()
        {
            if (InputManager.Instance.IsRunning())
            {
                _navMeshAgent.speed = _speed * _runningBoost;
                _navMeshAgent.acceleration = _acceleration * _runningBoost;
            }
            else
            {
                _navMeshAgent.speed = _speed;
                _navMeshAgent.acceleration = _acceleration;
            }
            
            var input = InputManager.Instance.GetMove();
            var currentPos = transform.position;
            var destination = currentPos +
                                    _camTransform.right * (input.x * _sensibilityX) +
                                    _camTransform.forward * (input.y * _sensibilityY);
            _navMeshAgent.destination = destination;
        }
        
        #endregion
    }
}