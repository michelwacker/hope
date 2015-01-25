using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{
	public Player player;
	public InteractionMenuManager interactionMenuManager;

	private static bool gameOver = false;

	public static bool isGameOver() {
		return gameOver;
	}

	public static void GameComplete()
	{
		gameOver = true;

		if (!instance.hasEnded)
			instance.EndGame ();
	}
	private static SceneManager instance;
	public bool hasEnded;
	void Awake()
	{
		instance = this;
		hasEnded = false;
	}

	public void EndGame()
	{
		hasEnded = true;

		StartCoroutine (WaitForGameEnd());
	}
	private IEnumerator WaitForGameEnd()
	{
		yield return new WaitForSeconds (3f);

		GameEnd ();
	}
	private void GameEnd()
	{
		// TODO end the game here!!
	}
	void OnEnable()
	{
		Player.WalkBegin += HandleWalkBegin;
		Player.WalkComplete += HandleWalkComplete;
	}

	void OnDisable()
	{
		Player.WalkBegin -= HandleWalkBegin;
		Player.WalkComplete -= HandleWalkComplete;
	}
	void HandleWalkBegin (GameObject gameObject)
	{
		Debug.Log ("HandleWalkBegin");
		interactionMenuManager.HideMenu ();
	}
	void HandleWalkComplete (GameObject gameObject)
	{
		Debug.Log ("HandleWalkComplete");
		ObjectInteraction oi = gameObject.GetComponent<ObjectInteraction> ();

		float diff = oi.transform.position.x - player.transform.position.x;
		if (diff > 0 && !player.facingRight || diff < 0 && player.facingRight)
			player.Flip ();


		if (oi == null || !oi.isBroken || oi.interactWhenBroken)
			interactionMenuManager.ShowMenu (gameObject);
	}
}
