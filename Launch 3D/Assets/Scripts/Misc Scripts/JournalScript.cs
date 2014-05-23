using UnityEngine;
using System.Collections;

/// <summary>
/// Holds a list of item names and descriptions that have been saved to the log
/// </summary>
public class JournalScript : MonoBehaviour {

	private static GameObject journal;

	void Start()
	{
		journal = GameObject.Find("Journal");
	}

	//Adds an item to the journal
	public static void addItem(string description)
	{
		//journal.GetComponent<dfListbox>().AddItem(description);
	}


}
