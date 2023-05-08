using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
	Idle=0,
	Run,
	Attack
}
public class Player : MonoBehaviour
{
    Animator animator;
	public Tile tile;
	TilePlate tilePlate;
	Unit enemy;
	int damage;
	[SerializeField]
	TurnManager turnManager;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
		tilePlate = FindObjectOfType<TilePlate>();
    }
	public void SetTile(Tile tile)
	{
		this.tile = tile;
		transform.position = tile.transform.position;
	}
    
	public IEnumerator Move(Tile[] connectedTiles)
	{
		animator.SetInteger("State", (int)State.Run);
		transform.position=connectedTiles[0].transform.position;
		float sec = 1f;
		//sec 초 동안  connectedPoss[i]에서 connectedPoss[i+1]로 이동하게 구현해봐
		for (int i = 0; i < connectedTiles.Length; i++)
		{
			yield return StartCoroutine(Attack(tilePlate.GetAdjacentEnemyTilePos(connectedTiles[i].Pos.x, connectedTiles[i].Pos.y)));
			if(i==connectedTiles.Length-1)
			{
				SetTile(connectedTiles[i]);
				break;
			}
			float timeElapsed = 0;
			float distance= Vector3.Distance(connectedTiles[i].transform.position, connectedTiles[i+1].transform.position);
			float speed= distance/ sec;
			transform.LookAt(connectedTiles[i + 1].transform);
			while (timeElapsed < sec)
			{
				transform.position += speed * (connectedTiles[i + 1].transform.position - connectedTiles[i].transform.position).normalized * Time.deltaTime;
				timeElapsed +=Time.deltaTime;
				yield return null;
			}
			
		}
		animator.SetInteger("State",(int)State.Idle);
		turnManager.TurnEnd();
	}
	private IEnumerator Attack(Vector2Int[] enemyPoss)
	{
		int damage = 10;
		foreach(Vector2Int enemyPos in enemyPoss)
		{
			Tile enemyTile = tilePlate.GetTile(enemyPos.x, enemyPos.y);
			transform.LookAt(enemyTile.transform);
			enemy = enemyTile.Unit;
			animator.SetTrigger("Attack");
			
			yield return new WaitForSeconds(1f);
			
		}
	}
	public void Hit()
	{
		enemy.TakeDamage(damage);
		enemy = null;
	}
}
