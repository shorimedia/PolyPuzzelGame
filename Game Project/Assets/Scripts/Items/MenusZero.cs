using UnityEngine;
using System.Collections;

public class MenusZero : Item {

	
	public override void Use()
	{
		
		PegObject.bonusPoints = -1;
		GameManger.ACTIVE = false;
		PegObject.isJumpable  = true;
		PegObject.blockState = HexBlock.BlockState.Dissolve;
		PegObject.ChangeBlockState();
	}
}
