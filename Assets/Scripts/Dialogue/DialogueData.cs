using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Dialogue/DialogueData")]

// stores dialogue lines
public class DialogueData : ScriptableObject
{
    [TextArea] public string[] lines;
}