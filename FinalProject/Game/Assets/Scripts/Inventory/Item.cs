using UnityEngine;

public enum ITEM_TYPE {
    UNKNOWN = 0,
    WEAPON = 1,
    ARMOR = 2,
    USABLE = 3
};

public enum ARMOR_SLOT {
    UNKNOWN = 0,
    HEAD = 1,
    ARMS = 2,
    BODY = 3,
    LEGS = 4,
    FEET = 5
};

public class Item {

    public ITEM_TYPE ItemType { get; set; }     //Identifies the type of item it is in a more general sense.
    public int ID { get; set; }                 //Uniquely identifies the type of this item.
    public string Name { get; set; }            //The name of the item.
    public string Slug { get; set; }            //The name with underscores.
    public string Description { get; set; }     //A description of the item that better explains what it is.
    public int Rarity { get; set; }             //Rarity of the item: 0 = junk, 1 = common, 2 = uncommon, 3 = rare, 4 = epic, 5 = legendary.
    public int Value { get; set; }              //Monetary value of the item.
    public bool Stackable { get; set; }         //Can this item be stacked with items of the same type.
    public int StackLimit { get; set; }         //If this item is stackable, then this variable will set a limit to the amount of items that can be stacked. If not stackable, this variable matters not.

    public Sprite Sprite { get; set; }          //Image that represents the item.

    public Item() {
        this.ItemType = ITEM_TYPE.UNKNOWN;
        this.ID = -1;
        this.Name = "Default Name";
        this.Slug = "default_slug";
        this.Description = "Default Description";
        this.Rarity = -1;
        this.Value = -1;
        this.Stackable = false;
        this.StackLimit = -1;
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + Slug);
    }

    public Item(ITEM_TYPE itemType, int id, string name, string slug, string description, int rarity, int value, bool stackable, int stackLimit) {
        this.ItemType = itemType;
        this.ID = id;
        this.Name = name;
        this.Slug = slug;
        this.Description = description;
        this.Rarity = rarity;
        this.Value = value;
        this.Stackable = stackable;
        this.StackLimit = stackLimit;
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + Slug);
    }
}

public class ItemWeapon : Item {

    public int Damage;

    public ItemWeapon() :
        base()
    {
        this.Damage = 0;
    }

    public ItemWeapon(ITEM_TYPE itemType, int id, string name, string slug, string description, int rarity, int value, bool stackable, int stackLimit, int damage) :
        base(itemType, id, name, slug, description, rarity, value, stackable, stackLimit)
    {
        this.Damage = damage;
    }
}

public class ItemArmor : Item {

    public int Armor;
    public ARMOR_SLOT ArmorSlot;

    public ItemArmor() :
        base()
    {
        this.Armor = 0;
        this.ArmorSlot = ARMOR_SLOT.UNKNOWN;
    }

    public ItemArmor(ITEM_TYPE itemType, int id, string name, string slug, string description, int rarity, int value, bool stackable, int stackLimit, int armor, ARMOR_SLOT armorSlot) :
        base(itemType, id, name, slug, description, rarity, value, stackable, stackLimit)
    {
        this.Armor = armor;
        this.ArmorSlot = armorSlot;
    }
}