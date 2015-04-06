using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderBtnControl : MonoBehaviour {

	public float[] SliderPosition;
	public Scrollbar  slider;

	private int _currentIndex = 1;

	public void ChangeSlider(){

		if(_currentIndex < SliderPosition.Length){
			_currentIndex++;
			slider.value = SliderPosition[_currentIndex - 1];
		}else
		{
			_currentIndex = 1;
			slider.value = SliderPosition[_currentIndex -1 ];
		}
	}

}
