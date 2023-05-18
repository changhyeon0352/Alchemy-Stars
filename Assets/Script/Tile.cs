using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public enum ElementAttribute {
    water=0,
    wind,
    fire,
    lightning 
}
public enum TileState { 
	empty=0,
	player,
	monster
}
public class Tile : MonoBehaviour,IPointerClickHandler,IPointerDownHandler,IPointerEnterHandler,IPointerUpHandler
{
	private int x;
	private int y;
	ElementAttribute elementAttribute=ElementAttribute.fire;
	private TileState tileState;
    [SerializeField]
    private Material[] colorMaterials;
	TileConnecter conneter;
	private Unit unit;

	public Unit Unit { get { return unit; } }
	public TileState TileState { get { return tileState; } }
	public Vector2Int Pos { get { return new Vector2Int(x,y); } }
	public ElementAttribute ElementAttribute { get { return elementAttribute; } }
	public void Initialize(int x, int y, ElementAttribute color)
	{
		this.x = x;
		this.y = y;
		ChangeColor(color);
		tileState = TileState.empty;
		conneter = FindObjectOfType<TileConnecter>();
	}
    public void ChangeColor(ElementAttribute tileColor)
    {
		elementAttribute = tileColor;
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = colorMaterials[(int)elementAttribute];
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
			else if (tileState== TileState.empty && conneter.ElementAttribute == elementAttribute && IsAdjacentTile(conneter.LastTile))
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
		if(tile == null)
		{
			return false;
		}
		bool result = false;
		if (this == tile)
			return false;
		int xdiff = Mathf.Abs(x - tile.x);
		int ydiff = Mathf.Abs(y - tile.y);
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
		if (unit != null)
		{
			unit.transform.position = this.transform.position;
			unit.SetPosition(Pos);
		}
			

	}
}
