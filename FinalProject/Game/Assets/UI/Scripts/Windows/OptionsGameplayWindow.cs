using UnityEngine;
using System.Collections;

public class OptionsGameplayWindow : GenericWindow {

	public string defaultLanguage;
	public string defaultLoseMoneyOnDeath;

	public Selectable languageSelectable;
	public Selectable loseMoneyOnDeathSelectable;

	public void OnDefault() {
		languageSelectable.SetCurrentOptionByText(defaultLanguage);
		loseMoneyOnDeathSelectable.SetCurrentOptionByText(defaultLoseMoneyOnDeath);
	}
}
