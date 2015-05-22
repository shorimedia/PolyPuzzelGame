using UnityEngine;
using System.Collections;

public class ExtraPoints : Item {

	public int pointsValue = 0;

	public enum OperationType
	{
		Add,
		multiply
	}

	public OperationType operation = OperationType.Add;

	public override void Use()
	{


		switch(operation)
		{
		case OperationType.Add: PegObject.bonusPoints = PegObject.PegType.hexType.points + pointsValue; break;
		case OperationType.multiply: PegObject.bonusPoints = PegObject.PegType.hexType.points * pointsValue; break;
		}
		GameManger.ACTIVE = false;
	}
}
