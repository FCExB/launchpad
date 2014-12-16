using UnityEngine;
using System.Collections;

public class MenuSystem : MonoBehaviour {
	
	public GameOfLife game;
	public LaunchpadManager launchpad;

	public GameObject player;

	public GUIStyle style;

	public Vector2 labelLocation;
	private bool onMenu;

	// Use this for initialization
	void Start () {
		onMenu = true;
		camera.enabled = true; 
		player.SetActive(false);
		GetComponent<AudioListener>().enabled = true;
	}

	public void SetMenuState(bool state) {
		onMenu = state;
		camera.enabled = state; 
		player.SetActive(!state);
		GetComponent<AudioListener>().enabled = state;
		game.reset();
	}

	void OnGUI() {
		if(!onMenu)
			return;

		if(launchpad.Ready)
			GUI.Label(new Rect(labelLocation.x,Screen.height-labelLocation.y,400,20),"Press A to begin!",style);
		else 
			GUI.Label(new Rect(labelLocation.x,Screen.height-labelLocation.y,400,20),"No Launchpad connected :(",style);
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Start") || !launchpad.Ready)
			SetMenuState(true);

		if(onMenu && Input.GetButtonDown("Jump"))
			SetMenuState(false);
	}

}
