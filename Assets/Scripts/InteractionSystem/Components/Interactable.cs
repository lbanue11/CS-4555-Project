using UnityEngine;
using UnityEngine.Events;

// Followed https://www.youtube.com/watch?v=FE0lJljavAM

public class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private string displayName = "Interact";
    [SerializeField] private bool isEnabled = true;
    [SerializeField] private UnityEvent OnInteract;
    [SerializeField] private Color outlineColor = Color.blue;
    
    public string DisplayName => displayName;
    public bool CanInteract() => isEnabled;
    
    private Outline outline;
    
    private void Awake()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = outlineColor;
        outline.OutlineWidth = 1f;
        outline.enabled = false;
    }
    

    public void Interact()
    {
        OnInteract?.Invoke();
    }

    public void OnFocusGained()
    {
        outline.enabled = true;
    }

    public void OnFocusLost()
    {
        outline.enabled = false;
    }
}
