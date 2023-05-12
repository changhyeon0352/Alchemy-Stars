using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
	protected int hp;
	protected int hpMax=100;
	protected Vector2Int position;
	protected Animator animator;
	protected TilePlate tilePlate;
	protected Unit enemy;
	protected int damage=10;
	public Vector2Int Pos { get { return position; } }
	protected virtual void Awake()
	{
		animator = GetComponent<Animator>();
		hp = hpMax;
		tilePlate=FindObjectOfType<TilePlate>();
	}
	
	public abstract IEnumerator UnitActualMove(Tile[] pathTiles);
	
	public void SetPosition(Vector2Int position)
	{
		this.position = position;
	}
	public virtual void TakeDamage(int damage)
	{
		animator.SetTrigger("Hit");
		hp -= damage;
		if (hp <= 0)
		{
			Debug.Log($"{this.name}�� �׾���");
		}
		else
		{
			Debug.Log($"{this.name}�� {damage}�� �������� �Ծ���\n���� ü�� {hp}");
		}
	}
	protected IEnumerator Attack(Vector2Int[] enemyPoss)
	{
		foreach (Vector2Int enemyPos in enemyPoss)
		{
			Tile enemyTile = tilePlate.GetTile(enemyPos);
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
