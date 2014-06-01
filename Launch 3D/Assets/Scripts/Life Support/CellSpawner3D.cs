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
	private List<Vector2> sinks = new List<Vector2>();
	private Transform[,] sinkObjects;
	private bool changed = false;
	public Material normalMaterial;
	public Material airMaterial;
	public Camera myCamera;
	public Transform prefab;
	public GameObject backdrop;
	public LifeSupportStart CompletionObject;
	public dfLabel instructions;
	public DoorScript door;
	public GameObject ventillationAudio;
	Cell prevCell;
	GameObject[,] grid;
	public AudioClip winning;
	bool StopWinning = false;
	
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
		sinkObjects = new Transform[WIDTH, HEIGHT];
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
			{1, 0, 0, 0, 4, 0, 8, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 8, 0, 1},
			{1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1},
			{1, 0, 1, 1, 1, 1, 0, 0, 0, 2, 8, 3, 0, 1, 1, 1, 1, 0, 0, 1},
			{1, 5, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1},
			{1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 4, 0, 8, 0, 0, 0, 5, 1},
			{1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 0, 0, 1},
			{7, 0, 0, 5, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{1, 0, 0, 3, 8, 4, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1},
			{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
		};
		for (int i = 0; i < WIDTH; i++) {
			for(int j = 0; j < HEIGHT; j++) {
				Cell3D cell = grid[WIDTH-i-1, j].GetComponent<Cell3D>();
				cell.myCamera = myCamera;
				switch(level[j,i]) {
				case 0:
					cell.SetType(Cell3D.TYPE.BLANK, Direction.LEFT);
					break;
				case 1:
					cell.SetType(Cell3D.TYPE.BLOCK, Direction.LEFT);
					break;
				case 2:
					cell.SetType(Cell3D.TYPE.FAN, Direction.LEFT);
					break;
				case 3:
					cell.SetType(Cell3D.TYPE.FAN, Direction.DOWN);
					break;
				case 4:
					cell.SetType(Cell3D.TYPE.FAN, Direction.RIGHT);
					break;
				case 5:
					cell.SetType(Cell3D.TYPE.FAN, Direction.UP);
					break;
				case 7:
					cell.SetType(Cell3D.TYPE.AIR, Direction.LEFT);
					break;
				case 8:
					sinks.Add(new Vector2(WIDTH-i-1, j));
					sinkObjects[WIDTH-i-1, j] = cell.spawnSink();
					cell.SetType(Cell3D.TYPE.BLANK, Direction.LEFT);
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
		if(CompletionCheck()) {
			if (StopWinning == false) {
				StopWinning = true;
				audio.PlayOneShot(winning);
			}
			instructions.Text = "Air now flowing to all rooms. Communications room is now accessible.";
			ventillationAudio.SetActive(true);
			PlayerPrefsX.SetBool("VentillationAudio", true);
			door.isAirLocked = false;
		}
	}

	bool CompletionCheck() {
		bool complete = true;
		foreach (Vector2 pos in sinks) {
			GameObject obj = grid[(int) pos.x, (int) pos.y];
			MeshRenderer sink = sinkObjects[(int) pos.x, (int) pos.y].GetComponent<MeshRenderer>();
			Cell3D cell = obj.GetComponent<Cell3D>();
			if(!cell.hasAir) {
				complete = false;
				sink.material = normalMaterial;
			} else {
				sink.material = airMaterial;
				print(sink.material);
			}
		}
		return complete;
	}


	//Add checks
	void CalculateAir(int x, int y, Direction dir) {
		Cell3D cell = grid [x, y].GetComponent<Cell3D> ();
		if(!cell.hasAir) {
			cell.hasAir = true;
			if(cell.type == Cell3D.TYPE.FAN) {
				Direction newdir = cell.direction;
				cell.EnableWind();
				if(newdir == Direction.RIGHT) {
					if(x-1 > 0) {
						CalculateAir(x-1, y, newdir);
					}
				}
				if(newdir == Direction.LEFT) {
					if(x+1 < WIDTH-1) {
						CalculateAir(x+1, y, newdir);
					}
				}
				if(newdir == Direction.UP) {
					if(y-1 > 0) {
						CalculateAir(x, y-1, newdir);
				}
				}
				if(newdir == Direction.DOWN) {
					if(y+1 < HEIGHT-1) {
						CalculateAir(x, y+1, newdir);
					}
				}
			} else if(cell.type != Cell3D.TYPE.BLOCK) {
				if(dir == Direction.RIGHT) {
					if(x-1 > 0) {
						CalculateAir(x-1, y, dir);
					}
				}
				if(dir == Direction.LEFT) {
					if(x+1 < WIDTH-1) {
						CalculateAir(x+1, y, dir);
					}
				}
				if(dir == Direction.UP) {
					if(y-1 > 0) {
						CalculateAir(x, y-1, dir);
					}
				}
				if(dir == Direction.DOWN) {
					if(y+1 < HEIGHT-1) {
						CalculateAir(x, y+1, dir);
					}
				}
			}
		}
	}
	
	void ClearAir() {
		Cell3D cell;
		for(int i = 0; i < WIDTH; i++) {
			for(int j = 0; j < HEIGHT; j++) {
				cell = grid[i,j].GetComponent<Cell3D>();
				cell.hasAir = false;
				if(cell.type == Cell3D.TYPE.FAN) {
					cell.DisableWind();
				}
			}
		}
		changed = false;
	}
}

