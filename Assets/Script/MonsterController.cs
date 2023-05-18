using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
	private List<Monster> monsterList=new List<Monster>();
	TilePlate tilePlate;
	public GameObject EnemyPrefab;
	public UnitData unitData;
	private void Awake()
	{
		tilePlate = FindObjectOfType<TilePlate>();
	}
	public IEnumerator MonsterTurn()
	{
		AStarPathfinding aStar = new AStarPathfinding(TilePlate.size, TilePlate.size);
		for (int i = 0; i < TilePlate.size; i++)
		{
			for (int j = 0; j < TilePlate.size; j++)
			{
				if (tilePlate.TileGrid[i, j] == null || tilePlate.TileGrid[i, j].TileState == TileState.monster)
				{
					aStar.SetObstacle(i, j);
				}
			}

		}
		Vector2Int[] monsterPoss=new Vector2Int[monsterList.Count];
		for(int i=0; i<monsterList.Count; i++)
		{
			monsterPoss[i] = monsterList[i].Pos;
		}
		List<List<Vector2Int>> pathList= aStar.FindPath(monsterPoss, tilePlate.Player.Pos);
		for (int j = 0; j < monsterList.Count; j++)
		{
			while (pathList[j].Count-1 > monsterList[j].MoveLength)
			{
				pathList[j].RemoveAt(pathList[j].Count - 1);
			}
			Tile[] tiles = new Tile[pathList[j].Count];
			for (int i = 0; i < pathList[j].Count; i++)
			{
				tiles[i] = tilePlate.GetTile(pathList[j][i]);
			}
			if (j == monsterList.Count - 1)
			{
				yield return StartCoroutine(tilePlate.Move(monsterList[j], tiles));
			}
			else
			{
				StartCoroutine(tilePlate.Move(monsterList[j], tiles));
			}
			
		}
		
	}
	public void SpawnEnemy()
	{
		GameObject obj=Instantiate(EnemyPrefab);
		Monster unit =obj.GetComponent<Monster>();
		unit.Initialize(unitData);
		monsterList.Add(unit);
		tilePlate.PlaceMonsterAtRandomTile(unit);

	}

}
