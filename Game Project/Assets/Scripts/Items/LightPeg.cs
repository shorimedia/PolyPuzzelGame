using UnityEngine;
using System.Collections;

public class LightPeg : Item {

	public override void Use()
	{
		
		PegObject.bonusPoints = 100;
		GameManger.ACTIVE = false;
		PegObject.PegType.blockType = PegTypeMach.BlockType.Lite;
		PegObject.PegType.ChangeBlockType();
//		PegObject.pUpdater.UpdateNeighbor();
	}

}
