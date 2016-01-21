using UnityEngine;
using System.Collections;

public class VirtualJoystickHandler : MonoBehaviour {

	public GameObject character;
	public float TurnAngleThreshold = 10.0f;
	public float TurnRateOffset = 0.0f;
	public float MaxMovementSpeed = 2.0f;  // in meters/sec
	public float MaxTurnRate = 50f; // in degrees/sec
	public float TurnAngleToRateScale = 6.0f;	

	private static Vector3 yAxis = new Vector3 (0f, 1f, 0f);

	// Use this for initialization
	void Start () {
	
        }
	
	// Update is called once per frame
	void Update () {

	}

	public void HandleJoystickMovement() {
		Vector3 moveDirection = new Vector3 (Mathf.Pow(transform.up.x, 2)*Mathf.Sign(transform.up.x), 0, Mathf.Pow(transform.up.z, 2)*Mathf.Sign(transform.up.z)); // only take x and z components of direction

		if (moveDirection.magnitude > 0.1f) {			
			character.transform.Translate (moveDirection*MaxMovementSpeed*Time.deltaTime, Space.World);
		}
		Vector3 controllerForwardDirectionInCharacterSpace = 
			character.transform.InverseTransformDirection (transform.forward);
		if (Mathf.Abs(controllerForwardDirectionInCharacterSpace.x) > 0.1f) {
			float amountToRotate = 
				(Mathf.Abs(controllerForwardDirectionInCharacterSpace.x) - 0.1f) * Mathf.Sign(controllerForwardDirectionInCharacterSpace.x) * MaxTurnRate * Time.deltaTime;
			pointerPositionTextMesh.text = "amountToRotate: " + MaxTurnRate + ", " + Time.deltaTime + ", " + (MaxTurnRate * Time.deltaTime) + ", " +amountToRotate;
			Vector3 rotateDirection = new Vector3 (0f, amountToRotate, 0f);
			character.transform.Rotate (rotateDirection);
		}
	}
}
