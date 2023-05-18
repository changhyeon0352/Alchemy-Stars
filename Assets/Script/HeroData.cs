using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Hero Data", menuName = "Scriptable Object/Hero Data", order = int.MaxValue)]
public class HeroData: UnitData
{
	[SerializeField]
	SkillData skillData;
	public SkillData SkillData { get { return skillData; } }
}
