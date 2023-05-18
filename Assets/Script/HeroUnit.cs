using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
	Idle=0,
	Run,
	Attack
}
public class HeroUnit : Unit
{
	public override Vector2Int Pos { get { return position; } }
	public override int Hp { get => tilePlate.Player.Hp;
		protected set
		{
			tilePlate.Player.Hp = value;
		}
	}
	
	public IEnumerator ComboSkill(int combo)
	{
		animator.SetTrigger("ComboSkill");
		//스킬 프리팹 실행
		
		Vector2Int[] skillArea= (data as HeroData).SkillData.GetSkillArea(combo);
		if(skillArea == null)
		{
			yield break;
		}
		yield return new WaitForSeconds(1f);
		for (int i=0; i<skillArea.Length; i++)
		{
			Tile tile= tilePlate.GetTile(position + skillArea[i]);
			if(tile!=null&&tile.Unit!=null)
			{
				tile.Unit.TakeDamage((int)(data.Atk * (data as HeroData).SkillData.damageMultiple));
			}

		}


	}
	public override IEnumerator UnitActualMove(Tile[] pathTiles)
	{
		
		float sec = 0.5f;
		animator.SetInteger("State", (int)State.Idle);
		yield return StartCoroutine(Attack(tilePlate.GetAdjacentEnemyTilePos(tilePlate.Player.Pos)));
		animator.SetInteger("State", (int)State.Run);
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
		
	}
}
