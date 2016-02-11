using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Turnable : MonoBehaviour {

	public float turn;
	public float maxTurn = 100;
	public Sprite sprite;
	
	public void IncrementTurn(float amount){
		turn += amount;
	}

	public bool Ready(){
		return(turn <= 0);
	}

	public void ResetTurn(){
		turn = maxTurn;
	}

}
