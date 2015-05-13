using UnityEngine;
using System.Collections;

public class EventCycle : MonoBehaviour {

	public float cycleTime;


	public  bool OnStart = false;

	public ItemSpawner Spawner;


	public float timerTime;

	private int currentPoints;

	private int endCyclePoints;


	// Use this for initialization
	void Start () {
	
		timerTime = 0;
		endCyclePoints = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		timerTime += 1 * Time.deltaTime;

		if(timerTime >= cycleTime )
		{
			OnStart = true;

			if(OnStart){
				//set current point from Game manager
				currentPoints = GameManger.TOTAL_POINTS_COUNT;

				float ranNUm = Random.Range(0,1);
			//Check if a Item should spawn
				if(PointsIncrease() <= 60)
				{

				}else
					if(PointsIncrease() > 60 && PointsIncrease() <= 90)
				{
					// only have a 10%  chance of a new item spawning if pointa are 60 to 90 increase
					if(ranNUm >= 0.5f && ranNUm <= 0.7f)
					{
						//spawn item
						Spawner.spawn = true;
					}
				}else
					if(PointsIncrease() > 90 && PointsIncrease() <= 150)
				{
					// only have a 50%  chance of a new item spawning if pointa are 90 to 150 increase
					if(ranNUm >= 0f && ranNUm <= 0.5f)
					{
						//spawn item
						Spawner.spawn = true;
					}
				}else
					if(PointsIncrease() > 150 && PointsIncrease() <= 300)
				{
					// only have a 70%  chance of a new item spawning if pointa are 150 to 300 increase
					if(ranNUm >= 0f && ranNUm <= 0.7f)
					{
						//spawn item
						Spawner.spawn = true;
					}
				}else
					if(PointsIncrease() > 300)
				{

					if(ranNUm >= 0f && ranNUm <= 0.7f)
					{
						//spawn item
						Spawner.spawn = true;
					}

					if(ranNUm >= 0.7f && ranNUm <= 1f)
					{
						//spawn item
						Spawner.spawn = true;

						//spawn item
						Spawner.spawn = true;
					}


				}
	
				Debug.Log("Point difference " + PointsIncrease());

			// Set end game points for next cycle
			endCyclePoints = currentPoints;
			// reset cycle
			OnStart = false;
			}

			timerTime = 0;
		}


	}


	int PointsIncrease()
	{
		return currentPoints - endCyclePoints; // return the difference if the points to rate progression
	}

		
}
