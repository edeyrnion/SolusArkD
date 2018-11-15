using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class CustomHierarchy : MonoBehaviour
{
	private static Vector2 offset = new Vector2(0, 2);

	static CustomHierarchy()
	{
		EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
	}

	private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
	{
		Color backgroundColor = new Color(.76f, .76f, .76f);
		FontStyle style = FontStyle.Normal;
		var roots = SceneManager.GetActiveScene().GetRootGameObjects();

		var obj = EditorUtility.InstanceIDToObject(instanceID) as Object;
		if (obj != null)
		{
			if (roots.Contains((GameObject)obj)) { style = FontStyle.Bold; } // <- backgroundColor = Color.gray;
			if (Selection.instanceIDs.Contains(instanceID)) { backgroundColor = new Color(0.24f, 0.48f, 0.90f); }
			Rect offsetRect = new Rect(selectionRect.position + offset, selectionRect.size);
			EditorGUI.DrawRect(selectionRect, backgroundColor);
			EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle() { fontStyle = style });
		}
	}
}
