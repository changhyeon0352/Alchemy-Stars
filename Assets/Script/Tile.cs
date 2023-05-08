using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public enum EelementAttributes {
    water=0,
    wind,
    fire,
    lightning 
}
public enum TileState { 
	empty=0,
	player,
	enemy
}
public class Tile : MonoBehaviour,IPointerClickHandler,IPointerDownHandler,IPointerEnterHandler,IPointerUpHandler
{
	private int x;
	private int y;
	EelementAttributes color=EelementAttributes.fire;
	private TileState tileState;
    [SerializeField]
    private Material[] colorMaterials;
	TileConnecter conneter;
	bool isConneting=false;
	private Unit unit;

	public Unit Unit { get { return unit; } }
	public TileState TileState { get { return tileState; } }
	public Vector2Int Pos { get { return new Vector2Int(x,y); } }
	public EelementAttributes Color { get { return color; } }
	public void Initialize(int x, int y, EelementAttributes color)
	{
		this.x = x;
		this.y = y;
		ChangeColor(color);
		this.tileState = TileState.empty;
		conneter = FindObjectOfType<TileConnecter>();
	}
    public void ChangeColor(EelementAttributes tileColor)
    {
		color = tileColor;
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = colorMaterials[(int)color];
    }

	public void OnPointerClick(PointerEventData eventData)
	{
		//몬스터가 있다면 그 정보를 넘김
		
	}

	public void OnPointerDown(PointerEventData eventData)
	{
			if (tileState == TileState.empty && IsAdjacentTile(conneter.LastTile))
			{
			conneter.StartConnecting(this);
			}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if(conneter.IsConnetStart)
		{
			if (conneter.IsContainTile(this))
			{
				conneter.RemoveFromTileToLast(this);
			}
			else if (tileState== TileState.empty && conneter.LastTile.color == color && IsAdjacentTile(conneter.LastTile))
			{
				conneter.AddTile(this);
			}
		}
		
	}


	public void OnPointerUp(PointerEventData eventData)
	{
		conneter.EndConneting();
	}
	private bool IsAdjacentTile(Tile tile)
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
	public void SetUnit(Unit unit,TileState tileState)
	{
		this.unit = unit;
		this.tileState = tileState;
		unit.transform.position = this.transform.position;
		
	}
}
