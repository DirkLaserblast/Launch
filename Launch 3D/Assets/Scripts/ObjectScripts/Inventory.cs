using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {
	protected static ArrayList inv  = new ArrayList(); 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("i")) {
			for(int i = 0; i < inv.Count; i ++){
				print(inv[i]);
			}
		}


	}
}
