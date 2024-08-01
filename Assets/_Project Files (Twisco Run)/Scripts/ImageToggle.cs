using UnityEngine;
using UnityEngine.UI;

public class ImageToggle : MonoBehaviour
{
	public InputField inputField;
	public Image imageToToggle;

	void Start()
	{
		// Ensure that the inputField and imageToToggle are properly assigned in the inspector.
		if (inputField == null || imageToToggle == null)
		{
			Debug.LogError("InputField or ImageToToggle not assigned!");
			return;
		}

		// Subscribe to the input field's OnValueChanged event.
		inputField.onValueChanged.AddListener(OnInputFieldValueChanged);

		// Initial check and setup based on the initial input field value.
		OnInputFieldValueChanged(inputField.text);
	}

	private void OnInputFieldValueChanged(string newText)
	{
		// Check if the input field is empty.
		bool isInputEmpty = string.IsNullOrEmpty(newText);

		// Set the image's active state based on the input field's content.
		if(imageToToggle.sprite != null)
		{
		imageToToggle.gameObject.SetActive(isInputEmpty);
		}
	}
}
