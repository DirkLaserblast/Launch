using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlotEvent : Inventory 
{
	public int slotNumber;
	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
		// Add event handler code here
		if (Inventory.inv [slotNumber] != null && Inventory.inv.Count != 0 && slotNumber+1 <= Inventory.inv.Count)
						Inventory.inv [slotNumber].gameObject.SendMessage ("DropItem");
				else
						print ("NO OBJ");

	}

}
