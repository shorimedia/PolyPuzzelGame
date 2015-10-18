using UnityEngine;
using System.Collections;

//
// Script Name: BombPeg
//Script by: Victor L Josey
// Description: 
// (c) 2015 Shoori Studios LLC  All rights reserved.

public class BombPeg : Item {


	public override void Use()
	{


		GameManger.ACTIVE = false;


	}


	public override void OnItemDissolve()
	{
		foreach(PegStateMachine hex in PegObject.pagState_pUpdater.neighborHEX)
		{
			if( hex.PegType.blockType!= PegTypeMach.BlockType.Empty)
			{
				hex.isJumpable  = true;
				hex.blockState = PegStateMachine.BlockState.Dissolve;
				hex.ChangeBlockState();
			}

		}
		
	}


}
