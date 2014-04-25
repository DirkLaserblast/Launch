using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

	int x = 0;
	int y = 0;
	bool moved = false;
	dfSprite sprite;
	private bool lightRotated = false;
	public bool isBlock = false;
	public bool fanRotated = false;
	public dfSprite light;
	public dfSprite sink;
	public dfSprite fan;
	private bool hasLight;
	private bool hasFan;
	private bool hasSink; 

	public bool HasLight {
		get { return hasLight; }
		set {
			if(value && !hasLight) {
				light.IsVisible = true;
			}
			if(!value && hasLight) {
				light.IsVisible = false;
				if(lightRotated) {
					RotateLightBack();
				}
			}
			hasLight = value;
		}
	}

	public bool HasFan {
		get { return hasFan; }
		set {
			if(value && !hasFan) {
				fan.IsVisible = true;
			}
			if(!value && hasFan) {
				fan.IsVisible = false;
			}
			hasFan = value;
		}
	}
	public bool HasSink {
		get { return hasSink; }
		set {
			if(value && !hasSink) {
				sink.IsVisible = true;
			}
			if(!value && hasSink) {
				sink.IsVisible = false;
			}
			hasSink = value;
		}
	}

	void Start () {
		sprite = transform.GetComponent<dfSprite> ();
		//GenerateRandomAttributes();
		sprite.RelativePosition = new Vector3 (x, y, 0);
		sprite.PerformLayout ();
	}

	void GenerateRandomAttributes() {
		if (Random.value > 0.8) {
			hasFan = true;
			fan.IsVisible = true;
			if(Random.value > 0.5) {
				RotateFan();
			}
			if(Random.value > 0.4) {
				FanToBlock();
			}
		}
	}

	public void MoveTo(int x, int y) {
		this.x = x;
		this.y = y;
		sprite = transform.GetComponent<dfSprite> ();
		sprite.RelativePosition = new Vector3 (x, y, 0);
		moved = true;
	}

	public void SegmentToBend() {
		light.SpriteName = "Bend";
	}

	public void BendToSegment() {
		light.SpriteName = "LightSegment";
	}

	public void LightToCross() {
		light.SpriteName = "Cross";
	}

	public void FanToBlock() {
		isBlock = true;
		fan.SpriteName = "Block";
	}

	public void BlockToFan() {
		isBlock = false;
		fan.SpriteName = "Diagonal";
	}

	public void SinkOn() {
		sink.SpriteName = "SinkOn";
	}

	public void SinkOff() {
		sink.SpriteName = "Sink";
	}

	public void RotateLight() {
		lightRotated = true;
		light.transform.Rotate (0, 0, 90);
		light.RelativePosition = new Vector3(light.RelativePosition.x, light.RelativePosition.y + CellSpawner.CELL_SIZE, 0);
	}

	public void RotateLightBack() {
		lightRotated = false;
		light.transform.Rotate (0, 0, -light.transform.rotation.eulerAngles.z);
		light.RelativePosition = new Vector3(0, 0, 0);
	}

	public void RotateFan() {
		fanRotated = true;
		fan.transform.Rotate (0, 0, 90);
		fan.RelativePosition = new Vector3(fan.RelativePosition.x, fan.RelativePosition.y + CellSpawner.CELL_SIZE, 0);
	}

	public void RotateFanBack() {
		fanRotated = false;
		fan.transform.Rotate (0, 0, -fan.transform.rotation.eulerAngles.z);
		fan.RelativePosition = new Vector3(0,0,0);
	}

	public void RotateLightBend(CellSpawner.Direction dir) {
		if(fanRotated) {
			if(dir == CellSpawner.Direction.DOWN || dir == CellSpawner.Direction.RIGHT) {
				light.transform.Rotate(0, 0, 180);
				light.RelativePosition = new Vector3(light.RelativePosition.x + CellSpawner.CELL_SIZE, light.RelativePosition.y + CellSpawner.CELL_SIZE, 0);
			}
			if(dir == CellSpawner.Direction.LEFT || dir == CellSpawner.Direction.UP) {
				//No rotation
			}
		} else {
			if(dir == CellSpawner.Direction.DOWN || dir == CellSpawner.Direction.LEFT) {
				light.transform.Rotate(0, 0, 90);
				light.RelativePosition = new Vector3(light.RelativePosition.x, light.RelativePosition.y + CellSpawner.CELL_SIZE, 0);
			}
			if(dir == CellSpawner.Direction.RIGHT || dir == CellSpawner.Direction.UP) {
				light.transform.Rotate(0, 0, 270);
				light.RelativePosition = new Vector3(light.RelativePosition.x + CellSpawner.CELL_SIZE, light.RelativePosition.y, 0);
			}
		}
	}

	public void RotateSink(CellSpawner.Direction dir) {
		if(dir == CellSpawner.Direction.DOWN) {
			sink.transform.Rotate(0, 0, 90);
			sink.RelativePosition = new Vector3(light.RelativePosition.x, light.RelativePosition.y + CellSpawner.CELL_SIZE, 0);
		}
		if(dir == CellSpawner.Direction.LEFT) {
			//No rotation
		}
		if(dir == CellSpawner.Direction.UP) {
			sink.transform.Rotate(0, 0, 270);
			sink.RelativePosition = new Vector3(light.RelativePosition.x + CellSpawner.CELL_SIZE, light.RelativePosition.y, 0);
		}
		if(dir == CellSpawner.Direction.RIGHT) {
			sink.transform.Rotate(0, 0, 180);
			sink.RelativePosition = new Vector3(light.RelativePosition.x + CellSpawner.CELL_SIZE, light.RelativePosition.y + CellSpawner.CELL_SIZE, 0);

		}
	}

	public CellSpawner.Direction newDirection(CellSpawner.Direction dir) {
		if (fanRotated) {
			if(dir == CellSpawner.Direction.RIGHT) {
				return CellSpawner.Direction.UP;
			}
			if(dir == CellSpawner.Direction.DOWN) {
				return CellSpawner.Direction.LEFT;
			}
			if(dir == CellSpawner.Direction.UP) {
				return CellSpawner.Direction.RIGHT;
			}
			if(dir == CellSpawner.Direction.LEFT) {
				return CellSpawner.Direction.DOWN;
			}
		} else {
			if(dir == CellSpawner.Direction.RIGHT) {
				return CellSpawner.Direction.DOWN;
			}
			if(dir == CellSpawner.Direction.DOWN) {
				return CellSpawner.Direction.RIGHT;
			}
			if(dir == CellSpawner.Direction.UP) {
				return CellSpawner.Direction.LEFT;
			}
			if(dir == CellSpawner.Direction.LEFT) {
				return CellSpawner.Direction.UP;
			}
		}
		return dir;
	}


}
