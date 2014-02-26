using UnityEngine;
using System.Collections;

public class Sinking : MonoBehaviour {
	
	private bool setDrag = false;
	
	private bool hitCorrectHeight = false;
	
	// Use this for initialization
	void Start () {
		rigidbody.drag = 0;	
		rigidbody.AddForce(new Vector3(0,-5000,0));
	}
	
	// Update is called once per frame
	void Update () {
		if(!setDrag && transform.position.y < 1) {

			setDrag = true;
			rigidbody.drag = 15;
		} 
		
		if(!hitCorrectHeight && transform.position.y < 10) {
			if(transform.position.y < 10) {
				hitCorrectHeight = true;
				rigidbody.velocity = new Vector3(0,-10f,0);
				rigidbody.drag = 0;
			} else if(transform.position.y < 50) {
				rigidbody.drag = rigidbody.velocity.magnitude*3;
			}
		}
	}
}
	