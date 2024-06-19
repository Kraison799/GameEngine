using System;
using Code.Scripts.Engine.Controls.InputManager;
using UnityEngine;

namespace Code.Scripts.Engine.Interactions
{
    public class InteractionSystem : MonoBehaviour
    {
        [SerializeField] private float _range = 2.0f;
        
        public void Update()
        {
            if (InputManager.Instance.GetInteract())
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, _range);
                foreach (var collider in colliders)
                {
                    if (collider.TryGetComponent(out IInteraction interaction))
                    {
                        Debug.Log("Successfully found interaction");
                        interaction.Interact();
                    }
                }
            }
        }
    }
}