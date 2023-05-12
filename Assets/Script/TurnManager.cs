using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
	public GameObject blockingWindow; // ������ â�� ��Ÿ���� UI ���

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

		// �÷��̾� �Է��� ���� ���� ������ â�� Ȱ��ȭ�ϰ� Ŭ���� �����ϴ�.
		blockingWindow.SetActive(true);

		yield return StartCoroutine(monsterController.MonsterTurn());

		// �÷��̾� �Է��� �ٽ� ����ϱ� ���� ������ â�� ��Ȱ��ȭ�ϰ� Ŭ���� ����մϴ�.
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
