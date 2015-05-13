using UnityEngine;
using System.Collections;

public class NegetivePeg : Item {

	public override void Use()
	{
		
		PegObject.bonusPoints = -1;
		GameManger.TOTAL_POINTS_COUNT -=  60;
		GameManger.ACTIVE = false;
		PegObject.isJumpable  = true;
		PegObject.blockState = PegStateMachine.BlockState.Dissolve;
		PegObject.ChangeBlockState();
	}
}
