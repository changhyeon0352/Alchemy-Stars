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
			Debug.Log("���Ͱ� �׾���");
		}
		else
		{
			Debug.Log($"���ʹ� {damage}�� �������� �Ծ���\n���� ü�� {hp}");
		}
		
	}
}
