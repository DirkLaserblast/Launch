using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlotEvent : Inventory 
{
	public int slotNumber;
	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
		// Add event handler code here

		Inventory.inv[slotNumber].gameObject.SendMessage ("DropItem");

	}

}
