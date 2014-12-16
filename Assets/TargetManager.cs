using UnityEngine;
using System.Collections;

public class TargetManager : MonoBehaviour {

	public Vector3 Target {
		get {return target;}
	}

	public Vector3 target;
	public MenuSystem menu;

	// Use this for initialization
	void Start () {
		transform.position = target;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		menu.SetMenuState(true);
	}

}
