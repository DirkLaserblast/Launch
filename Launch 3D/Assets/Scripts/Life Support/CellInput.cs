using UnityEngine;
using System.Collections;

public class CellInput : MonoBehaviour {

	public Camera minigamecamera;
	private const int WIDTH = 21;
	private const int HEIGHT = 9;
	private const int CELL_SIZE = 64;
	private dfPanel sprite;
	private Vector3 origin;
	
	void Start () {
		sprite = transform.GetComponent<dfPanel> ();
		origin = minigamecamera.WorldToScreenPoint (transform.position);
	}
	

	void Update () {
		sprite.ZOrder = WIDTH * HEIGHT;
	}

	void OnMouseDrag() {

	}

	void OnMouseDown() {
		//print (origin);
		//print (Input.mousePosition);
		//print (MouseToCell(Input.mousePosition).transform.position);
		MouseToCell (Input.mousePosition);
	}

	Cell MouseToCell(Vector3 pos) {
		Vector3 newpos = new Vector3 ((int) (pos.x - origin.x) , (int) (origin.y - pos.y) , 0);
		print (newpos);
		CellSpawner spawner = transform.parent.GetComponent<CellSpawner> ();
		GameObject cellobj = spawner.getCell((int) newpos.x, (int) newpos.y);
		return cellobj.GetComponent<Cell>();

	}
}
