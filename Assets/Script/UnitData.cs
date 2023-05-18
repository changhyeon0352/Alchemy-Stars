using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Unit Data", menuName = "Scriptable Object/Unit Data", order = int.MaxValue)]
public class UnitData:ScriptableObject
{
	[SerializeField]
	int id;
	public int ID { get { return id; } }
	[SerializeField]
	string unitname;
	public string Name { get { return unitname; } }
	[SerializeField]
	int atk;
	public int Atk { get { return atk; } }
	[SerializeField]
	int hp;
	public int HP { get { return hp; } }
	[SerializeField]
	ElementAttribute elementAttribute;
	public ElementAttribute ElementAttribute { get { return elementAttribute; } }
	[SerializeField]
	GameObject prefab;
	public GameObject Prefab { get { return prefab; } }

}
