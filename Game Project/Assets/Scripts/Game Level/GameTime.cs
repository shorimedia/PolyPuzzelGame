using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTime : MonoBehaviour {
	public static int TIME_IN_SECONDS;

	public int  minutes;
	public int 	seconds;

	private int CountSeconds = 0;
	private int CountMinutes;

	private int restSeconds;

	public static string TEXT_TIME;

	public Text text;
	public Text pointText;

	public bool OnTimer = true;



	public bool ToggleTimer
	{
		get{return OnTimer; }
		set{ OnTimer = value;}
	}
	

	// Update is called once per frame
	public void Update () {

		if(OnTimer)
		{
			Time.timeScale = 1.0f ;

			var guitime = Time.timeSinceLevelLoad;

		restSeconds = CountSeconds + (int)guitime;

		TIME_IN_SECONDS = restSeconds;
		seconds = restSeconds % 60;
		minutes = restSeconds / 60;

		TEXT_TIME = string.Format("{0:00}:{1:00}", minutes, seconds);

		text.text = TEXT_TIME;

		pointText.text = GameManger.TOTAL_POINTS_COUNT.ToString();
		}else
		{
			Time.timeScale = 0f;
		}
	}




}
