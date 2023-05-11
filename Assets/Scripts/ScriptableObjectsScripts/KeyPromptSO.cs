using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/KeyPrompt")]
public class KeyPromptSO : ScriptableObject
{
	[field: SerializeField]
	public string Verb { get; set; }

	[field: SerializeField]
	public Sprite Icon { get; set; }

	[field: SerializeField]
	public string Text { get; set; }
}
