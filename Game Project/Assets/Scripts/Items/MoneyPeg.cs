using UnityEngine;
using System.Collections;

public class MoneyPeg : Item {

	public enum Metals
	{
		Bronze,
		Gold,
		Silver
	}
	
	
	public Metals metalType = Metals.Bronze; 
	
	public override void Use()
	{
		
		GameManger.ACTIVE = false;

	}
	
	
	public override void OnItemDissolve()
	{

		switch(metalType) 
		{
		case Metals.Bronze: 
			PegObject.bonusPoints = 75;
			break;
		case Metals.Silver: 
			PegObject.bonusPoints = 100;
			break;
		case Metals.Gold:
			PegObject.bonusPoints = 250;
			break;
			
		}

	}

}
