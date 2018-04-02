using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatebase : MonoBehaviour {
	public List<Item> items = new List<Item>();

	void Start()
	{
		items.Add (new Item ("Wooden Sword",0,"A wooden sword", 1,1, Item.ItemType.Weapon));
		items.Add (new Item ("Stone Sword",1,"A stone sword", 2,2, Item.ItemType.Weapon));
	}




}
