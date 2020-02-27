using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUI : MonoBehaviour {

	// Reference to player
	public PlayerDeck playerDeck;
	public FPSController playerController;

	// Card UI of player hand
	public List<Image> playerHandImage;

	// Display Equipped
	public Image gunUI;
	public Image ammoUI;
	public Text ammoText;

	Vector3 largerCardSize = new Vector3 (1.3f,1.3f,1.3f);

	void Start () {
	}

	void Update () {
		ShowAmmo();
		ShowCard();
	}

	public void ShowCard() {

		for (int i = 0; i < playerDeck.playerHand.Count; i++) {
			if (playerController.selectedCardIndex == i) 
			{
				playerHandImage[i].rectTransform.localScale = largerCardSize;
			} 
			else 
			{
				playerHandImage[i].rectTransform.localScale = Vector3.one;
			}

			playerHandImage[i].transform.GetChild(0).GetComponent<Image>().sprite = playerDeck.playerHand[i].symbol;
			playerHandImage[i].GetComponentInChildren<Text>().text = playerDeck.playerHand[i].cardName;

			if (playerDeck.playerHand.Count < 5) {
				playerHandImage[i + 1].transform.GetChild(0).GetComponent<Image>().sprite = null;
				playerHandImage[i + 1].GetComponentInChildren<Text>().text = "No Card";
				playerHandImage[i + 1].rectTransform.localScale = Vector3.one;
			}
		}
	
	}

	void ShowAmmo() {
		if (playerController.gunEquipped != null) {
			gunUI.sprite = playerController.gunEquipped.symbol;
			ammoText.text = playerController.currentAmmo + "/" + playerController.maxAmmo;
		}

		if (playerController.ammoEquipped != null) {
			ammoUI.sprite = playerController.ammoEquipped.symbol;
		}
	}
}
