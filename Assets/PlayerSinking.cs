using UnityEngine;
using System.Collections;

public class PlayerSinking : MonoBehaviour {
	
	public Collider floor;
	private bool hitHeight = false;

	private CharacterMotor motor;
	
	// Use this for initialization
	void Start () {
		Physics.IgnoreCollision(collider,floor, true);
		motor = GetComponent<CharacterMotor>();
		motor.movement.maxFallSpeed = 10000;
	}
	
	private float gravity = 20;
	
	// Update is called once per frame
	void Update () {
		if(!hitHeight && transform.position.y < 10) {
			hitHeight = true;
			motor.movement.maxFallSpeed = 20;	
		}
		
		if(transform.position.y < -0.5) {
			motor.canControl = false;
			motor.movement.gravity = -gravity;
			motor.movement.maxFallSpeed = 5;
		} else {
			motor.movement.gravity = gravity;
		}
	}

	public void reset() {
		motor.movement.maxFallSpeed = 10000;
		motor.canControl = true;

		hitHeight = false;
	}
}
