using UnityEngine;
using System.Collections;

public class MyCompass : MonoBehaviour {

	public Texture compass;
	public Texture needle;

	public TargetManager targetManager;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		Vector3 us = transform.position;
		us.y = 0;

		Vector3 target = targetManager.Target;
		target.y = 0;

		Vector3 direction = transform.position - target;

		float angle = Vector3.Angle(new Vector3(0,0,1), direction);

		angle += 180;

		if(us.x < target.x)
			angle = 360 - angle;

		angle -= transform.rotation.eulerAngles.y;

		//GUI.DrawTexture(new Rect(Screen.width-150,Screen.height - 150,100,100),compass);

		Matrix4x4 matrixBackup = GUI.matrix;
		GUIUtility.RotateAroundPivot(angle, new Vector2(Screen.width-75,Screen.height - 75));
		GUI.DrawTexture(new Rect(Screen.width-125,Screen.height - 125,100,100),needle);
		GUI.matrix = matrixBackup;
	}
}
