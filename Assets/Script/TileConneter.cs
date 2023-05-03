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
		if(connectedLists.Contains(tile))
		{
			int index=connectedLists.IndexOf(tile)+1;
			if (index >= connectedLists.Count)
				return;
			connectedLists.RemoveRange(index, connectedLists.Count - index);
		}
		else
		{
			connectedLists.Add(tile);
			Debug.Log("Add " + tile.Pos);
		}
		
		ConnetLine();
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
			positions3D[i] = new Vector3(connectedLists[i].Pos.y-4, 0.1f, connectedLists[i].Pos.x-4);
		}
		lineRenderer.enabled = true;
		lineRenderer.positionCount = positions3D.Length;
		lineRenderer.SetPositions(positions3D);
	}
}
