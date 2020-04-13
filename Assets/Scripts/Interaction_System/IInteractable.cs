namespace Destination
{
    public interface IInteractable
    {
        // Interface only accepts properties not variables
        float HoldDuration { get; }

        bool HoldInteract { get; }

        bool IsInteractable { get; }

        string ToolTip { get; }

        void OnInteract();
    }
}