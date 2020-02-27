using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType {
	Gun,
	Ammo
}
	
public abstract class CardClass : ScriptableObject{

	public CardType cardType;
	public string cardName;
	public string description;

	public Sprite symbol;

	public int cost;
}



