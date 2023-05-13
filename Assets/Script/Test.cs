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
		player.Initialize(0, 4);

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
