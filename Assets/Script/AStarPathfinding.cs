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

			// ���� ���� ��ǥ ��� ����
			startNode.gCost = 0; //������������ ����������� �Ÿ�
			startNode.hCost = CalculateHCost(startNode, goalNode); //���Ƿ� ����� ��������� ������������ �Ÿ�
			startNode.CalculateFCost(); //g+h ��ü�Ÿ�(���ǰ��)

			// Open ����Ʈ�� Closed ����Ʈ
			List<Node> openList = new List<Node>();
			HashSet<Node> closedSet = new HashSet<Node>();

			openList.Add(startNode);

			while (openList.Count > 0)
			{
				Node currentNode = openList[0];

				// Open ����Ʈ���� ���� ����� ���� ���� ��� ����
				for (int i = 1; i < openList.Count; i++)
				{
					if (openList[i].fCost < currentNode.fCost || openList[i].fCost == currentNode.fCost && openList[i].hCost < currentNode.hCost)
					{
						currentNode = openList[i];
					}
				}

				openList.Remove(currentNode);
				closedSet.Add(currentNode);

				// ��ǥ ��ġ ���� �� ��� �ϼ�
				if (currentNode.position == goal)
				{
					path = RetracePath(startNode, currentNode);
					break;
				}

				// �̿��� ĭ�� Ȯ��
				List<Node> neighbors = GetNeighbors(currentNode.position);//��������
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
					// ���� ������ path[currentNode.gCost+1]��� ��ŵ 
					// ��ֹ�(����)�� ��� ����
					if (neighbor.isObstacle)//IsObstacle()
					{
						continue;
					}

					// Closed ����Ʈ�� �ִ� ��� ����
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

		// ������ �����¿� ĭ �߰�
		int[] dx = { 0, 0, -1, 1 };
		int[] dy = { -1, 1, 0, 0 };

		for (int i = 0; i < 4; i++)
		{
			int newX = position.x + dx[i];
			int newY = position.y + dy[i];

			// �׸��� ������ ����� ��� ����
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
		// �ش� ��ġ�� ���Ͱ� �ִ��� Ȯ���Ͽ� ��ֹ��� �Ǵ�
		// �ʿ信 ���� ���� ��ġ�� �����ϴ� ������ ������ ����� ����ϸ� �˴ϴ�.
		// �ӽ÷� true�� ��ȯ�ϵ��� �����մϴ�.
		return true;
	}

	private int CalculateHCost(Node nodeA, Node nodeB)
	{
		// �� ��� ������ ����ź �Ÿ� ���
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
		public int gCost; // ���� ��ġ���� ���� ��ġ������ ���
		public int hCost; // ���� ��ġ���� ��ǥ ��ġ������ ���� ���
		public int fCost; // gCost�� hCost�� ��
		public Node parent; // ��� ������ ���� �θ� ���
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

