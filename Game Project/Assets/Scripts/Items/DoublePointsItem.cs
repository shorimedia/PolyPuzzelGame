using UnityEngine;
using System.Collections;

public class DoublePointsItem : Item {



	public override void Use()
	{

		PegObject.bonusPoints = PegObject.hexType.points * 2;
		GameManger.ACTIVE = false;
	}



}
