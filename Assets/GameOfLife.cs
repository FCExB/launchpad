using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameOfLife : MonoBehaviour {

	public LaunchpadManager launchpad;
	
	public PlayerControl player;
	
	private HashSet<Cell> exisitingCells = new HashSet<Cell>();
	private HashSet<Cell> solidCells = new HashSet<Cell>();

	private HashSet<Cell> toRemove = new HashSet<Cell>();
	private HashSet<Cell> toAdd = new HashSet<Cell>();
	
	void Start () {
		launchpad.OnPress += new LaunchpadManager.OnPressHandler(onPress);

		solidCells.Add(new Cell(0,0));;
		solidCells.Add(new Cell(7,7));

		exisitingCells.UnionWith(solidCells);
	}

	public HashSet<Cell> getCells() {
		HashSet<Cell> result = new HashSet<Cell>(exisitingCells);
		result.UnionWith(solidCells);
		return result;	
	}
	
	private void onPress(int x, int y){
		if(x > 7 || x < 0 || y > 7 || y < 0)
			return;

		Cell cell = new Cell(x,y);
		if(exisitingCells.Contains(cell)) {
			if(canDie(cell)) {
				toRemove.Add(cell);
				launchpad.ledOff(x,y);
			} 
		} else {
			if(canGrow(cell)) {
				toAdd.Add(cell);
				launchpad.ledOnYellow(x,y);
				timeSinceRemoves = 0;
			}
		}
	}

	public void reset()
	{
		if(launchpad.Ready) {
			foreach(Cell c in exisitingCells) {
				launchpad.ledOff(c.x,c.y);
			}
			foreach(Cell c in solidCells) {
 				launchpad.ledOnYellow(c.x,c.y);
			}
		}
		
		exisitingCells = new HashSet<Cell>();
		exisitingCells.UnionWith(solidCells);

		toRemove.Clear();
		
		player.reset();
	}

	private float timeSinceRemoves = 0;
	
	void Update () {
		timeSinceRemoves += Time.deltaTime;

		if(Input.GetButton("Reset")) {
			reset();
		}

		exisitingCells.UnionWith(toAdd);
		toAdd.Clear();

		if(timeSinceRemoves > 0.6) {

			foreach (Cell center in exisitingCells) {
				if(solidCells.Contains(center))
					continue;

				int x = center.x;
				int y = center.y;
				
				Cell up = new Cell(x,y-1);
				Cell upLeft = new Cell(x-1,y-1);
				Cell upRight = new Cell(x+1,y-1);
				Cell down = new Cell(x,y+1);
				Cell downLeft = new Cell(x-1,y+1);
				Cell downRight = new Cell(x+1,y+1);
				Cell left = new Cell(x+1,y);
				Cell right = new Cell(x-1,y);

				int count = 0;
				
				if(exisitingCells.Contains(up)) 
					count++;
				if(exisitingCells.Contains(down))
					count++;
				if(exisitingCells.Contains(left))
					count++;
				if(exisitingCells.Contains(right))
					count++;
				if(exisitingCells.Contains(upLeft)) 
					count++;
				if(exisitingCells.Contains(upRight))
					count++;
				if(exisitingCells.Contains(downLeft))
					count++;
				if(exisitingCells.Contains(downRight))
					count++;
				
				if (count == 0) {
					toRemove.Add(center);
					timeSinceRemoves = 0;
					launchpad.ledOff(x,y);
				}
			}

			exisitingCells.ExceptWith(toRemove);

			toRemove.Clear();
		}
	}

	public void setNewPlayerLocation(Cell oldCell, Cell newCell) {
		
		launchpad.ledOnRed(newCell.x,newCell.y);	
		
		if(oldCell == null) 
			return;

		if(launchpad.Ready) {
			if(exisitingCells.Contains(oldCell) || solidCells.Contains(oldCell)) {
				launchpad.ledOnYellow(oldCell.x,oldCell.y);	
			} else {
				launchpad.ledOff(oldCell.x,oldCell.y);
			}
		}
	}

	private bool canGrow(Cell center) {
		int x = center.x;
		int y = center.y;
		
		Cell up = new Cell(x,y-1);
		Cell upLeft = new Cell(x-1,y-1);
		Cell upRight = new Cell(x+1,y-1);
		Cell down = new Cell(x,y+1);
		Cell downLeft = new Cell(x-1,y+1);
		Cell downRight = new Cell(x+1,y+1);
		Cell left = new Cell(x+1,y);
		Cell right = new Cell(x-1,y);

		int count = 0;

		if(exisitingCells.Contains(up)) 
			count++;
		if(exisitingCells.Contains(down))
			count++;
		if(exisitingCells.Contains(left))
			count++;
		if(exisitingCells.Contains(right))
			count++;
		if(exisitingCells.Contains(upLeft)) 
			count++;
		if(exisitingCells.Contains(upRight))
			count++;
		if(exisitingCells.Contains(downLeft))
			count++;
		if(exisitingCells.Contains(downRight))
			count++;

		return count > 0;
	}
	
	private bool canDie(Cell center ) {
		if(solidCells.Contains(center)) {
			return false;
		}

		int x = center.x;
		int y = center.y;

		Cell up = new Cell(x,y-1);
		Cell upLeft = new Cell(x-1,y-1);
		Cell upRight = new Cell(x+1,y-1);
		Cell down = new Cell(x,y+1);
		Cell downLeft = new Cell(x-1,y+1);
		Cell downRight = new Cell(x+1,y+1);
		Cell left = new Cell(x+1,y);
		Cell right = new Cell(x-1,y);

		if(exisitingCells.Contains(up) && exisitingCells.Contains(down))
			return true;
		if(exisitingCells.Contains(left) && exisitingCells.Contains(right))
			return true;
		if(exisitingCells.Contains(upLeft) && exisitingCells.Contains(downRight))
			return true;
		if(exisitingCells.Contains(upRight) && exisitingCells.Contains(downLeft))
			return true;

		int count = 0;
		
		if(exisitingCells.Contains(up)) 
			count++;
		if(exisitingCells.Contains(down))
			count++;
		if(exisitingCells.Contains(left))
			count++;
		if(exisitingCells.Contains(right))
			count++;
		if(exisitingCells.Contains(upLeft)) 
			count++;
		if(exisitingCells.Contains(upRight))
			count++;
		if(exisitingCells.Contains(downLeft))
			count++;
		if(exisitingCells.Contains(downRight))
			count++;
		
		return count == 0;
	}

	public bool canAddCell(Cell cell) {
		return cell.x >= 0 && cell.x < 8 && cell.y >= 0 && cell.y < 8 && canGrow(cell);
	}

	public void addCell(Cell cell ) {
		exisitingCells.Add(cell);
		if(launchpad.Ready)
			launchpad.ledOnYellow(cell.x,cell.y);
	}

	public float cellWidth = 10;

	public Vector3 getPositionFromCell(Cell cell) {
		return new Vector3((cell.x-3.5f)*cellWidth,100f,(cell.y-3.5f)*cellWidth);
	}

	public Cell getCellFromPosition(Vector3 position) {
		return new Cell(Mathf.RoundToInt((position.x/cellWidth)+3.5f), Mathf.RoundToInt((position.z/cellWidth)+3.5f));	
	}
}