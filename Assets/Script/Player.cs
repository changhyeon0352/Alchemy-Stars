using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
	Idle=0,
	Run,
	Attack
}
public class Player : Unit
{
	int damage=10;
	[SerializeField]
	TurnManager turnManager;

	
	
	public void Hit()
	{
		enemy.TakeDamage(damage);
		enemy = null;
	}

	public override IEnumerator UnitActualMove(Tile[] pathTiles)
	{
		animator.SetInteger("State", (int)State.Run);
		float sec = 1f;
		yield return StartCoroutine(Attack(tilePlate.GetAdjacentEnemyTilePos(Pos)));
		//sec 초 동안  connectedPoss[i]에서 connectedPoss[i+1]로 이동하게 구현해봐
		for (int i = 0; i < pathTiles.Length-1; i++)
		{
			float timeElapsed = 0;
			float distance = Vector3.Distance(pathTiles[i].transform.position, pathTiles[i + 1].transform.position);
			float speed = distance / sec;
			transform.LookAt(pathTiles[i + 1].transform);
			while (timeElapsed < sec)
			{
				transform.position += speed * (pathTiles[i + 1].transform.position - pathTiles[i].transform.position).normalized * Time.deltaTime;
				timeElapsed += Time.deltaTime;
				yield return null;
			}
			SetPosition(pathTiles[i + 1].Pos);
			yield return StartCoroutine(Attack(tilePlate.GetAdjacentEnemyTilePos(pathTiles[i+1].Pos)));
		}
		animator.SetInteger("State", (int)State.Idle);
		turnManager.TurnEnd();
	}
}
