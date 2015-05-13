using UnityEngine;
using System.Collections;

public class BlackPeg : Item {

	public override void Use()
	{
		
		PegObject.bonusPoints = 100;
		GameManger.ACTIVE = false;
		PegObject.PegType.blockType = PegTypeMach.BlockType.Darkness;
		PegObject.PegType.ChangeBlockType();
		PegObject.pUpdater.UpdateNeighbor();
	}
}
