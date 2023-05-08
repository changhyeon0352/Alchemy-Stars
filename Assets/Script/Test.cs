using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	public Player player;
	public TilePlate tilePlate;
	public TileConnecter tileConnecter;

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			Tile tile = tilePlate.GetTile(0, 4);
			player.SetTile(tile);
			tileConnecter.AddPlayerTile();
			Enemy enemy = FindObjectOfType<Enemy>();
			Tile enemyTile = tilePlate.GetTile(0, 5);
			enemyTile.SetUnit(enemy,TileState.enemy);
		}
		
	}
}
