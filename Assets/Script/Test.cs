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
	public TurnManager turnManager;
	public MonsterController monsterController;
	private void Start()
	{
		Tile tile = tilePlate.GetTile(new Vector2Int(0, 4));
		tile.SetUnit(player, TileState.player);
	}
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			for (int i = 0; i < 4; i++)
			{
				monsterController.SpawnEnemy();
			}
			

			turnManager.enabled = true;
			

			
			
		}
		
	}
}
