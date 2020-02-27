using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCard : CardClass {

	[Space]
	public int damage;
	public int ammoCost;
	public int magSize;
	public float shootSpeed;
	public float bulletSpeed;
	public float spread;

	public GunCard() {
		cardType = CardType.Gun;
	}

}
