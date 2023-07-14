using System.Collections.Generic;
using Code.Scripts.Core.Enums.Interactions;
using Code.Scripts.Core.Enums.States.Input;
using Code.Scripts.Core.Enums.UI;
using Code.Scripts.Engine.Controls.InputManager;
using Code.Scripts.Engine.UI;
using UnityEngine;

namespace Code.Scripts.Engine.Interactions.Categories
{
    public class Loot : MonoBehaviour, IInteraction
    {
        [SerializeField] private Category _category;
        [SerializeField] private Status _status;
        private bool _highlighted;
        
        public void Interact()
        {
            Debug.Log("Open loot inventory");
            if (_status == Status.Active)
            {
                InventoryControls.Instance.ActivateUI(new List<InventorySection>()
                {
                    InventorySection.Inventory,
                    InventorySection.Loot
                });
                InputManager.Instance.SetState(InputState.UI);
            }
        }

        public void Complete()
        {
            _status = Status.Completed;
        }

        public void Reset()
        {
            _status = Status.Active;
        }

        public bool IsHighlighted()
        {
            return _highlighted;
        }

        public void ToggleHighlight()
        {
            _highlighted = !_highlighted;
        }
    }
}