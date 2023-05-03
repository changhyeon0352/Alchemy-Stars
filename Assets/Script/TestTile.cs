using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTile : MonoBehaviour
{
	[SerializeField]
	private GameObject tilePrefab;
	[SerializeField]
	Transform planeTr;
	const int size = 9;
	[SerializeField]
	Transform tr;

	private void Start()
	{
		float f = (float)size / 2f - 0.5f;
		Vector3 pos = new Vector3(-f, 0, -f);
		for (int i = 0; i < size; i++)
		{
			
			for(int j = 0; j < size; j++)
			{
				if((i% (size-1)== 0&&j%(size-2)<=1)||(j% (size - 1) == 0&&i% (size - 2) <= 1))
				{
					pos.x++;
					continue;
				}
				GameObject obj=Instantiate(tilePrefab, pos, Quaternion.identity,planeTr);
				Tile newTile=obj.GetComponent<Tile>();
				int count = Enum.GetNames(typeof(TileColor)).Length;
				int rand = UnityEngine.Random.Range(0, count);
				newTile.Initialize(i, j,(TileColor)rand);
				pos.x++;
			}
			pos.z++;
			pos.x -= size;
		}
		tr.localScale = new Vector3(0.7f,1,1);
	}
}
