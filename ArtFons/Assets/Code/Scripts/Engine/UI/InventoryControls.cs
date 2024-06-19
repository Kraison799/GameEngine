using System;
using System.Collections.Generic;
using Code.Scripts.Core.DesignPatterns.Singleton;
using Code.Scripts.Core.Enums.States.Input;
using Code.Scripts.Core.Enums.UI;
using Code.Scripts.Engine.Controls.InputManager;
using UnityEngine;

namespace Code.Scripts.Engine.UI
{
    public class InventoryControls : Singleton<InventoryControls>
    {
        [SerializeField] private GameObject _general;
        [SerializeField] private GameObject _inventory;
        [SerializeField] private GameObject _loot;
        private List<GameObject> _actives;

        public void Start()
        {
            _actives = new List<GameObject>()
            {
                _general
            };
        }

        public void Update()
        {
            if (InputManager.Instance.ToggleInventory())
            {
                if (InputManager.Instance.GetState() == InputState.Third)
                {
                    ActivateUI(new List<InventorySection>()
                    {
                        InventorySection.Inventory
                    });
                    InputManager.Instance.SetState(InputState.UI);
                }
                else if (InputManager.Instance.GetState() == InputState.UI)
                {
                    DeactivateUI();
                    InputManager.Instance.SetState(InputState.Third);
                }
            }
        }

        public void ActivateUI(List<InventorySection> sections)
        {
            foreach (var section in sections)
            {
                switch (section)
                {
                    case InventorySection.Inventory:
                        _actives.Add(_inventory);
                        break;
                    case InventorySection.Loot:
                        _actives.Add(_loot);
                        break;
                }
            }

            foreach (var section in _actives)
            {
                section.SetActive(true);
            }
        }

        public void DeactivateUI()
        {
            foreach (var section in _actives)
            {
                section.SetActive(false);
            }

            _actives = new List<GameObject>()
            {
                _general
            };
        }
    }
}
