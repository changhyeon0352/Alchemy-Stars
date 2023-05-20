using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
	protected int hp;
	protected Vector2Int position;
	protected Animator animator;
	protected TilePlate tilePlate;
	protected Unit enemy;
	protected UnitData data;
	protected UnitHpBar hpBar;

	public UnitData Data { get { return data; } }
	public virtual Vector2Int Pos { get { return position; } }
	public abstract int Hp { get; protected set; }
	protected virtual void Awake()
	{
		animator = GetComponent<Animator>();
		tilePlate=FindObjectOfType<TilePlate>();
	}
	public void Initialize(UnitData data)
	{
		this.data = data;
		hp = data.HP;
	}

	public abstract IEnumerator UnitActualMove(Tile[] pathTiles);
	
	public void SetPosition(Vector2Int position)
	{
		this.position = position;
		transform.position = tilePlate.GetTile(position).transform.position;
	}
	public virtual void TakeDamage(int damage)
	{
		animator.SetTrigger("Hit");
		Hp -= damage;
		hpBar.UpdateHpBar(Hp, data.HP);
		if (Hp <= 0)
		{
			Debug.Log($"{data.Name}가 죽었다");
		}
		else
		{
			Debug.Log($"{data.Name}는 {damage}의 데미지를 입었다\n남은 체력 {Hp}");
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
	public  void Hit()
	{
		enemy.TakeDamage(data.Atk);
		enemy = null;
	}
	public void SetHpbar(UnitHpBar hpbar)
	{
		this.hpBar = hpbar;
	}
}
