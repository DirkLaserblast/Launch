using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CellSpawner : MonoBehaviour {

	/* Overall Technical Design: 
	 * The CellSpawner class is the controller for the game.
	 * Based on player input this class will send commands to
	 * the appropriate cells to perform various actions (e.g.
	 * displaying sprites and the like).
	 *  
	 * */

	public enum Direction { UP, DOWN, LEFT, RIGHT };


	public const int WIDTH = 21; //cell width/height
	public const int HEIGHT = 9;
	public const int CELL_SIZE = 64;
	private float width; //screen width/height
	private float height;
	private Vector2 origin;
	private dfPanel panel;
	private Vector2 airStart = new Vector2(0, 7);
	private List<Cell> sinks = new List<Cell>();
	public Transform prefab;
	public GameObject gameParent;
	public Camera camera;
	public LifeSupportComplete CompletionObject;
	Cell prevCell;
	GameObject[,] grid;

	void Start () { //Initializes the grid
		panel = transform.GetComponent<dfPanel> ();
		width = panel.GetScreenRect().width;
		height = panel.GetScreenRect().height;
		origin = new Vector2 (panel.GetScreenRect ().x, Screen.height - panel.GetScreenRect ().y);
		InitGrid();
		BuildLevel ();
		gameParent.SetActive (false);
	}

	void InitGrid() {
		for(int i = 0; i < WIDTH*HEIGHT; i++) {
			Instantiate(prefab, transform.position, transform.rotation).GetInstanceID();
		}
		grid = new GameObject[WIDTH, HEIGHT];
		GameObject[] instancesArr = GameObject.FindGameObjectsWithTag("LS");
		for(int i = 0; i < WIDTH; ++i){
			for(int j = 0; j < HEIGHT; j++) {
				grid[i, j] = instancesArr[i+j*WIDTH];
				grid[i,j].transform.parent = transform;
				Cell sprite = grid[i,j].GetComponent<Cell>();
				sprite.MoveTo(i*CELL_SIZE, j*CELL_SIZE);
			}
		}
	}

	void BuildLevel() {
		//Array is technically reversed to make it easier to read
		//0 = blank
		//1 = block
		//2 = fan
		//3 = rotated fan
		//4 = Up Sink
		//5 = Left Sink
		//6 = Down Sink
		//7 = Right Sink
		int[,] level = {
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 7},
			{1, 0, 2, 0, 0, 0, 0, 0, 4, 0, 0, 2, 0, 1, 1, 1, 1, 0, 1, 1, 1},
			{1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 3, 0, 1, 1, 1, 1, 0, 1, 1, 0},
			{1, 0, 1, 1, 1, 1, 0, 0, 3, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 0},
			{1, 3, 0, 0, 3, 0, 0, 1, 2, 6, 0, 0, 0, 1, 1, 1, 1, 0, 1, 1, 0},
			{1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 2, 3, 2, 0, 0, 0, 0, 0, 0, 0, 0},
			{1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 5, 1, 0, 1, 1, 1, 1, 3, 3, 0, 7},
			{0, 0, 0, 3, 0, 3, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2},
			{0, 0, 6, 0, 2, 0, 0, 1, 1, 1, 3, 0, 2, 1, 1, 1, 1, 0, 0, 0, 0},
		};

		for (int i = 0; i < WIDTH; i++) {
			for(int j = 0; j < HEIGHT; j++) {
				Cell cell = grid[i, j].GetComponent<Cell>();
				switch(level[j,i]) {
				case 1:
					cell.HasFan = true;
					cell.FanToBlock();
					break;
				case 2:
					cell.HasFan = true;
					cell.RotateFan();
					break;
				case 3:
					cell.HasFan = true;
					break;
				case 4:
					sinks.Add(cell);
					cell.HasSink = true;
					cell.RotateSink(Direction.UP);
					break;
				case 5:
					sinks.Add(cell);
					cell.HasSink = true;
					cell.RotateSink(Direction.LEFT);
					break;
				case 6:
					sinks.Add(cell);
					cell.HasSink = true;
					cell.RotateSink(Direction.DOWN);
					break;
				case 7:
					sinks.Add(cell);
					cell.HasSink = true;
					cell.RotateSink(Direction.RIGHT);
					break;
				}
			}
		}
	}
	

	//The "airflow" sprites are cleared each tick, and then recalculated.
	void Update () {
		ClearAir ();
		DrawAir((int) airStart.x, (int) airStart.y, Direction.RIGHT);
		if(CompletionCheck ()) {
			CompletionObject.PuzzleCompleted();
		}
	}


	bool CompletionCheck() {
		bool complete = true;
		foreach (Cell cell in sinks) {
			if(!cell.HasLight) {
				complete = false;
				cell.SinkOff();
			} else {
				cell.SinkOn();
			}
		}
		return complete;
	}


	/* *
	 * Called whenever the mouse is moved over the gameObject.
	 * It will move the sprite attributes attached to the current cell to
	 * whereever the player drags his/her mouse.
	 * 
	 * Disallows blocks from moving.
	 * */
	void OnMouseMove() {
		if (Input.GetMouseButton(0)) {
			Cell currentCell = MouseToCell (Input.mousePosition);
			if (prevCell != null) {
				if(currentCell != prevCell) {
					if(prevCell.HasFan && !currentCell.HasFan && !prevCell.isBlock) {
						prevCell.HasFan = false;
						currentCell.HasFan = true;
						if(prevCell.fanRotated) {
							currentCell.RotateFan();
							prevCell.RotateFanBack();
						}
					}
				}
			}
			prevCell = currentCell;
		}
	}

	void OnMouseUp() {
		prevCell = null;
	}

	void DrawAir(int x, int y, Direction dir) {
		Cell cell = grid [x, y].GetComponent<Cell> ();
		if(!cell.HasFan) {
			if(cell.HasLight) {
				cell.LightToCross();
			} else {
				cell.HasLight = true;
				cell.BendToSegment();
			}
			if(x+1 < WIDTH && dir == Direction.RIGHT) {
				DrawAir(x+1, y, dir);
			}
			if(x-1 >= 0 && dir == Direction.LEFT) {
				DrawAir(x-1, y, dir);
			}
			if(dir == Direction.UP) {
				cell.RotateLight();
				if(y-1 >= 0) {
					DrawAir(x, y-1, dir);
				}
			}
			if(dir == Direction.DOWN) {
				cell.RotateLight();
				if(y+1 < HEIGHT) {
					DrawAir(x, y+1, dir);
				}
			}
		} else if(!cell.isBlock) {
			if(cell.HasLight) {
				cell.LightToCross();
			} else {
				cell.HasLight = true;
				cell.SegmentToBend();
			}
			Direction newdir = cell.newDirection(dir);
			if(newdir == Direction.RIGHT) {
				cell.RotateLightBend(newdir);
				if(x+1 < WIDTH) {
					DrawAir(x+1, y, newdir);
				}
			}
			if(newdir == Direction.LEFT) {
				cell.RotateLightBend(newdir);
				if(x-1 >= 0) {
					DrawAir(x-1, y, newdir);
				}
			}
			if(newdir == Direction.UP) {
				cell.RotateLightBend(newdir);
				if(y-1 >= 0) {
					DrawAir(x, y-1, newdir);
				}
			}
			if(newdir == Direction.DOWN) {
				cell.RotateLightBend(newdir);
				if(y+1 < HEIGHT) {
					DrawAir(x, y+1, newdir);
				}
			}
		}
	}
	
	void ClearAir() {
		Cell cell;
		for(int i = 0; i < WIDTH; i++) {
			for(int j = 0; j < HEIGHT; j++) {
				cell = grid[i,j].GetComponent<Cell>();
				cell.HasLight = false;
				cell.RotateLightBack();
			}
		}                         
	}
	
	Cell MouseToCell(Vector2 pos) {
		Vector2 newpos = new Vector2 ((int) ((pos.x - origin.x) / (width / WIDTH)), (int) ((origin.y - pos.y) / (height / HEIGHT)));
		return grid [(int) newpos.x, (int) newpos.y].GetComponent<Cell>();
	}
}
