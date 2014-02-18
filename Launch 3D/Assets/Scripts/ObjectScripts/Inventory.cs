using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {  
	protected static List<Transform> inv = new List<Transform>();

	public static ArrayList inventoryObjects
	{
		get
		{
			ArrayList inventory = new ArrayList();
			foreach (Transform trans in inv)
			{
				inventory.Add(trans.gameObject);
			}
			return inventory;
		}
		set
		{
			List<Transform> inventory = new List<Transform>();
			foreach (GameObject go in value)
			{
				inventory.Add(go.GetComponent<Transform>());
			}
			inv = inventory;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("i")) {
			for(int i = 0; i < inv.Count; i ++){
				print("in: " + inv[i]);
			}
		}

	}

	public static void addItem(Transform item){
		inv.Add (item);

	}

	public static void RemoveItem(Transform item){// does not drop
		int index = 0;bool finished = false;
		foreach (Transform i in inv) {
			if(i == item){
				inv.RemoveAt(index);
				finished = true;
			}
			index ++;
			if(finished){
				return;
			}
		}
	}

	public static void RemoveAt(int index) {
		inv.RemoveAt (index);
	}

	public static void dropItem(Transform item){// drop
		RemoveItem (item.transform);
	}

	public static bool checkItem(int index) {
		return inv.Count > index;
	}

	public static Transform getItem(int index) {
		return inv[index];
	}

}
