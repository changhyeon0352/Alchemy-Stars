using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillData))]
public class SkillDataEditor : Editor
{
	SerializedProperty skillAreaListProp;
	private bool[,] tileChecked;
	private bool isEditing;

	int size = 9;
	private void OnEnable()
	{
		skillAreaListProp = serializedObject.FindProperty("skillAreaList");
		
		tileChecked = new bool[size, size];
		tileChecked[4, 4] = true;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		serializedObject.Update();

		isEditing = EditorGUILayout.Toggle("Edit Mode", isEditing);

		if (isEditing)
		{
			DrawSkillAreaGrid();
			
			
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("FlipX"))
			{
				FlipX();
			}
			if (GUILayout.Button("FlipY"))
			{
				FlipY();
			}
			EditorGUILayout.EndHorizontal();
			if (GUILayout.Button("Complete"))
			{
				CompleteSkillArea();
			}
		}

		serializedObject.ApplyModifiedProperties();
	}
	private void FlipX()
	{
		for (int y = 0; y < size; y++)
		{
			for (int x = 0; x < size; x++)
			{
				if (tileChecked[x, y])
				{
					int flipx = Mathf.Abs(x - (size - 1));
					tileChecked[flipx, y] = true;
				}
			}
		}
	}
	private void FlipY()
	{
		for (int y = 0; y < size; y++)
		{
			for (int x = 0; x < size; x++)
			{
				if (tileChecked[x, y])
				{
					int flipy = Mathf.Abs(y - (size - 1));
					tileChecked[x, flipy] = true;
				}
			}
		}
	}

	private void DrawSkillAreaGrid()
	{
		

		// Draw the toggle grid
		
		for (int y = 0; y < size; y++)
		{
			EditorGUILayout.BeginHorizontal();
			for (int x = 0; x < size; x++)
			{
				tileChecked[x, y] = EditorGUILayout.Toggle(tileChecked[x, y], GUILayout.Width(20));
			}
			EditorGUILayout.EndHorizontal();
		}
	}

	private void CompleteSkillArea()
	{
		int size = tileChecked.GetLength(0);
		List<Vector2Int> checkedPositions = new List<Vector2Int>();

		// Collect the checked positions
		for (int y = 0; y < size; y++)
		{
			for (int x = 0; x < size; x++)
			{
				if (tileChecked[x, y])
				{
					checkedPositions.Add(new Vector2Int(x-4, y-4));
				}
			}
		}

		// Add the new array to the skillAreaListProp
		skillAreaListProp.arraySize++;
		SerializedProperty arrayElement = skillAreaListProp.GetArrayElementAtIndex(skillAreaListProp.arraySize - 1);
		arrayElement.FindPropertyRelative("array").arraySize = checkedPositions.Count;
		for (int i = 0; i < checkedPositions.Count; i++)
		{
			SerializedProperty element = arrayElement.FindPropertyRelative("array").GetArrayElementAtIndex(i);
			element.vector2IntValue = checkedPositions[i];
		}

		// Reset tileChecked
		for (int y = 0; y < size; y++)
		{
			for (int x = 0; x < size; x++)
			{
				tileChecked[x, y] = false;
			}
		}
		tileChecked[4, 4] = true;
	}

}
