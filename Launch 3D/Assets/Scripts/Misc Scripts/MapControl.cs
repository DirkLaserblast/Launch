using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapControl : MonoBehaviour {

	public GameObject[] Arrows = new GameObject[8];
	private int activeIndex = 0;

	public void MovedToSector(int index) {
		Arrows[index].SetActive(true);
		Arrows[activeIndex].SetActive(false);
		activeIndex = index;
	}
}
