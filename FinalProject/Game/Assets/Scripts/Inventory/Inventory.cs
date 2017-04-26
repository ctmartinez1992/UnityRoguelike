using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    private ItemDB db;

    private GameObject inventoryPanel;
    private GameObject slotPanel;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    public List<Item> items = new List<Item>();
    private int slotAmount;
    public List<GameObject> slots = new List<GameObject>();

    private void Start() {
        db = GetComponent<ItemDB>();

        inventoryPanel = GameObject.Find("InventoryPanel");
        slotPanel = inventoryPanel.transform.FindChild("SlotPanel").gameObject;

        slotAmount = 16;
        for(int i = 0; i < slotAmount; ++i) {
            slots.Add(Instantiate(inventorySlot));
            slots[i].GetComponent<Slot>().id = i;
            slots[i].transform.SetParent(slotPanel.transform);

            items.Add(new Item());
        }

        AddItem(0);
        AddItem(2);
        AddItem(3);
        AddItem(1);
        AddItem(1);
        AddItem(1);
        AddItem(1);

        inventoryPanel.SetActive(false);
    }

    public void AddItem(int id) {
        Item item = db.FetchItemByID(id);

        if(item.Stackable && IsItemInInventory(item)) {
            for(int i = 0; i < items.Count; ++i) {
                if(items[i].ID == id) {
                    ItemData data = slots[i].transform.FindChild(items[i].Name).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.FindChild("AmountText").GetComponent<Text>().text = data.amount.ToString();

                    break;
                }
            }
        }
        else {
            for(int i = 0; i < items.Count; ++i) {
                if(items[i].ID == -1) {
                    items[i] = item;

                    GameObject itemGO = Instantiate(inventoryItem);
                    itemGO.transform.SetParent(slots[i].transform);
                    itemGO.transform.position = Vector2.zero;
                    itemGO.GetComponent<Image>().sprite = item.Sprite;
                    itemGO.GetComponent<ItemData>().item = item;
                    itemGO.GetComponent<ItemData>().slot = i;
                    itemGO.GetComponent<ItemData>().amount = 1;
                    itemGO.name = item.Name;

                    break;
                }
            }
        }
    }

    public bool IsItemInInventory(Item item) {
        for(int i = 0; i < items.Count; ++i) {
            if(items[i].ID == item.ID) {
                return(true);
            }
        }

        return(false);
    }

    public void Activate() {
        inventoryPanel.SetActive(true);
    }

    public void Deactivate() {
        inventoryPanel.SetActive(false);
    }

    public void Toggle() {
        if(inventoryPanel.activeSelf) {
            Deactivate();
        }
        else {
            Activate();
        }
    }
}
