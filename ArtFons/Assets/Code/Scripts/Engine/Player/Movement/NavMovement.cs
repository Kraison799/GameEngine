using Code.Scripts.Engine.Controls.InputManager;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Scripts.Engine.Player.Movement
{
    public class NavMovement: MonoBehaviour
    {
        #region Set Up

        private float _sensibilityX = 2.0f;
        private float _sensibilityY = 2.0f;
        private float _speed = 6.0f;
        private float _rotSpeed = 280.0f;
        private float _acceleration = 120.0f;
        private float _runningBoost = 1.75f;

        private float _dash;
        private float _dashDistance = 8.0f;

        private NavMeshAgent _navMeshAgent;
        private Transform _camTransform;

        public void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _camTransform = Camera.main.transform;
            _dash = 0.0f;
        }
        
        #endregion
        
        #region Movement

        public void Update()
        {
            if (_dash > 0.0f) _dash -= Time.deltaTime;
            
            Move();
        }

        private void Move()
        {
            if (_dash <= 0.0f)
            {
                if (InputManager.Instance.IsRunning())
                {
                    _navMeshAgent.speed = _speed * _runningBoost;
                    _navMeshAgent.acceleration = _acceleration * _runningBoost;
                }
                else if (InputManager.Instance.IsWalking())
                {
                    _navMeshAgent.speed = _speed;
                    _navMeshAgent.acceleration = _acceleration;
                }
                
                var currentPos = transform.position;
                Vector3 destination;
                
                if (InputManager.Instance.GetRoll())
                {
                    _dash = 0.80f;
                    destination = currentPos +
                                  transform.forward * _dashDistance;
                    _navMeshAgent.speed = _speed * _runningBoost * 1.25f;
                    _navMeshAgent.acceleration = _acceleration * _runningBoost * 1.25f;
                }
                else
                {
                    var input = InputManager.Instance.GetMove();
                    destination = currentPos +
                                  _camTransform.right * (input.x * _sensibilityX) +
                                  _camTransform.forward * (input.y * _sensibilityY);    
                }
    
                _navMeshAgent.destination = destination;
            }
        }

        private void RollForward()
        {
            
        }
        
        #endregion
    }
}