using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item  {
		public string itemName;
		public int itemId;
		public string itemDesc;
		public Texture2D itemIcon;
		public int ItemPower;
		public int itemSpeed;
		public ItemType itemType;

		public enum ItemType{
			Weapon,
			Consumable,
			Quest
	}

	public Item(string name, int id, string desc, int power, int speed, ItemType type)
	{
		itemName = name;
		itemId = id;
		itemDesc = desc;
		itemIcon = Resources.Load<Texture2D>("Item Icons/" + name);
		ItemPower = power;
		itemSpeed = speed;
		itemType = type;
	}
	public Item()
	{
	}

}
