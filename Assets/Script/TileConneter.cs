using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileConneter : MonoBehaviour
{
	private List<Tile> connectedLists;
	bool isConnetStart=false;
	[SerializeField]
	LineRenderer lineRenderer;
	public Tile LastTile { get { return connectedLists[connectedLists.Count-1]; } }

	public bool IsConnetStart { get { return isConnetStart; } }

	public void StartConneting(Tile tile)
	{
		if(connectedLists == null)
			connectedLists = new List<Tile>();
		AddTile(tile);
		isConnetStart = true;
	}
	public void AddTile(Tile tile)
	{
		connectedLists.Add(tile);
		Debug.Log("Add " + tile.Pos);
		ConnetLine();
	}
	public void RemoveFromTileToLast(Tile tile)
	{
		int index = connectedLists.IndexOf(tile) + 1;
		if (index >= connectedLists.Count)
			return;
		connectedLists.RemoveRange(index, connectedLists.Count - index);
		ConnetLine();
	}
	public bool IsContainTile(Tile tile)
	{
		return connectedLists.Contains(tile);
	}
	public void EndConneting()
	{
		foreach(Tile tile in connectedLists)
		{
			Debug.Log("«ÿ¡¶"+tile.Pos);
		}
		connectedLists.Clear();
		lineRenderer.enabled = false;
		isConnetStart=false;
	}
	private void ConnetLine()
	{
		Vector3[] positions3D=new Vector3[connectedLists.Count];
		for(int i=0;i<connectedLists.Count;i++)
		{
			positions3D[i] = connectedLists[i].transform.position + Vector3.up * 0.1f;
		}
		lineRenderer.enabled = true;
		lineRenderer.positionCount = positions3D.Length;
		lineRenderer.SetPositions(positions3D);
	}
}
