using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LocalizationManager : MonoBehaviour
{
	public static LocalizationManager instace;

	[SerializeField] private List<LocalizableUI> localizableUIs;
	[SerializeField] private Deftsoft.LocalizationLibrary library;

	private void Awake()
	{
		instace = this;
	}

	public void SetLanguage(string languageID)
	{
		foreach (var item in localizableUIs)
		{
			var langDictEntry = library.GetLanguageDictionary(languageID, item.localizationID);
			if (langDictEntry != null)
			{
				switch (langDictEntry.uiType)
				{
					case Deftsoft.LanguageDictionary.UIType.Text:
						if (item.textComponent != null)
						{
							item.textComponent.gameObject.SetActive(true);
							item.textComponent.text = langDictEntry.textValue;
						}
						if (item.imageComponent != null)
						{
							item.imageComponent.gameObject.SetActive(false);
						}
						break;

					case Deftsoft.LanguageDictionary.UIType.Image:
						if (item.imageComponent != null)
						{
							item.imageComponent.gameObject.SetActive(true);
							item.imageComponent.sprite = langDictEntry.imageValue;
						}
						if (item.textComponent != null)
						{
							item.textComponent.gameObject.SetActive(false);
						}
						break;
				}
			}
		}
	}
}

[System.Serializable]
public class LocalizableUI
{
	public Text textComponent;
	public Image imageComponent;
	public string localizationID;
}
