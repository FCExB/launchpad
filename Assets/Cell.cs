using UnityEngine;
using System.Collections;

public class Cell : ScriptableObject {
	
	public int x, y;
	
	public Cell(int x, int y) {
		this.x = x;
		this.y = y;
	}
	
	public override bool Equals(object obj) {
		if (obj == null || GetType() != obj.GetType()) 
			return false;
		
		Cell that = (Cell)obj;
		return x.Equals(that.x) && y.Equals(that.y);	
	}
	
	public override int GetHashCode() {
		return x*8+y;
	}
}
