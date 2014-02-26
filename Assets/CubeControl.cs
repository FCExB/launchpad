using UnityEngine;
using System.Collections.Generic;

public class CubeControl : MonoBehaviour {

	public GameOfLife game;
	public GameObject cell;
	public GameObject ghost;
	public GameObject plane;

	private Dictionary<Cell,GameObject> cells = new Dictionary<Cell,GameObject>();
	private Dictionary<Cell,GameObject> ghosts = new Dictionary<Cell,GameObject>();
	private HashSet<Cell> lastSet = new HashSet<Cell>();
	private HashSet<GameObject> toDestroy = new HashSet<GameObject>();
	

	void Update () {
		HashSet<GameObject> remove = new HashSet<GameObject>(); 
		foreach(GameObject obj in toDestroy) {
			if(obj.GetComponent<Transform>().position.y < -1) {
				Destroy(obj);
				remove.Add(obj);
			}
		}
		toDestroy.ExceptWith(remove);
		
		HashSet<Cell> newAll = game.getCells();
		HashSet<Cell> newOnes = new HashSet<Cell>(newAll);
		
		newOnes.ExceptWith(lastSet);
		lastSet.ExceptWith(newAll);
		
		foreach(Cell c in newOnes) {
			Vector3 position = game.getPositionFromCell(c);
			cells.Add(c, Instantiate(cell, position,Quaternion.identity) as GameObject);
			position.y = 0;
			ghosts.Add(c, Instantiate(ghost, position,Quaternion.identity) as GameObject);
		}
		
		foreach(Cell c in lastSet) {
			GameObject t  = cells[c];
			Physics.IgnoreCollision(plane.GetComponent<Collider>(),t.GetComponent<Collider>(),true);
			toDestroy.Add(t);
			cells.Remove(c);

			Destroy(ghosts[c]);
			ghosts.Remove(c);
		}
		
		lastSet = newAll;
	}
}
