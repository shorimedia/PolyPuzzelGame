using UnityEngine;
using System.Collections;

public class PlusZero : Item {

	public override void Use()
	{
		PegObject.bonusPoints = -1;
		GameManger.ACTIVE = false;


		int ranNum = Random.Range(1 , 4);

		switch(ranNum)
		{
		case 1: 
			PegObject.blockType = HexBlock.BlockType.Flow;
			break;
		case 2: 
			PegObject.blockType = HexBlock.BlockType.Fire;
			break;
		case 3: 
			PegObject.blockType = HexBlock.BlockType.Stone;
			break;
		}

		PegObject.blockState = HexBlock.BlockState.Normal;
		PegObject.ChangeBlockState();

		PegObject.UpdateNeighbor();

	}
}
