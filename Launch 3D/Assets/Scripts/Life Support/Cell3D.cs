using UnityEngine;
using System.Collections;

public class Cell3D : MonoBehaviour {

	public enum TYPE {FAN, ROTATEDFAN, BLOCK, BLANK, AIR};
	
	public TYPE type;
	private bool mouseover = false;
	private Transform wind;
	private Transform fan;
	private Vector3 mousePosition;
	public int x = 0;
	public int y = 0;
	public CellSpawner3D.Direction direction;
	public Transform windPrefab;
	public Transform fanPrefab;
	public Transform ventPrefab;
	public bool hasAir = false;
	public Material nonBlockMaterial;
	public CellSpawner3D spawner;
	public Camera myCamera;

	void Start() { }

	void Update() { 
		if (type == TYPE.FAN) {
			fan.Rotate(0, 8, 0);
		}
	}

	public void EnableWind() {
		if(type == TYPE.FAN) {
			wind.gameObject.SetActive(true);
		}
	}

	public void DisableWind() {
		if(type == TYPE.FAN) {
			wind.gameObject.SetActive(false);
		}
	}

	public void MoveTo(int x, int y, Vector3 origin) {
		this.x = x;
		this.y = y;
		Vector3 scale = transform.localScale;
		transform.position = new Vector3 (origin.x, origin.y - scale.y*y - scale.y/2, origin.z + scale.z*x + scale.z/2);
	}

	public void SetType(TYPE type, CellSpawner3D.Direction direction) {
		this.type = type;
		MeshRenderer renderer = transform.GetComponent<MeshRenderer>();
		Vector3 pos = new Vector3 (transform.position.x - 0.005f, transform.position.y, transform.position.z);
		switch (this.type) {
		case TYPE.AIR: 
			transform.Rotate(0, 180, 0);
			wind = (Transform) Instantiate(windPrefab, pos, transform.rotation);
			wind.parent = this.transform;
			DisableRenderer();
			break;
		case TYPE.BLANK: 
			DisableRenderer();
			break;
		case TYPE.BLOCK: 
			break;
		case TYPE.FAN:
			renderer.enabled = false;
			wind = (Transform) Instantiate(windPrefab, pos, transform.rotation);
			wind.parent = this.transform;
			setRotation(direction);
			pos.x = pos.x - 0.03f;
			fan = (Transform) Instantiate(fanPrefab, pos, transform.rotation);
			fan.parent = this.transform;
			RotateFan();
			wind.gameObject.SetActive(false);
			break;
		}
	}

	public void setRotation(CellSpawner3D.Direction direction) {
		this.direction = direction;
		if (direction == CellSpawner3D.Direction.LEFT) {
			//No rotation
		}
		if (direction == CellSpawner3D.Direction.DOWN) {
			transform.Rotate(90, 0, 0);
		}
		if (direction == CellSpawner3D.Direction.RIGHT) {
			transform.Rotate(180, 0, 0);
		}
		if (direction == CellSpawner3D.Direction.UP) {
			transform.Rotate(270, 0, 0);
		}
	}

	public void RotateFan() {
		fan.Rotate(90, 0, 0);
		if (direction == CellSpawner3D.Direction.UP) {
			//No rotation
		}
		if (direction == CellSpawner3D.Direction.LEFT) {
			//fan.Rotate(90, 0, 0);
		}
		if (direction == CellSpawner3D.Direction.DOWN) {
			//fan.Rotate(180, 0, 0);
		}
		if (direction == CellSpawner3D.Direction.RIGHT) {
			//fan.Rotate(270, 0, 0);
		}
	}

	private void DisableRenderer() {
		MeshRenderer renderer = transform.GetComponent<MeshRenderer>();
		BoxCollider collider = transform.GetComponent<BoxCollider> ();
		renderer.enabled = false;
		collider.enabled = false;
	}

	private void EnableRenderer() {
		MeshRenderer renderer = transform.GetComponent<MeshRenderer>();
		BoxCollider collider = transform.GetComponent<BoxCollider> ();
		renderer.enabled = true;
		collider.enabled = true;
	}

	public CellSpawner3D.Direction newDirection(CellSpawner3D.Direction dir) {
		if (this.type == TYPE.ROTATEDFAN) {
			if(dir == CellSpawner3D.Direction.RIGHT) {
				return CellSpawner3D.Direction.UP;
			}
			if(dir == CellSpawner3D.Direction.DOWN) {
				return CellSpawner3D.Direction.LEFT;
			}
			if(dir == CellSpawner3D.Direction.UP) {
				return CellSpawner3D.Direction.RIGHT;
			}
			if(dir == CellSpawner3D.Direction.LEFT) {
				return CellSpawner3D.Direction.DOWN;
			}
		} else {
			if(dir == CellSpawner3D.Direction.RIGHT) {
				return CellSpawner3D.Direction.DOWN;
			}
			if(dir == CellSpawner3D.Direction.DOWN) {
				return CellSpawner3D.Direction.RIGHT;
			}
			if(dir == CellSpawner3D.Direction.UP) {
				return CellSpawner3D.Direction.LEFT;
			}
			if(dir == CellSpawner3D.Direction.LEFT) {
				return CellSpawner3D.Direction.UP;
			}
		}
		return dir;
	}

	public void spawnSink() {
		Vector3 pos = new Vector3 (transform.position.x - 0.598f, transform.position.y + 0.16f, transform.position.z - 0.23f);
		Instantiate (ventPrefab, pos, transform.rotation);
	}

	void OnMouseOver() {
		mouseover = true;
	}

	void OnMouseExit() {
		mouseover = false;
	}

	void OnMouseDrag() {
		if (!mouseover && (type == TYPE.FAN || type == TYPE.ROTATEDFAN)) {
			Cell3D other = null;
			Vector3 currentPosition = myCamera.WorldToScreenPoint(transform.position);
			Vector2 positionDifference = new Vector2(currentPosition.x - Input.mousePosition.x, currentPosition.y - Input.mousePosition.y);
			if(Mathf.Abs(positionDifference.x) > Mathf.Abs(positionDifference.y)) { //Check if the difference is larger in Y or in X
				if(positionDifference.x > 0) {
					if(Mathf.Abs(positionDifference.x) > 30) {
						other = spawner.getCell(x+1, y);
					}
				} else {
					if(Mathf.Abs(positionDifference.x) > 30) {
						other = spawner.getCell(x-1, y);
					}
				}
			} else {
				if(positionDifference.y > 0) {
					if(Mathf.Abs(positionDifference.y) > 30) {
						other = spawner.getCell(x, y+1);
					}
				} else {
					if(Mathf.Abs(positionDifference.y) > 30) {
						other = spawner.getCell(x, y-1);
					}
				}
			}
			if(other == null) {
				other = GetComponent<Cell3D>();
			}
			if(other.type == TYPE.BLANK) {
				spawner.SwapCells(GetComponent<Cell3D>(), other);
			}
			
			mousePosition = Input.mousePosition;
			//mouseover = true;
		}
	}
	
	
}

