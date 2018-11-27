using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethodes
{
	public static Vector3 ToVector3(this Vector2 v2)
	{
		return new Vector3(v2.x, 0, v2.y);
	}

	public static List<Resolution> ToList(this Resolution[] array)
	{
		return new List<Resolution>(array);
	}
}
