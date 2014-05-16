using UnityEngine;
using System.Collections;

public class MapControlTrigger : MonoBehaviour {

	public MapControl map;
	public int sector;

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			map.MovedToSector(sector);
		}
	}
}
