using UnityEngine;

namespace David
{
	public class QualitySettings : MonoBehaviour
	{
		public void SetQuality(int qualityIndex)
		{
			UnityEngine.QualitySettings.SetQualityLevel(qualityIndex);
		}
	}
}
