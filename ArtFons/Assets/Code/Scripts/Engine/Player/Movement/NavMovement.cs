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
            var input = InputManager.Instance.GetMove();
            Debug.Log(input);
            var currentPos = transform.position;
            var destination = currentPos +
                                    _camTransform.right * (input.x * _sensibilityX) +
                                    _camTransform.forward * (input.y * _sensibilityX);
            _navMeshAgent.destination = destination;
        }
        
        #endregion
    }
}