using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileConnecter : MonoBehaviour
{
	private List<Tile> connectedTileList;
	bool isConnetStart=false;
	[SerializeField]
	LineRenderer lineRenderer;
	Player player;
	public Tile LastTile { get { return connectedTileList[connectedTileList.Count-1]; } }

	public bool IsConnetStart { get { return isConnetStart; } }


	private void Awake()
	{
		if (lineRenderer == null)
		{
			lineRenderer = FindObjectOfType<LineRenderer>();
		}
		player = FindObjectOfType<Player>();
	}
	private void Start()
	{
		

	}
	public void AddPlayerTile()
	{
		if (connectedTileList == null)
			connectedTileList = new List<Tile>();
		connectedTileList.Add(player.tile);
	}
	public void StartConnecting(Tile tile)
	{
		
		AddTile(tile);
		isConnetStart = true;
	}
	public void AddTile(Tile tile)
	{
		connectedTileList.Add(tile);
		Debug.Log("Add " + tile.Pos);
		ConnectLine();
	}
	public void RemoveFromTileToLast(Tile tile)
	{
		int index = connectedTileList.IndexOf(tile) + 1;
		if (index >= connectedTileList.Count)
			return;
		connectedTileList.RemoveRange(index, connectedTileList.Count - index);
		ConnectLine();
	}
	public bool IsContainTile(Tile tile)
	{
		return connectedTileList.Contains(tile);
	}
	public void EndConneting()
	{
		if (connectedTileList.Count > 1)
		{
			StartCoroutine(player.Move(connectedTileList.ToArray()));
			foreach (Tile tile in connectedTileList)
			{
				Debug.Log("����" + tile.Pos);
			}
			connectedTileList.Clear();
			isConnetStart = false;
		}
		lineRenderer.enabled = false;
	}
	private void ConnectLine()
	{
		if(lineRenderer ==null)
		{
			lineRenderer = FindObjectOfType<LineRenderer>();
		}

		Vector3[] positions3D = GetConnectedPosArray();
		lineRenderer.enabled = true;
		lineRenderer.positionCount = positions3D.Length;
		lineRenderer.SetPositions(positions3D);
	}
	private Vector3[] GetConnectedPosArray()
	{
		Vector3[] positions3D = new Vector3[connectedTileList.Count];
		for (int i = 0; i < connectedTileList.Count; i++)
		{
			positions3D[i] = connectedTileList[i].transform.position + Vector3.up * 0.1f;
		}
		return positions3D;
	}
}