using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	private int layerMask;
	public GameOfLife game;
	public GameObject ghostBlock;

	private Cell currentCell;

	private Vector3 resetPosition;
	private Vector3 resetForward;

	void Start () {
		layerMask = 1 << 8;
		ghostBlock.renderer.enabled = false;
		currentCell = game.getCellFromPosition(transform.position);
		resetPosition = transform.position;
		resetForward = transform.forward;
		game.setNewPlayerLocation(null,currentCell);
	}

	void Update () {
		Ray ray = GetComponentInChildren<Camera>().ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
		RaycastHit hit;
		
		if(Physics.Raycast(ray,out hit,layerMask)) {
			Cell cell = game.getCellFromPosition(hit.point);
			if(game.canAddCell(cell)) {
				ghostBlock.transform.position = new Vector3((cell.x-3.5f)*10,0f,(cell.y-3.5f)*10);
				ghostBlock.renderer.enabled = true;
				if(Input.GetButtonDown("Place")) {
					game.addCell(cell);
				}
			} else {
				ghostBlock.renderer.enabled = false;
			}
		}

		Cell newCell = game.getCellFromPosition(transform.position);
		if(!newCell.Equals(currentCell)) {
			game.setNewPlayerLocation(currentCell, newCell);
			currentCell = newCell;
		}
	}

	public void reset() {
		transform.position = resetPosition;	
		transform.forward = resetForward;
		GetComponent<PlayerSinking>().reset();
	}
}
