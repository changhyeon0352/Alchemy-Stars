using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePlate : MonoBehaviour
{
	[SerializeField]
	private GameObject tilePrefab;
	[SerializeField]
	Transform planeTr;
	const int size = 9;
	[SerializeField]
	Transform tr;
	Tile[,] tiles;

	private void Awake()
	{
		InitializeRandomTIlePlate();
	}

	private void InitializeRandomTIlePlate()
	{
		tiles = new Tile[size, size];
		float f = (float)size / 2f - 0.5f;
		Vector3 pos = new Vector3(-f, 0, -f);
		for (int i = 0; i < size; i++)
		{

			for (int j = 0; j < size; j++)
			{
				if ((i % (size - 1) == 0 && j % (size - 2) <= 1) || (j % (size - 1) == 0 && i % (size - 2) <= 1))
				{
					pos.x++;
					continue;
				}
				GameObject obj = Instantiate(tilePrefab, pos, Quaternion.identity, planeTr);
				Tile newTile = obj.GetComponent<Tile>();
				int count = Enum.GetNames(typeof(EelementAttributes)).Length;
				int rand = UnityEngine.Random.Range(0, count);
				newTile.Initialize(i, j, (EelementAttributes)rand);
				tiles[i, j] = newTile;
				pos.x++;
			}
			pos.z++;
			pos.x -= size;
		}
		tr.localScale = new Vector3(0.7f, 1, 1);
	}

	public Tile GetTile(int x, int y)
	{
		return tiles[x, y];
	}
	public Vector2Int[] GetAdjacentEnemyTilePos(int x, int y)
	{
		List<Vector2Int> enemyPositions = new List<Vector2Int>();
		for (int i = -1; i < 2; i ++)
		{
			for(int j=-1; j<2;j++)
			{
				if (x+i>=0&&x+i<9&& y + i >= 0 && y + i < 9&&Mathf.Abs(i+j)==1&&
					tiles[x + i, y + j] !=null&& tiles[x +i, y+j].TileState == TileState.enemy)
				{
					enemyPositions.Add(new Vector2Int(x + i, y + j));
				}
			}
		}
		return enemyPositions.ToArray();
		
	}
}
