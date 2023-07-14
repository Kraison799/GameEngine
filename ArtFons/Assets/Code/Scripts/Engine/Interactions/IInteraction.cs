using Code.Scripts.Core.Enums.Interactions;

namespace Code.Scripts.Engine.Interactions
{
    public interface IInteraction
    {
        public void Interact();
        public void Complete();
        public void Reset();
        public bool IsHighlighted();
        public void ToggleHighlight();
    }
}
