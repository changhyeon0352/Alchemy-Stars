using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Data", menuName = "Scriptable Object/Skill Data", order = int.MaxValue)]
[System.Serializable]
public class SkillData:ScriptableObject
{
	public int[] comboCondition;
	public float damageMultiple;
	public List<SerializableVector2IntArray> skillAreaList;

	public Vector2Int[] GetSkillArea(int combo)
	{
		for (int i= comboCondition.Length-1; i>=0;i--)
		{
			if(combo>=comboCondition[i])
			{
				return skillAreaList[i].array;
			}
		}
		return null;
	}
}
[System.Serializable]
public class SerializableVector2IntArray
{
	public Vector2Int[] array;
}