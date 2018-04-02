using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public int slotsX;
	public int slotsY;
	public GUISkin skin;
	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item>();
	private bool showInventory;
	private ItemDatebase database;
	private bool showTooltip;
	private string tooltip;

	private bool draggingItem;
	private Item draggedItem;
	private int prevIndex;




	// Use this for initialization
	void Start () {
		for (int i = 0;i < (slotsX * slotsY); i++)
		{
			slots.Add(new Item());
			inventory.Add (new Item());
		}
		database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDatebase>();
		AddItem (0);
		AddItem(1);
		//RemoveItem (0);

	}

	void Update()
	{
		if (Input.GetButtonDown ("Inventory")) 
		{
			showInventory = !showInventory;
		}
	}


	void OnGUI () 
	{
		tooltip = "";
		GUI.skin = skin;
		if (showInventory)
		{
			DrawInventory();
			if (showTooltip)
				GUI.Box (new Rect(Event.current.mousePosition.x +15f, Event.current.mousePosition.y, 200, 200), tooltip, skin.GetStyle("Tooltip"));
		}
		if(draggingItem)
		{
			GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.itemIcon);
		}

	}

	void DrawInventory()
	{
		Event e = Event.current;
		int i = 0;
		for (int y = 0; y < slotsY; y++)
		{
			for (int x = 0; x < slotsX; x++)
			{
				Rect slotRect = new Rect (x * 60, y* 60, 50,50);
				GUI.Box(slotRect, "", skin.GetStyle("slot"));
				slots[i] = inventory[i];
				if(slots[i].itemName != null)
				{
					GUI.DrawTexture(slotRect, slots[i].itemIcon);
					if (slotRect.Contains(e.mousePosition))
					{
						tooltip = CreateTooltip (slots[i]);
						showTooltip = true;
						if(e.button == 0  && e.type == EventType.mouseDrag  && !draggingItem)
						{
							draggingItem = true;
							prevIndex = i;
							draggedItem = slots[i];
							inventory[i] = new Item();
						}
						if(e.type == EventType.mouseUp && draggingItem)
						{
							inventory[prevIndex] = inventory[i];
							inventory[i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
					}

				}
				else 
				{
					if(slotRect.Contains(e.mousePosition))
					{
						if(e.type == EventType.mouseUp && draggingItem)
						{
							inventory[i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
					}
				}
				if(tooltip == "")
				{
					showTooltip = false;
				}
				i++;
			}
		}
	}

	string CreateTooltip(Item item)
	{
		tooltip = "<color=#4DA4BF>" + item.itemName + "</color>\n\n" + "<color=#f2f2f2>" + item.itemDesc + "</color>\n";
		return tooltip;
	}


	void RemoveItem(int id)
	{
		for (int i=0; i < inventory.Count; i++) 
		{
			if (inventory[i].itemId == id)
			{
				inventory[i] = new Item();
				break;
			}
		}
	}



	void AddItem(int id)
	{
		for (int i = 0; i < inventory.Count; i++)
		{
			if (inventory[i].itemName == null)
			{
				for(int j = 0; j < database.items.Count; j++)
				{
					if(database.items[j].itemId == id)
					{
						inventory[i] = database.items[j];
					}
				}
				break; 
			}
		}
	}


	 bool InventoryContains(int id)
	{
		bool result = false;
		for (int i = 0; i < inventory.Count; i++)
		{
			result = inventory[i].itemId == id;
			if (result)
			{
				break;
			}
		}
		return result;
	}
}					