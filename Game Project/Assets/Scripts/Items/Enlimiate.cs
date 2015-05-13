using UnityEngine;
using System.Collections;

public class Enlimiate : Item {

	public HexBlock[] hexNeighbor = new HexBlock[42];
	public int hexIndex = 0;

	

	public override void Use()
	{
//		hexIndex = 0;
//		
//		GameManger.ACTIVE = false;
//
//		 HexBlock[]  hex =  PegObject.neighborHEX;
//
//		for(int P= 0; P < hex.Length; P++)
//		{
//
//			hexNeighbor[hexIndex] = hex[P];
//
//			hexIndex++;
//
//
//			for(int N = 0; N < hex[P].neighborHEX.Length; N++)
//			{
//				hexNeighbor[hexIndex] = hex[P].neighborHEX[N];
//				hexIndex++;
//
//			}
//
//
//		}
//
//
//
//
//		Debug.Log("Neighbor " + hexNeighbor.Length  );
//
//
////		PegObject.isJumpable  = true;
////		PegObject.blockState = HexBlock.BlockState.Dissolve;
////		PegObject.ChangeBlockState();
//

	}
}
