using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
	protected int hp;
	protected int hpMax;

	
	public abstract void TakeDamage(int damage);
}
