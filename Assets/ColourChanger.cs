using UnityEngine;
using System.Collections;

public class ColourChanger : MonoBehaviour {

	private Color targetColor;

	// Use this for initialization
	void Start () {
		targetColor = new Color(Random.value,Random.value,Random.value);
	}
	
	// Update is called once per frame
	void Update () {
		camera.backgroundColor = Color.Lerp(camera.backgroundColor, targetColor, 0.2f*Time.deltaTime);

		float difference = Mathf.Abs((camera.backgroundColor - targetColor).grayscale);

		if (difference < 0.1) {
			targetColor = new Color(Random.value,Random.value,Random.value);
		}
	}
}
