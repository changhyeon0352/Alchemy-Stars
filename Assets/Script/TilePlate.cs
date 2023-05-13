
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePlate : MonoBehaviour
{
	List<Monster> enemyList;
	Player player;
	public const int size = 9;
	Tile[,] tileGrid;

	public Player Player { get { return player; } }
	public Tile[,] TileGrid { get { return tileGrid; } }
	private void Awake()
	{
		enemyList = new List<Monster>();
		InitializeTilePlate();
		player = FindObjectOfType<Player>();
	}

	private void InitializeTilePlate()
	{
		tileGrid = new Tile[size, size];
		Tile[] tiles = GetComponentsInChildren<Tile>();
		foreach(Tile tile in tiles)
		{
			
			string str = tile.name;
			int i = int.Parse(str[5].ToString());
			int j = int.Parse(str[7].ToString());
			int rand = UnityEngine.Random.Range(0, 4);
			tile.Initialize(i, j, (ElementAttribute)rand);
			//tile.
			tileGrid[i, j] = tile;
		}
		
	}

	public Tile GetTile(Vector2Int pos)
	{
		return tileGrid[pos.x, pos.y];
	}
	public Vector2Int[] GetAdjacentEnemyTilePos(Vector2Int pos)
	{
		List<Vector2Int> enemyPositions = new List<Vector2Int>();
		int[] dx = { -1, 1, 0, 0 };
		int[] dy = { 0, 0, -1, 1 };
		for(int i=0;i<dx.Length;i++)
		{
			if (pos.x + dx[i]<9&&pos.x + dx[i]>-1&& pos.y + dy[i] < 9 && pos.y + dy[i] > -1&&
				tileGrid[pos.x + dx[i], pos.y + dy[i]]!=null&& 
				tileGrid[pos.x + dx[i], pos.y + dy[i]].TileState == TileState.monster)
			{
				enemyPositions.Add(new Vector2Int(pos.x + dx[i], pos.y + dy[i]));
			}
		}
		return enemyPositions.ToArray();
	}

	public IEnumerator Move(Unit unit, Tile[] path)
	{
		TileState newtileState = unit is HeroUnit ? TileState.player : TileState.monster;
		
		GetTile(unit.Pos).SetUnit(null,TileState.empty);
		yield return StartCoroutine(unit.UnitActualMove(path));
		if (GetTile(unit.Pos).TileState==TileState.empty)
		{
			GetTile(unit.Pos).SetUnit(unit, newtileState);
		}
		
	}
	public void PlaceMonsterAtRandomTile(Unit unit)
	{
		
		bool isEnd = false;
		while(!isEnd)
		{
			int randx = Random.Range(1, 8);
			int randy = Random.Range(1, 8);
			if (tileGrid[randx,randy].TileState==TileState.empty)
			{
				tileGrid[randx, randy].SetUnit(unit, TileState.monster);
				isEnd = true;
			}
		}

		
	}
}
