using UnityEngine;
using System.Collections;

public class IncreaseTime : Item {

	public float addTime = 2;

	public override void Use()
	{

		GameManger.ACTIVE = false;

	}


	public override void OnItemDissolve()
	{
		Messenger.Broadcast<float>("Set MaxTime", addTime);
		
	}
	
}
