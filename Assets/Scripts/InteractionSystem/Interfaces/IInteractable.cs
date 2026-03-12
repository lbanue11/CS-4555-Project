using UnityEngine;

// followed https://www.youtube.com/watch?v=FE0lJljavAM

public interface IInteractable
{
    
    Transform transform { get; }
    
    string DisplayName { get; }

    bool CanInteract();
    
    void Interact();

    void OnFocusGained();
    
    void OnFocusLost();

}
