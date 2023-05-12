using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
	public GameObject blockingWindow; // 투명한 창을 나타내는 UI 요소

	private bool isPlayerTurn;
	private bool isGameOver;
	private bool isComplteTurn=false;
	[SerializeField]
	TileConnecter tileConnecter;
	[SerializeField]
	MonsterController monsterController;
	private void Start()
	{
		isPlayerTurn = true;
		isGameOver = false;
		monsterController=FindObjectOfType<MonsterController>();
		StartCoroutine(GameLoop());
	}

	private IEnumerator GameLoop()
	{
		while (!isGameOver)
		{
			if (isPlayerTurn)
			{
				yield return StartCoroutine(PlayerTurn());
			}
			else
			{
				yield return StartCoroutine(EnemyTurn());
			}

			isPlayerTurn = !isPlayerTurn;
			yield return new WaitForSeconds(1f);
		}
		if (isGameOver)
		{
			Debug.Log("Game Over");
		}
	}

	private IEnumerator PlayerTurn()
	{
		isComplteTurn=false ;
		Debug.Log("Player's Turn");
		tileConnecter.AddPlayerTile();
		while (!isComplteTurn)
		{
			yield return null;
		}
	}

	private IEnumerator EnemyTurn()
	{
		Debug.Log("Enemy's Turn");

		// 플레이어 입력을 막기 위해 투명한 창을 활성화하고 클릭을 막습니다.
		blockingWindow.SetActive(true);

		yield return StartCoroutine(monsterController.MonsterTurn());

		// 플레이어 입력을 다시 허용하기 위해 투명한 창을 비활성화하고 클릭을 허용합니다.
		blockingWindow.SetActive(false);
	}

	private bool CheckGameOver()
	{
		return false;
	}
	public void TurnEnd()
	{
		isComplteTurn = true;
	}
}
