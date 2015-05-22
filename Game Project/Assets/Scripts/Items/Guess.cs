using UnityEngine;
using System.Collections;

public class Guess : Item {

	public int randomPoints = Random.Range(0,25);

	public float addTime = Random.Range(0.1f, 4f);


	public enum ItemSelection
	{
		AddPoint,
		MenusPoints,
		MenusPegZero,
		MenusPeg,
		PlusTime

	}

	public ItemSelection itemSelection = ItemSelection.AddPoint;

	public override void Use()
	{

		RandomSelection();


		switch(itemSelection)
		{

		case ItemSelection.AddPoint : 		
			PegObject.bonusPoints = PegObject.PegType.hexType.points + randomPoints; 
			break;

		case ItemSelection.MenusPeg : 	

			PegObject.bonusPoints = -1;
			GameManger.TOTAL_POINTS_COUNT -=  randomPoints;
			GameManger.ACTIVE = false;
			PegObject.isJumpable  = true;
			PegObject.blockState = PegStateMachine.BlockState.Dissolve;
			PegObject.ChangeBlockState();
			break;

		case ItemSelection.MenusPegZero : 	

			PegObject.bonusPoints = -1;
			GameManger.ACTIVE = false;
			PegObject.isJumpable  = true;
			PegObject.blockState = PegStateMachine.BlockState.Dissolve;
			PegObject.ChangeBlockState();
			break;

		case ItemSelection.MenusPoints :	

			PegObject.bonusPoints = -1;
			GameManger.TOTAL_POINTS_COUNT -=  randomPoints;

			break;
		case ItemSelection.PlusTime : 		

			GameManger.ACTIVE = false;
			break;
		}


	}


	private void RandomSelection()
	{
		float ranNum = Random.Range (0f,1f);

		if(ranNum <= 0.45f)
		{
			itemSelection = ItemSelection.AddPoint;
		
		}else if(ranNum > 0.45f && ranNum <= 0.61f)
		{
			itemSelection = ItemSelection.MenusPoints;

		}else if(ranNum > 0.61f && ranNum <= 0.77f)
		{
			itemSelection = ItemSelection.MenusPegZero;

		}else if(ranNum > 0.77f && ranNum <= 0.93f)
		{
			itemSelection = ItemSelection.MenusPeg;

		}else if(ranNum > 0.93f && ranNum <= 1f)
		{
			itemSelection = ItemSelection.PlusTime;
		}


	}


	public override void OnItemDissolve()
	{
		if(itemSelection == ItemSelection.PlusTime)
		{ 
		Messenger.Broadcast<float>("Set MaxTime", addTime);
		}
	}







}
