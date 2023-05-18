using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding
{
	private int gridSizeX;
	private int gridSizeY;
	private Node[,] grid;

	public AStarPathfinding(int sizeX, int sizeY)
	{
		gridSizeX = sizeX;
		gridSizeY = sizeY;
		grid = new Node[gridSizeX, gridSizeY];
		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				grid[x, y] = new Node(new Vector2Int(x, y));
			}
		}
	}

	public List<List<Vector2Int>> FindPath(Vector2Int[] start, Vector2Int goal)
	{
		List<List<Vector2Int>> pathList = new List<List<Vector2Int>>();
		for (int j=0;j<start.Length;j++)
		{
			
			List<Vector2Int> path = new List<Vector2Int>();

			Node startNode = new Node(start[j]);
			Node goalNode = new Node(goal);

			// 시작 노드와 목표 노드 설정
			startNode.gCost = 0; //시작지점에서 현재노드까지의 거리
			startNode.hCost = CalculateHCost(startNode, goalNode); //임의로 계산한 현재노드부터 도착노드까지의 거리
			startNode.CalculateFCost(); //g+h 전체거리(임의계산)

			// Open 리스트와 Closed 리스트
			List<Node> openList = new List<Node>();
			HashSet<Node> closedSet = new HashSet<Node>();

			openList.Add(startNode);

			while (openList.Count > 0)
			{
				Node currentNode = openList[0];

				// Open 리스트에서 예상 비용이 가장 낮은 노드 선택
				for (int i = 1; i < openList.Count; i++)
				{
					if (openList[i].fCost < currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
					{
						currentNode = openList[i];
					}
				}

				openList.Remove(currentNode);
				closedSet.Add(currentNode);

				// 목표 위치 도달 시 경로 완성
				if (currentNode.position == goal)
				{
					path = RetracePath(startNode, currentNode);
					break;
				}

				// 이웃한 칸들 확인
				List<Node> neighbors = GetNeighbors(currentNode.position);//동서남북
				foreach (Node neighbor in neighbors)
				{
					bool isbreak = false;
					for(int k=0;k<pathList.Count;k++)
					{
						if (currentNode.gCost< pathList[k].Count&&currentNode.position == pathList[k][currentNode.gCost])
						{
							isbreak = true;
							break;
						}
					}
					if(isbreak)
					{
						continue;
					}
					// 이전 몬스터의 path[currentNode.gCost+1]라면 스킵 
					// 장애물(몬스터)인 경우 무시
					if (neighbor.isObstacle)//IsObstacle()
					{
						continue;
					}

					// Closed 리스트에 있는 경우 무시
					if (closedSet.Contains(neighbor))
					{
						continue;
					}

					int newCostToNeighbor = currentNode.gCost + CalculateHCost(currentNode, neighbor);
					if (newCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor))
					{
						neighbor.gCost = newCostToNeighbor;
						neighbor.hCost = CalculateHCost(neighbor, goalNode);
						neighbor.parent = currentNode;
						neighbor.CalculateFCost();

						if (!openList.Contains(neighbor))
						{
							openList.Add(neighbor);
						}
					}
				}
			}

			pathList.Add(path);
		}
		return pathList;
	}

	private List<Node> GetNeighbors(Vector2Int position)
	{
		List<Node> neighbors = new List<Node>();

		// 인접한 상하좌우 칸 추가
		int[] dx = { 0, 0, -1, 1 };
		int[] dy = { -1, 1, 0, 0 };

		for (int i = 0; i < 4; i++)
		{
			int newX = position.x + dx[i];
			int newY = position.y + dy[i];

			// 그리드 범위를 벗어나는 경우 무시
			if (newX < 0 || newX >= gridSizeX || newY < 0 || newY >= gridSizeY)
			{
				continue;
			}

			Node neighbor = grid[newX, newY];
			neighbors.Add(neighbor);
		}

		return neighbors;
	}

	private bool IsObstacle(Vector2Int position)
	{
		// 해당 위치에 몬스터가 있는지 확인하여 장애물로 판단
		// 필요에 따라 몬스터 위치를 저장하는 데이터 구조를 만들고 사용하면 됩니다.
		// 임시로 true를 반환하도록 설정합니다.
		return true;
	}

	private int CalculateHCost(Node nodeA, Node nodeB)
	{
		// 두 노드 사이의 맨하탄 거리 계산
		return Mathf.Abs(nodeA.position.x - nodeB.position.x) + Mathf.Abs(nodeA.position.y - nodeB.position.y);
	}

	private List<Vector2Int> RetracePath(Node startNode, Node endNode)
	{
		List<Vector2Int> path = new List<Vector2Int>();
		Node currentNode = endNode;

		while (currentNode.parent!=null)
		{
			path.Add(currentNode.position);
			currentNode = currentNode.parent;
		}
		path.Add(startNode.position);
		path.Reverse();
		return path;
	}
	public void SetObstacle(int x,int y)
	{
		grid[x,y].isObstacle = true;
	}

	private class Node
	{
		public Vector2Int position;
		public int gCost; // 시작 위치부터 현재 위치까지의 비용
		public int hCost; // 현재 위치부터 목표 위치까지의 예상 비용
		public int fCost; // gCost와 hCost의 합
		public Node parent; // 경로 추적을 위한 부모 노드
		public bool isObstacle;	

		public Node(Vector2Int pos)
		{
			position = pos;
			gCost = int.MaxValue;
			hCost = int.MaxValue;
			fCost = int.MaxValue;
			parent = null;
			isObstacle=false;
		}

		public void CalculateFCost()
		{
			fCost = gCost + hCost;
		}
		
	}
}

