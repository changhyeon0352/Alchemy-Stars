using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
	public int Hp => hp;

	public override void TakeDamage(int damage)
	{
		hp-=damage;
		if(hp<=0)
		{
			Debug.Log("몬스터가 죽었다");
		}
		else
		{
			Debug.Log($"몬스터는 {damage}의 데미지를 입었다\n남은 체력 {hp}");
		}
		
	}
}
