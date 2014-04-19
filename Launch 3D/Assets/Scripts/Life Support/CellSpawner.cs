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



	private const int WIDTH = 21;
	private const int HEIGHT = 9;
	private const int CELL_SIZE = 64;
	public Transform prefab;
	public GameObject gameParent;
	GameObject[,] grid;

	void Start () {
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

	public GameObject getCell(int x, int y) {
		return grid[x,y];
	}
}
