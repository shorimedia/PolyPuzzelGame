using UnityEngine;


public class BlackPeg : Item {

	public override void Use()
	{
		
		PegObject.bonusPoints = 100;
		GameManger.ACTIVE = false;
		PegObject.PegType.blockType = PegTypeMach.BlockType.Darkness;
		PegObject.PegType.ChangeBlockType();
        PegObject.pagState_pUpdater.CheckNeighbor();
        //Update empty peg count. Call emptycheck method in the EndGameCheck.cs
        Messenger.Broadcast("Check Empties");
	}
}
