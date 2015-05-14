using UnityEngine;
using System.Collections;

public class PlusPeg : Item {

	public override void Use()
	{
		PegObject.bonusPoints = 0;
		GameManger.ACTIVE = false;
		
		
		int ranNum = Random.Range(1 , 4);
		
		switch(ranNum)
		{
		case 1: 
			PegObject.PegType.blockType = PegTypeMach.BlockType.Flow;
			break;
		case 2: 
			PegObject.PegType.blockType = PegTypeMach.BlockType.Fire;
			break;
		case 3: 
			PegObject.PegType.blockType = PegTypeMach.BlockType.Stone;
			break;
		}
		
		PegObject.blockState = PegStateMachine.BlockState.Normal;
		PegObject.ChangeBlockState();
		
//		PegObject.pUpdater.UpdateNeighbor();
		
	}
}
