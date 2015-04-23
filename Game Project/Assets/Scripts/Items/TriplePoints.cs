using UnityEngine;
using System.Collections;

public class TriplePoints : Item {

	public override void Use()
	{
		
		PegObject.bonusPoints = PegObject.hexType.points * 3;
		GameManger.ACTIVE = false;
	}
}
