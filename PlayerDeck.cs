using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour {

	// Different decks
	public List<CardClass> playerDeck = new List<CardClass>();
	public List<CardClass> playerHand = new List<CardClass>();
	public List<CardClass> discardedCards = new List<CardClass>();

	// Reference to player
	FPSController fpsController;


	void Start () {

		CardClass[] tempDeck = Resources.LoadAll<CardClass>("Cards");

		int random;

		do {
			random = Random.Range(0, tempDeck.Length);
			playerDeck.Add(tempDeck[random]);
				
		} while (playerDeck.Count < 20);

		DrawCards(5);
			
	}
	
	// Update is called once per frame
	void Update () {
		if (playerDeck.Count == 0 && playerHand.Count == 00) {
			ResetDeckFromDiscard();
		}

		if (Input.GetKeyDown(KeyCode.E)) {
			ShuffleCards(playerDeck);
		}
			
	}

	public void DrawCards(int amount) {

		int cardsToDraw = amount;

		if (playerDeck.Count > 0) {
			if (cardsToDraw > 0) {
				playerHand.Add (playerDeck[0]);
				playerDeck.RemoveAt(0);
				cardsToDraw --;
				DrawCards(cardsToDraw);
			} 
		} else {
			Debug.Log ("No more cards in deck");
		}
			
	}

	void ShuffleCards(List<CardClass> deck) {
		for (int i = 0; i < deck.Count; i++) {
			CardClass temp = deck[i];
			int random = Random.Range(i, deck.Count);
			deck[i] = deck[random];
			deck[random] = temp;
		}
	}

	void ResetDeckFromDiscard() {
		playerDeck.AddRange(discardedCards);
		ShuffleCards(playerDeck);
		discardedCards.Clear();
		DrawCards(5);
	}
}
