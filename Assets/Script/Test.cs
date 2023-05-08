using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	public Player player;
	public TilePlate tilePlate;
	public TileConnecter tileConnecter;
	[SerializeField]
	GameObject enemyPrefab;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			Tile tile = tilePlate.GetTile(0, 4);
			player.SetTile(tile);
			tileConnecter.AddPlayerTile();

			for(int i = 0; i < 4; i++)
			{
				Enemy enemy = Instantiate(enemyPrefab).GetComponent<Enemy>();
				int x = Random.RandomRange(2, 7);
				int y= Random.RandomRange(2, 7);
				Tile enemyTile = tilePlate.GetTile(x, y);
				enemyTile.SetUnit(enemy, TileState.enemy);
			}
			
		}
		
	}
}
