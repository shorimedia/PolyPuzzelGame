using UnityEngine;
using System.Collections;

public class LightPeg : Item {

	public override void Use()
	{
		
		PegObject.bonusPoints = 100;
		GameManger.ACTIVE = false;
		PegObject.blockType = HexBlock.BlockType.Lite;
		PegObject.ChangeBlockType();
		PegObject.UpdateNeighbor();
	}

}
