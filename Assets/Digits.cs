﻿using UnityEngine;
using System.Collections;

public class Digits : MonoBehaviour {

	public LaunchpadManager launchpad;
	public TextMesh infoText;
	public Effects effects;

	public int numPlayers = 2;
	
	private bool gameOver = true;
	private int winner = 0;

	private int nextPlayer = 1;

	int targetX, targetY;
		
	private int[,] buttons = new int[8,8];

	float toggleTimer, gameOverTimer = 1000;
	bool sideOn = false;


	// Use this for initialization
	void Start () {
		launchpad.OnPress += new LaunchpadManager.OnPressHandler(onPress);
		launchpad.OnRelease += new LaunchpadManager.OnReleaseHandler(onRelease);

		targetX = (int)(Random.value * 7);
		targetY = (int)(Random.value * 7);

		effects.Activate();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameOver) {
			if (winner == 0) {
				infoText.text = "";
			} else {
				infoText.text = "Player " + winner + " wins!";
			}

		} else {
			infoText.text = "Player " + nextPlayer + ":\nPress & Hold!";
		} 

		while (buttons[targetX,targetY] != 0) {
			targetX = (int)(Random.value * 7);
			targetY = (int)(Random.value * 7);
		}
		
		if (gameOver) {
			gameOverTimer += Time.deltaTime;
			toggleTimer += Time.deltaTime;
			
			if (toggleTimer > 0.1 && gameOverTimer < 10) {
				toggleTimer = 0;
				sideOn = !sideOn;
			}

			if (gameOverTimer > 10 && gameOverTimer < 100) {
				for (int x = 0; x < 8; x++) {
					for (int y = 0; y < 8; y++) { 
						launchpad.ledOff(x, y);
					} 
				}
				effects.Activate();

				sideOn = false;

				gameOverTimer = 1000;
				winner = 0;
			} 

			launchpad.ledOff(targetX, targetY);
		} else {
			launchpad.ledOnRed(targetX, targetY);
		}

		for (int y = 0; y < 8; y++) {
			if (sideOn) {
				launchpad.ledOnYellow(8, y);
			} else {
				launchpad.ledOff(8, y);
			}
		}
	}
	
	public void reset() {
		effects.Deactivate();

		for (int x = 0; x < 8; x++) {
			for (int y = 0; y < 8; y++) {
				buttons[x,y] = 0;
				launchpad.ledOff(x, y);
			} 
		}

		sideOn = false;
		gameOver = false;

		nextPlayer = 1;
	}

	private void onPress(int x, int y) {

		if (gameOver && ((x == 8) || gameOverTimer > 10)) {
			reset();
			return;
		}

		if(x<0 || x > 7 || y <0 || y > 7) {
			return;
		}

		if (x == targetX && y == targetY && !gameOver) {
			buttons[x,y] = nextPlayer;
			launchpad.ledOnYellow(x,y);
			startNextTurn();
		}
	}

	private void onRelease(int x, int y){
		if(x<0 || x > 7 || y <0 || y > 7) {
			return;
		}

		if (!gameOver && buttons[x,y] != 0) {
			gameOver = true;
			winner = (buttons[x,y] % numPlayers) + 1;
			launchpad.ledOnRed(x, y);

			gameOverTimer = 0;
		}
	}

	private void startNextTurn() {
		if (nextPlayer++ == numPlayers) {
			nextPlayer = 1;
		}
	}
}
