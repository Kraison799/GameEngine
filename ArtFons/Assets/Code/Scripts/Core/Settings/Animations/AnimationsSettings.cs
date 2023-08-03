using System;
using System.Collections.Generic;
using Code.Scripts.Core.DesignPatterns.Singleton;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Code.Scripts.Core.Settings.Animations
{
    public class AnimationsSettings : Singleton<AnimationsSettings>
    {
        #region Set Up Animations
        
        private List<AnimationInfo> _animations;

        public AnimationsSettings()
        {
            // Init list
            _animations = new List<AnimationInfo>();
        }

        protected void Awake()
        {
            // Animations
            _animations.Add(new AnimationInfo("Roll", 1.10f, 0.10f));
        }

        #endregion
        
        #region Info

        public AnimationInfo GetAnim(string name)
        {
            for (int i = 0; i < _animations.Count; i++)
            {
                if (_animations[i].GetName().Equals(name)) return _animations[i];
            }

            Debug.LogError($"AnimationSetting: Animation {name} not found!");
            throw new Exception($"AnimationSetting: Animation {name} not found!");
        }
        
        #endregion
    }
}