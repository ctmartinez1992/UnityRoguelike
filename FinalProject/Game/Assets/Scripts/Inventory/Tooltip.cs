using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    private Item item;
    private string data;
    private GameObject tooltip;

    private void Start() {
        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);               //So it doesn't show by default. Can't be done in Unity's UI otherwise we can't find it.
    }

    private void Update() {
        if(tooltip.activeSelf) {
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void Activate(Item item) {
        this.item = item;
        ConstructDataString();

        tooltip.SetActive(true);
    }

    public void Deactivate() {
        tooltip.SetActive(false);
    }

    public void ConstructDataString() {
        data = "<color=#000000><b>" + item.Name + "</b></color>\n";
        data += item.Description;

        tooltip.transform.FindChild("Text").GetComponent<Text>().text = data;
    }
}
