using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public enum TileColor {
    blue=0,
    green,
    red,
    yellow 
}
public class Tile : MonoBehaviour,IPointerClickHandler,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler,IPointerUpHandler
{
	private int x;
	private int y;
	TileColor color=TileColor.red;
	private bool isOccupied;
    [SerializeField]
    private Material[] colorMaterials;
	TileConneter conneter;
	bool isConneting=false;

	public Vector2Int Pos { get { return new Vector2Int(x,y); } }
	public TileColor Color { get { return color; } }
	public void Initialize(int x, int y, TileColor color)
	{
		this.x = x;
		this.y = y;
		ChangeColor(color);
		this.isOccupied = false;
		conneter = FindObjectOfType<TileConneter>();
	}
    public void ChangeColor(TileColor tileColor)
    {
		color = tileColor;
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = colorMaterials[(int)color];
    }

	public void OnPointerClick(PointerEventData eventData)
	{
		
		
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		conneter.StartConneting(this);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if(!isOccupied&&conneter.IsConnetStart&&conneter.LastTile.color==color&&IsNearTile(conneter.LastTile))
		{
			conneter.AddTile(this);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//foreach(Vector3 pos in connectedTilePositions)
		//{
		//	Debug.Log(pos);
		//}
		//connectedTilePositions.Clear();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		conneter.EndConneting();
	}
	private bool IsNearTile(Tile tile)
	{
		bool result = false;
		if (this == tile)
			return false;
		int xdiff = Mathf.Abs(this.x - tile.x);
		int ydiff = Mathf.Abs(this.y - tile.y);
		if(xdiff<=1&&ydiff<=1)
		{
			result = true;
		}
		return result;
	}
}
