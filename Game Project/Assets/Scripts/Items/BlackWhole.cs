using UnityEngine;
using System.Collections;

public class BlackWhole : Item {

	public override void Use()
	{
		
		GameManger.ACTIVE = false;
		PegObject.isJumpable  = true;
		PegObject.blockState = PegStateMachine.BlockState.Dissolve;
		PegObject.ChangeBlockState();
	}
}
