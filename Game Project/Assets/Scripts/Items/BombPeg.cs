using UnityEngine;
using System.Collections;



public class BombPeg : Item {


	public override void Use()
	{


		GameManger.ACTIVE = false;


	}


	public override void OnItemDissolve()
	{
		foreach(PegStateMachine hex in PegObject.pUpdater.neighborHEX)
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
