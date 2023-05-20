using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Player : MonoBehaviour
{
	[SerializeField]
    UnitData[] heroDatas;
	HeroUnit[] heroUnits;
	HeroUnit leaderUnit;
	Vector2Int position;
	TilePlate tilePlate;
	TurnManager turnManager;
	public GameObject hpbarPrefab;
	public Transform hpbarTr;

	int hpMax;
	int hp;
	public Vector2Int Pos { get { return position; } }
	public HeroUnit LeaderUnit { get { return leaderUnit; } }
	
	public int Hp { get { return hp; }set { hp = value; } }
	public UnitData[] HeroDatas { get { return heroDatas; } }

	private void Awake()
	{
		tilePlate = FindObjectOfType<TilePlate>();
		turnManager=FindObjectOfType<TurnManager>();

	}

	public void Initialize(int x, int y)
	{
		position = new Vector2Int(x, y);
		heroUnits=new HeroUnit[heroDatas.Length];
		UnitHpBar hpbar = Instantiate(hpbarPrefab, hpbarTr).GetComponent<UnitHpBar>();
		
		for(int i = 0; i < heroDatas.Length; i++)
		{
			hpMax += heroDatas[i].HP;
			heroUnits[i]= Instantiate(heroDatas[i].Prefab,transform).GetComponent<HeroUnit>();
			heroUnits[i].Initialize(heroDatas[i]);
			heroUnits[i].SetPosition(position);
			if (i != 0)
			{
				heroUnits[i].gameObject.SetActive(false);
			}
			heroUnits[i].SetHpbar(hpbar);
		}
		hp = hpMax;
		leaderUnit = heroUnits[0];
		hpbar.SetUnitInfo(leaderUnit);
		

		Tile tile = tilePlate.GetTile(Pos);
		tile.SetUnit(LeaderUnit,TileState.player);
		leaderUnit.transform.position = tile.transform.position;
	}
	public IEnumerator playerMove(Tile[] path,ElementAttribute lineElement)
	{
		HeroUnit[] elementUnits = heroUnits.Where(hero => (hero.Data.ElementAttribute == lineElement||hero==leaderUnit)).ToArray();
		for (int i = 0; i < elementUnits.Length; i++)
		{
			elementUnits[i].gameObject.SetActive(true);
			elementUnits[i].transform.position = path[0].transform.position;
			if(i< elementUnits.Length-1)
			{
				StartCoroutine(tilePlate.Move(elementUnits[i], path));
				yield return new WaitForSeconds(1);
			}
			else
			{
				yield return StartCoroutine(tilePlate.Move(elementUnits[i], path));

			}
		}
		position = path[path.Length-1].Pos;
		for (int i = 0; i < elementUnits.Length; i++)
		{
			yield return StartCoroutine(elementUnits[i].ComboSkill(path.Length));
		
			
		}
		foreach (HeroUnit heroUnit in heroUnits)
		{
			heroUnit.SetPosition(position);
			if(heroUnit!=leaderUnit)
			{
				heroUnit.gameObject.SetActive(false);
			}
		}
		turnManager.TurnEnd();
	}
}
