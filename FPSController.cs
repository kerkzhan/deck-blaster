using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSController : MonoBehaviour {

	// Player Movement
	[SerializeField] float playerSpeed = 100f;
	Rigidbody rb;
	Vector3 desiredMovement;

	// Player Shooting
	public GameObject bulletPrefab;
	public Transform bulletSpawnPoint;
	//public GunCard single;
	//public GunCard spread;
	//public GunCard rapid;
	public GunCard gunEquipped;
	public AmmoCard ammoEquipped;

	bool shooting;
	bool canShoot;

	public int currentAmmo;
	public int maxAmmo;

	//Player Deck
	public int selectedCardIndex;
	CardClass selectedCard;
	PlayerDeck playerDeck;

	void Awake () {
		rb = this.GetComponent<Rigidbody>();
		shooting = false;
		canShoot = true;
		//gunEquipped = single;
		//currentAmmo = gunEquipped.magSize;
		//maxAmmo = gunEquipped.magSize;

	}

	void Start () {
		playerDeck = this.GetComponent<PlayerDeck>();
		selectedCardIndex = 0;
		selectedCard = playerDeck.playerHand[selectedCardIndex];
		ObjectPoolManager.Instance.CreatePool(bulletPrefab, 60, 200);
	}

	void Update () {
		GetMoveInput();

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			shooting = true;
			StartCoroutine(Shoot());

		} else if (Input.GetKeyUp(KeyCode.Mouse0)) 
		{
			shooting = false;
		}

		if (Input.GetKeyDown(KeyCode.Mouse1)) 
		{
			SelectCard();
		}

		if (Input.GetKeyDown(KeyCode.Space)) 
		{
			UseSelectedCard();
		}


		//SwitchGunCard();
			
	}

	void FixedUpdate() {
		Move();
	}

	void GetMoveInput() {
		float horizontal;
		float vertical;

		horizontal = Input.GetAxisRaw("Horizontal");
		vertical = Input.GetAxisRaw("Vertical");

		desiredMovement = horizontal * transform.right + vertical * transform.forward;

	}

	void Move() {
		Vector3 yVel = new Vector3 (0, rb.velocity.y, 0);
		rb.velocity = desiredMovement * playerSpeed * Time.fixedDeltaTime;
		rb.velocity += yVel;
	}

	IEnumerator Shoot() {

		if (canShoot && currentAmmo > 0 && gunEquipped != null && ammoEquipped != null) {

			//List<GameObject> goList = new List<GameObject>();

			for (int i = 0; i < gunEquipped.ammoCost; i++) {

				GameObject bullet = ObjectPoolManager.Instance.GetObject(bulletPrefab.name);
				BulletCollision bCol = bullet.GetComponent<BulletCollision>();

				bullet.transform.position = bulletSpawnPoint.position;
				bullet.transform.rotation = bulletSpawnPoint.rotation;

				Vector3 bulletDirection =  new Vector3 (
					Random.Range(-gunEquipped.spread, gunEquipped.spread) * Mathf.Cos(Random.Range(0, 2 * Mathf.PI)), 
					Random.Range(-gunEquipped.spread, gunEquipped.spread) * Mathf.Sin(Random.Range(0, 2 * Mathf.PI)), 
					Random.Range(-gunEquipped.spread, gunEquipped.spread) * Mathf.Sin(Random.Range(0, 2 * Mathf.PI)));
				
				bulletDirection = (bulletDirection + bullet.transform.forward).normalized;

				bCol.bulletSpeed = gunEquipped.bulletSpeed;
				bCol.ammoEffect = ammoEquipped;
				bullet.GetComponent<Rigidbody>().velocity = bulletDirection;
				bCol.BulletSetup();

				/*GameObject go = Instantiate (bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

				Vector3 bulletDirection = new Vector3 (Random.Range(-gunEquipped.spread, gunEquipped.spread) * Mathf.Cos(Random.Range(0, 2 * Mathf.PI)), Random.Range(-gunEquipped.spread, gunEquipped.spread) * Mathf.Sin(Random.Range(0,2*Mathf.PI)), Random.Range(-gunEquipped.spread, gunEquipped.spread) * Mathf.Sin(Random.Range(0,2*Mathf.PI)));

				//Vector3 bulletDirection = new Vector3 (Random.Range(-gunEquipped.spread, gunEquipped.spread), Random.Range (-gunEquipped.spread, gunEquipped.spread), 0f);
				bulletDirection = (bulletDirection + go.transform.forward).normalized;

				go.GetComponent<Rigidbody>().velocity = bulletDirection;
				BulletCollision bc = go.GetComponent<BulletCollision>();

				bc.bulletSpeed = gunEquipped.bulletSpeed;
				bc.ammoEffect = ammoEquipped;

				goList.Add(go);*/

			}


			currentAmmo = currentAmmo - gunEquipped.ammoCost;

			/*foreach (GameObject bulletToDestroy in goList) {
				Destroy(bulletToDestroy, 2.0f);
			}*/

			canShoot = false;

			//recoil.StartRecoil(shootSpeed/2, maxRecoil, recoilSpeed);
		} else {
			yield break;
		}

		yield return new WaitForSeconds(gunEquipped.shootSpeed);

		canShoot = true;

		if (shooting) {
			StartCoroutine(Shoot());
		}

		else {
			StopCoroutine(Shoot());
		}

	}

	/*void SwitchGunCard() {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			gunEquipped = single;
			currentAmmo = gunEquipped.magSize;
			maxAmmo = gunEquipped.magSize;

		} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
			gunEquipped = spread;
			currentAmmo = gunEquipped.magSize;
			maxAmmo = gunEquipped.magSize;

		} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			gunEquipped = rapid;
			currentAmmo = gunEquipped.magSize;
			maxAmmo = gunEquipped.magSize;
		} 

		if (currentAmmo == 0) {
			canShoot = true;
		}
	}*/

	void UseSelectedCard () 
	{
		if (playerDeck.playerHand.Count > 0) 
		{
			Debug.Log("YO I have still cards");

			if (selectedCard.cardType == CardType.Gun)
			{
				gunEquipped = selectedCard as GunCard;
				maxAmmo = gunEquipped.magSize;
				currentAmmo = maxAmmo;
			}

			else if (selectedCard.cardType == CardType.Ammo)
			{
				Debug.Log("Hrllo?");
				ammoEquipped = selectedCard as AmmoCard;
				if (gunEquipped != null) {
					maxAmmo = gunEquipped.magSize;
					currentAmmo = maxAmmo;
				}
			}

			playerDeck.discardedCards.Add(playerDeck.playerHand[selectedCardIndex]);
			playerDeck.playerHand.RemoveAt(selectedCardIndex);
			playerDeck.DrawCards(1);

			if (selectedCardIndex > playerDeck.playerHand.Count - 1 && playerDeck.playerHand.Count > 0) 
			{
				selectedCardIndex -= 1;
			} 

			else if (selectedCardIndex == playerDeck.playerHand.Count)
			{
				return;
			}

			selectedCard = playerDeck.playerHand[selectedCardIndex];
		}

		else 
		{
			Debug.Log("No more cards in hand");
		}
	}

	void SelectCard() 
	{
		selectedCardIndex = (selectedCardIndex + 1) % playerDeck.playerHand.Count;
		selectedCard = playerDeck.playerHand[selectedCardIndex];
	}
}
