using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject cat;

	// Use this for initialization
	void Start () {
		InvokeRepeating("spawn", 0f, 0.5f);
	}

	void spawn()
	{
		Instantiate(cat, new Vector3(Random.Range(-5f, 5f), 10, -1), Quaternion.AngleAxis(Random.Range(-360, 360), Vector3.forward));
	}

	// Update is called once per frame
	void Update () {
	}
}
