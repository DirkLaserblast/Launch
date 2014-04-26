using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerControl : MonoBehaviour {

	public PowerManager powerManager;
	public List<OnOffSector> Sector = new List<OnOffSector>();
	public List<DoorScript> Doors = new List<DoorScript>();
	private bool modifiedLocks = false;
	private bool modifiedPower = false;
	private bool decrementedHere = false;
	
	public void Trigger() {
		for(int i = 0; i < Sector.Count; i++) {
			if (Sector[i].isOn) {
				Sector[i].ToggleOff();
				//LockDoors();
				IncrementPower();
			} else {
				if(powerManager.CurrentPower > 0 || decrementedHere) {
					//UnlockDoors();
					Sector[i].ToggleOn();
					DecrementPower();
				}
			}
		}
		modifiedLocks = false;
		modifiedPower = false;
		decrementedHere = false;
	}

	void LockDoors() {
		if(!modifiedLocks) {
			for (int i = 0; i < Doors.Count; i++) {
				Doors[i].locks++;
				Doors[i].isLocked = true;
			}
		}
	}

	void UnlockDoors() {
		if(!modifiedLocks) {
			for (int i = 0; i < Doors.Count; i++) {
				Doors[i].locks--;
				if(Doors[i].locks <= 0) {
					Doors[i].isLocked = false;
				}
			}
		}
	}

	void IncrementPower() {
		if(!modifiedPower) {
			powerManager.CurrentPower++;
			modifiedPower = true;
		}
	}

	void DecrementPower() {
		if(!modifiedPower) {
			powerManager.CurrentPower--;
			decrementedHere = true;
			modifiedPower = true;
		}
	}
}
