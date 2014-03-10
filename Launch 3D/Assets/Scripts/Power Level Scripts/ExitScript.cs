using UnityEngine;
using System.Collections;

public class ExitScript : MonoBehaviour {

	public GameObject PLS;
	private PowerLevelStart PLSscript;

	void Start() {
		PLSscript = PLS.GetComponent<PowerLevelStart> ();
	}

	public void Exit() {
		PLSscript.EndMinigame();
	}
}
