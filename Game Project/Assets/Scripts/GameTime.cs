using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
	public static int TIME_IN_SECONDS;

	public int  minutes;
	public int 	seconds;

	private int CountSeconds;
	private int CountMinutes;

	private int restSeconds;

	public string textTime;
	

	// Update is called once per frame
	void FixedUpdate () {

		var guitime = Time.time;

		restSeconds= CountSeconds + (int)guitime;

		TIME_IN_SECONDS = restSeconds;
		seconds = restSeconds % 60;
		minutes = restSeconds / 60;

		textTime = string.Format("{0:00}:{1:00}", minutes, seconds);
	}


}
