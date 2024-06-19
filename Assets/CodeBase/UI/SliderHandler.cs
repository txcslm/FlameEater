using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class SliderHandler : MonoBehaviour
	{
		[SerializeField] private Slider _slider;
		
		public float Value
		{
			set => _slider.value = value;
		}
		
		public float MaxValue
		{
			set => _slider.maxValue = value;
		}
	}
}