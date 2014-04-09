using UnityEngine;


// this script is specific to the supplied Sample Assets car, which has mudguards over the front wheels
// which have to turn with the wheels when steering is applied.

public class Mudguard : MonoBehaviour {

    public Wheel wheel;                // the wheel we are orientating to
	public bool steerable = true;
	public bool invertSteering = false;
	Quaternion originalRotation;
	Vector3 offset;

	void Start() {
		originalRotation = transform.localRotation;
		offset = transform.position-wheel.wheelModel.transform.position;
	}

    void Update() {
		if (steerable)
		{
			if (!invertSteering)
			{
				transform.localRotation = originalRotation * Quaternion.Euler(0, wheel.car.CurrentSteerAngle * wheel.steerPercentage, 0);
			}
			else
			{
				transform.localRotation = originalRotation * Quaternion.Euler(0, -wheel.car.CurrentSteerAngle  * wheel.steerPercentage, 0);
			}
		}
		transform.position = wheel.wheelModel.transform.position + offset;
    }
}
