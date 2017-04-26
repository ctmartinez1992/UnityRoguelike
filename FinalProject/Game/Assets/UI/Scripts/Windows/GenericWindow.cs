using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GenericWindow : MonoBehaviour {

	public static WindowManager manager;

	public GameObject firstSelected;

	public Windows nextWindow;
	public Windows prevWindow;

	protected EventSystem eventSystem {
		get { return GameObject.Find("EventSystem").GetComponent<EventSystem>(); }
	}

	public virtual void OnFocus() {
		eventSystem.SetSelectedGameObject(firstSelected);
	}

	protected virtual void Display(bool value) {
		gameObject.SetActive(value);
	}

	public virtual void Open() {
		Display(true);
		OnFocus();
	}

	public virtual void Close() {
		Display(false);
	}

	public void OnNextWindow() {
		manager.Open((int)nextWindow - 1);
	}

	public void OnPreviousWindow() {
		manager.Open((int)prevWindow - 1);
	}

	protected virtual void Awake() {
		Close();
	}
}
