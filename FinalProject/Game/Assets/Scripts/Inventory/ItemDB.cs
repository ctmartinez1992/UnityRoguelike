using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;

public class ItemDB : MonoBehaviour {
    
    private List<Item> db = new List<Item>();
    private JsonData itemData;

    private void Start() {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/items.json"));
        ConstructItemDB();
    }

    private void ConstructItemDB() {
        for(int i = 0; i < itemData.Count; ++i) {
            ITEM_TYPE itemType = (ITEM_TYPE)((int)itemData[i]["item_type"]);

            if(itemType == ITEM_TYPE.WEAPON) {
                db.Add(new ItemWeapon((ITEM_TYPE)((int)itemData[i]["item_type"]), (int)itemData[i]["id"], itemData[i]["name"].ToString(), itemData[i]["slug"].ToString(), itemData[i]["description"].ToString(),
                                      (int)itemData[i]["rarity"], (int)itemData[i]["value"], (bool)itemData[i]["stackable"], (int)itemData[i]["stack_limit"],
                                      (int)itemData[i]["damage"]));
            }
            else if(itemType == ITEM_TYPE.ARMOR) {
                db.Add(new ItemArmor((ITEM_TYPE)((int)itemData[i]["item_type"]), (int)itemData[i]["id"], itemData[i]["name"].ToString(), itemData[i]["slug"].ToString(), itemData[i]["description"].ToString(),
                                     (int)itemData[i]["rarity"], (int)itemData[i]["value"], (bool)itemData[i]["stackable"], (int)itemData[i]["stack_limit"],
                                     (int)itemData[i]["armor"], (ARMOR_SLOT)((int)itemData[i]["armor_slot"])));
            }
        }
    }

    public Item FetchItemByID(int id) {
        for(int i = 0; i < itemData.Count; ++i) {
            if(db[i].ID == id) {
                return(db[i]);
            }
        }

        return(null);
    }
}