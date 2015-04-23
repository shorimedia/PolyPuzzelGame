using UnityEngine;
using System.Collections;

public class FivePoints : Item {

	public override void Use()
	{
		
		PegObject.bonusPoints = PegObject.hexType.points + 5;
		GameManger.ACTIVE = false;
	}
}
