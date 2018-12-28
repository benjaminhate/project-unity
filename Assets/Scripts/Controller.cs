﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControllerType {
	PLAYER,
	ENEMY
}

[System.Serializable]
public class ControllerState {
	public Characteristics characteristics;
	public ControllerType type;
	public SpriteManager spriteManager;
}

public class Controller : MonoBehaviour{
	public ControllerState innerState;

	public void ChangeController(ControllerState other){
		innerState.characteristics = other.characteristics;
		innerState.type = other.type;
		innerState.spriteManager = other.spriteManager;
	}

	public void UpdateController(){
		GetComponent<SpriteRenderer> ().sprite = innerState.spriteManager.sprite;
		GetComponent<SpriteRenderer> ().color = innerState.spriteManager.color;
		transform.localScale = innerState.spriteManager.scale;
	}

	public void SaveCurrentState(){
		innerState.spriteManager.sprite = GetComponent<SpriteRenderer> ().sprite;
		innerState.spriteManager.color = GetComponent<SpriteRenderer> ().color;
		innerState.spriteManager.scale = transform.localScale;
	}

	public ControllerState SaveController(){
		ControllerState save = new ControllerState ();
		save.characteristics = innerState.characteristics;
		save.type = innerState.type;
		save.spriteManager = innerState.spriteManager;
		return save;
	}
}
