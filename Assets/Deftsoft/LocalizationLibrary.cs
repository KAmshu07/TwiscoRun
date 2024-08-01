using UnityEngine;
using System.Collections.Generic;

namespace Deftsoft
{
	[CreateAssetMenu(fileName = "LocalizationLibrary", menuName = "Deftsoft/LocalizationLibrary", order = 1)]
	public class LocalizationLibrary : ScriptableObject
	{
		[SerializeField] private List<LanguageData> languageData;

		// Method to get language dictionary entry
		public LanguageDictionary GetLanguageDictionary(string selectedLanguageID, string key)
		{
			var selectedLanguage = languageData.Find(lang => lang.id == selectedLanguageID);
			return selectedLanguage?.dictionary.Find(pair => pair.key == key);
		}
	}

	[System.Serializable]
	public class LanguageData
	{
		public string id;
		public List<LanguageDictionary> dictionary;
	}

	[System.Serializable]
	public class LanguageDictionary
	{
		public enum UIType { Text, Image }
		public string key;
		public UIType uiType;
		public string textValue;
		public Sprite imageValue;
	}
}
