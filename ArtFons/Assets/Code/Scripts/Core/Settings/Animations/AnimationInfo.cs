namespace Code.Scripts.Core.Settings.Animations
{
    public class AnimationInfo
    {
        private string _name;
        private float _duration;
        private float _cooldown;

        public AnimationInfo(string name, float duration, float cooldown)
        {
            _name = name;
            _duration = duration;
            _cooldown = cooldown;
        }
        #region Getters & Setters

        public string GetName()
        {
            return _name;
        }

        public float GetDuration()
        {
            return _duration;
        }
        
        public float GetCooldown()
        {
            return _cooldown;
        }
        
        public float GetFixedCooldown()
        {
            return _duration + _cooldown;
        }

        #endregion
    }
}