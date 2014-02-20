using UnityEngine;
using System.Collections;

public class AirVentScript : MonoBehaviour {
	
	public GameObject cleanAirFilter;
	public GameObject dirtyAirFilter;
	public float dirtyFilterDuration = 2f;
	public float cleanFilterDuration = 2f;
	public float transitionDuration = 0.5f;
	private ItemReceive itemReceive;
	private Vector3 dirtyStep = new Vector3(0f, 0f, -0.172f);
	private Vector3 cleanStep = new Vector3(0f, 0f, 0.172f);



	void Start () {
		cleanAirFilter.SetActive(false);
		itemReceive = this.gameObject.GetComponent<ItemReceive>();
	}
	
	void Update () {
		if(itemReceive.received && dirtyFilterDuration >= 0) {
			AnimateDirtyFilter();
		}
		if(dirtyFilterDuration <= 0 && cleanFilterDuration >= 0) {
			AnimateCleanFilter();
		}
	}

	void AnimateDirtyFilter() {
		dirtyFilterDuration -= Time.deltaTime;
		dirtyAirFilter.transform.Translate(dirtyStep * Time.deltaTime);
		if(dirtyFilterDuration <= 0) {
			dirtyAirFilter.rigidbody.useGravity = true;
			dirtyAirFilter.gameObject.GetComponent<Item>().isPickable = true;
		}
	}

	void AnimateCleanFilter() {
		if (transitionDuration >= 0) {
			transitionDuration -= Time.deltaTime;
			if(transitionDuration <= 0) {
				cleanAirFilter.SetActive(true);
			}
		} else {
			cleanFilterDuration -= Time.deltaTime;
			cleanAirFilter.transform.Translate (cleanStep * Time.deltaTime);
		}	
	}
}
