using UnityEngine;
using System.IO;
using TMPro;

public class HandleTextFile : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI text;

	void Start()
	{
		string path = "Assets/Resources/README.txt";

		StreamReader reader = new StreamReader(path);
		text.fontSize = 30;		
		text.text = reader.ReadToEnd();
		reader.Close();
	}
}
