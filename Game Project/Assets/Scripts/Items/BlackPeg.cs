using UnityEngine;
using System.Collections;

public class BlackPeg : Item {

	public override void Use()
	{
		
		PegObject.bonusPoints = 100;
		GameManger.ACTIVE = false;
		PegObject.blockType = HexBlock.BlockType.Darkness;
		PegObject.ChangeBlockType();
		PegObject.UpdateNeighbor();
	}
}
