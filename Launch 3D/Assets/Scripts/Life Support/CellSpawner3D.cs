using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CellSpawner3D : MonoBehaviour {
	
	/* Overall Technical Design: 
	 * The CellSpawner class is the controller for the game.
	 * Based on player input this class will send commands to
	 * the appropriate cells to perform various actions (e.g.
	 * displaying sprites and the like).
	 *  
	 * */
	
	public enum Direction { UP, DOWN, LEFT, RIGHT };

	public const int WIDTH = 20; //grid width and height. Probably worth making more sane
	public const int HEIGHT = 10;
	private Vector3 origin; //Top-Left-Front corner of the backdrop
	private Vector2 airStart = new Vector2(19, 7);
	private List<Cell3D> sinks = new List<Cell3D>();
	private bool changed = false;
	public Transform prefab;
	public GameObject backdrop;
	public LifeSupportComplete CompletionObject;
	Cell prevCell;
	GameObject[,] grid;
	
	void Start () { 
		origin = backdrop.transform.position;
		Vector3 scale = transform.localScale;
		origin = new Vector3 (origin.x - scale.x / 2, origin.y + scale.y / 2, origin.z - scale.z / 2);
		InitGrid();
		BuildLevel();
	}
	
	void InitGrid() {
		for(int i = 0; i < WIDTH*HEIGHT; i++) {
			Instantiate(prefab, origin, transform.rotation).GetInstanceID();
		}
		grid = new GameObject[WIDTH, HEIGHT];
		GameObject[] cells = GameObject.FindGameObjectsWithTag("LS");
		for(int i = 0; i < WIDTH; ++i){
			for(int j = 0; j < HEIGHT; j++) {
				grid[i,j] = cells[i+j*WIDTH];
				Cell3D cell = grid[i,j].GetComponent<Cell3D>();
				cell.spawner = GetComponent<CellSpawner3D>();
				cell.MoveTo(i, j, origin);
			}
		}
	}

	public Cell3D getCell(int x, int y) {
		return grid[x,y].GetComponent<Cell3D>();
	}

	void BuildLevel() {
		//Array is technically reversed to make it easier to read
		//0 = blank
		//1 = block
		//2 = left fan
		//3 = down fan
		//4 = right fan
		//5 = up fan
		//6 = Sink
		//7 = Air Source
		int[,] level = {
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{1, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 1, 1, 1, 1, 0, 0, 1},
			{1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1},
			{1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1},
			{1, 5, 0, 0, 0, 0, 2, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1},
			{1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 4, 0, 0, 0, 0, 0, 5, 1},
			{1, 1, 1, 1, 1, 0, 5, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 0, 0, 1},
			{7, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
		};
		for (int i = 0; i < WIDTH; i++) {
			for(int j = 0; j < HEIGHT; j++) {
				Cell3D cell = grid[WIDTH-i-1, j].GetComponent<Cell3D>();
				switch(level[j,i]) {
				case 0:
					cell.SetType(Cell3D.TYPE.BLANK);
					break;
				case 1:
					cell.SetType(Cell3D.TYPE.BLOCK);
					break;
				case 2:
					cell.SetType(Cell3D.TYPE.FAN);
					cell.setRotation(Direction.LEFT);
					break;
				case 3:
					cell.SetType(Cell3D.TYPE.FAN);
					cell.setRotation(Direction.DOWN);
					break;
				case 4:
					cell.SetType(Cell3D.TYPE.FAN);
					cell.setRotation(Direction.RIGHT);
					break;
				case 5:
					cell.SetType(Cell3D.TYPE.FAN);
					cell.setRotation(Direction.UP);
					break;
				case 7:
					sinks.Add(cell);
					cell.SetType(Cell3D.TYPE.AIR);
					break;
				}
			}
		}
	}



	public void SwapCells(Cell3D cell1, Cell3D cell2) {
		changed = true;
		grid[cell1.x, cell1.y] = cell2.gameObject; 
		grid[cell2.x, cell2.y] = cell1.gameObject; 
		int xpos = cell2.x;
		int ypos = cell2.y;
		cell2.MoveTo(cell1.x, cell1.y, origin);
		cell1.MoveTo(xpos, ypos, origin);
	}
	
	void Update () {
		if(changed) {
			ClearAir();
		}
		CalculateAir((int) airStart.x, (int) airStart.y, Direction.RIGHT);
		//if(CompletionCheck ()) {
		//	CompletionObject.PuzzleCompleted();
		//}
	}

//	bool CompletionCheck() {
//		bool complete = true;
//		foreach (Cell3D cell in sinks) {
//			if(!cell.HasLight) {
//				complete = false;
//				cell.SinkOff();
//			} else {
//				cell.SinkOn();
//			}
//		}
//		return complete;
//	}


	//Add checks
	void CalculateAir(int x, int y, Direction dir) {
		Cell3D cell = grid [x, y].GetComponent<Cell3D> ();
		if(cell.type == Cell3D.TYPE.FAN) {
			Direction newdir = cell.direction;
			cell.EnableWind();
			if(newdir == Direction.RIGHT) {
				CalculateAir(x-1, y, newdir);
			}
			if(newdir == Direction.LEFT) {
				CalculateAir(x+1, y, newdir);
			}
			if(newdir == Direction.UP) {
				CalculateAir(x, y-1, newdir);
			}
			if(newdir == Direction.DOWN) {
				CalculateAir(x, y+1, newdir);
			}
		} else {
			if(dir == Direction.RIGHT) {
				CalculateAir(x-1, y, dir);
			}
			if(dir == Direction.LEFT) {
				CalculateAir(x+1, y, dir);
			}
			if(dir == Direction.UP) {
				CalculateAir(x, y-1, dir);
			}
			if(dir == Direction.DOWN) {
				CalculateAir(x, y+1, dir);
			}
		}
	}
	
	void ClearAir() {
		Cell3D cell;
		for(int i = 0; i < WIDTH; i++) {
			for(int j = 0; j < HEIGHT; j++) {
				cell = grid[i,j].GetComponent<Cell3D>();
				if(cell.type == Cell3D.TYPE.FAN) {
					cell.DisableWind();
				}
			}
		}
		changed = false;
	}
}

