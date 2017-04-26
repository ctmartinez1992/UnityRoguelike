using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {

    public Item item;
    public int amount;
    public int slot;

    private Vector2 offset;
    private Inventory inventory;
    private Tooltip tooltip;

    private void Start() {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = inventory.GetComponent<Tooltip>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if(item != null) {
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
            this.transform.SetParent(this.transform.parent.parent);
            this.transform.position = eventData.position;
            this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if(item != null) {
            this.transform.position = eventData.position - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        if(item != null) {
            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
            this.transform.SetParent(inventory.slots[slot].transform);
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        if(item != null) {
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        tooltip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltip.Deactivate();
    }
}
