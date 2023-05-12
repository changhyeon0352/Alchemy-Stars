using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
	public int Hp { get { return hp; } }
	private int moveLength=2;
	public int MoveLength { get { return moveLength; } }

	public override IEnumerator UnitActualMove(Tile[] pathTiles)
	{
		animator.SetInteger("State", (int)State.Run);
		//transform.position = pathTiles[0].transform.position;
		float sec = 1f;
		//sec 초 동안  connectedPoss[i]에서 connectedPoss[i+1]로 이동하게 구현해봐
		yield return StartCoroutine(Attack(CheckPlayer(Pos)));
		if (pathTiles.Length > 0)
		{
			for (int i = 0; i < pathTiles.Length-1; i++)
			{
				if(pathTiles[i + 1].Pos==tilePlate.PlayerPos)
				{
					break;
				}
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
				yield return StartCoroutine(Attack(CheckPlayer(pathTiles[i+1].Pos)));
			}
		}
		
		animator.SetInteger("State", (int)State.Idle);
	}
	private Vector2Int[] CheckPlayer(Vector2Int pos)
	{
		List<Vector2Int> enemyPositions = new List<Vector2Int>();
		float distance = Vector2Int.Distance(tilePlate.PlayerPos, pos);
		if(distance<1.1f)
		{
			enemyPositions.Add(tilePlate.PlayerPos);
		}
		return enemyPositions.ToArray();
	}
}
