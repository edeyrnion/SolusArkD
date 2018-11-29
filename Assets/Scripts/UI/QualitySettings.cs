using UnityEngine;
using TMPro;

namespace David
{
	public class QualitySettings : MonoBehaviour
	{
		[SerializeField] TMP_Dropdown dropdown;

		public int qualityIndex;


		private void Start()
		{
			qualityIndex = dropdown.value;
		}

		public void SetQuality(int index)
		{
			qualityIndex = index;			
		}
	}
}
