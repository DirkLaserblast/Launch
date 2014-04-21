using UnityEngine;
using System.Collections;

public class CellSpawner : MonoBehaviour {

	/* Overall Technical Design: 
	 * The CellSpawner class is the controller for the game.
	 * Based on player input this class will send commands to
	 * the appropriate cells to perform various actions (e.g.
	 * displaying sprites and the like).
	 * 
	 * Input comes in via another object -- 
	 * */



	private const int WIDTH = 21; //cell width/height
	private const int HEIGHT = 9;
	private const int CELL_SIZE = 64;
	private float width; //screen width/height
	private float height;
	private Vector2 origin;
	private dfPanel panel;
	public Transform prefab;
	public GameObject gameParent;
	public Camera camera;
	Cell prevCell;
	GameObject[,] grid;

	void Start () {
		panel = transform.GetComponent<dfPanel> ();
		width = panel.GetScreenRect().width;
		height = panel.GetScreenRect().height;
		origin = new Vector2 (panel.GetScreenRect ().x, Screen.height - panel.GetScreenRect ().y );
		for(int i = 0; i < WIDTH*HEIGHT; i++) {
			int instanceID = Instantiate(prefab, transform.position, transform.rotation).GetInstanceID();
		}
		grid = new GameObject[WIDTH, HEIGHT];
		GameObject[] instancesArr = GameObject.FindGameObjectsWithTag("LS");
		for(int i = 0; i < WIDTH; ++i){
			for(int j = 0; j < HEIGHT; j++) {
				grid[i, j] = instancesArr[i+j*WIDTH];
				instancesArr[i+j*WIDTH].transform.parent = transform;
				Cell sprite = instancesArr[i+j*WIDTH].GetComponent<Cell>();
				sprite.MoveTo(i*CELL_SIZE, j*CELL_SIZE);
			}
		}
		gameParent.SetActive (false);
	}
	
	void Update () {

	}

	void OnMouseMove() { //USING DF GUI's EVENTS, SINCE THEY FUCK UP NORMAL FUCKING ONMOUSE SHIT
		print (Input.GetMouseButton (0));
		if (Input.GetMouseButton(0)) {
			Cell currentCell = MouseToCell (Input.mousePosition);
			if (prevCell != null) {
				if(currentCell != prevCell) {
					if(prevCell.HasFan && !currentCell.HasFan) {
						prevCell.HasFan = false;
						currentCell.HasFan = true;
					}
				}
			}
			prevCell = currentCell;
		}
	}

	void OnMouseDown() {
//		Cell currentCell = MouseToCell (Input.mousePosition);
//		if (prevCell != null) {
//			if(currentCell != prevCell) {
//				if(prevCell.HasFan && !currentCell.HasFan) {
//					prevCell.HasFan = false;
//					currentCell.HasFan = true;
//				}
//			}
//		}
//		prevCell = currentCell;
	}



	Cell MouseToCell(Vector2 pos) {
		Vector2 newpos = new Vector2 ((int) ((pos.x - origin.x) / (width / WIDTH)), (int) ((origin.y - pos.y) / (height / HEIGHT)));
		print (newpos);
		return grid [(int) newpos.x, (int) newpos.y].GetComponent<Cell>();
	}
	
}
