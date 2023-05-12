using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
	private List<Monster> monsterList=new List<Monster>();
	TilePlate tilePlate;
	public GameObject EnemyPrefab;
	
	private void Awake()
	{
		tilePlate = FindObjectOfType<TilePlate>();
	}
	public IEnumerator MonsterTurn()
	{
		


		foreach (Monster monster in monsterList)
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
			List<Vector2Int> path=aStar.FindPath(monster.Pos, tilePlate.PlayerPos);
			if(path.Count>monster.MoveLength)
			{
				for(int i=0; path.Count>monster.MoveLength; i++)
				{
					path.RemoveAt(path.Count - 1);
				}
			}
			Tile[] tiles= new Tile[path.Count+1];
			tiles[0] = tilePlate.GetTile(monster.Pos);
			for (int i=0; i< path.Count; i++)
			{
				tiles[i+1] = tilePlate.GetTile(path[i]);
			}
			yield return StartCoroutine(tilePlate.Move(monster,tiles));
		}
		
	}
	public void SpawnEnemy()
	{
		GameObject obj=Instantiate(EnemyPrefab);
		Monster unit =obj.GetComponent<Monster>();
		monsterList.Add(unit);
		tilePlate.PlaceMonsterAtRandomTile(unit);

	}

}
