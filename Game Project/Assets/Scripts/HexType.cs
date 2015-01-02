using UnityEngine;
using System.Collections;

[System.Serializable]
public class HexType  {

	public enum BlockType {
		Flow,
		Stone,
		Fire,
		Lite,
		Shield,
		Sword,
		Spear,
		Wealth,
		Wisdom,
		TimeType,
		Destruction,
		Darkness,
		Empty
	}

	public BlockType hexType = BlockType.Flow;

	public Material blockMaterial;
	public int points;

	public HexType(BlockType type){

		hexType = type;
		SetHexType();
	}


	public void ChangeHexType(BlockType type){
		hexType = type;
		SetHexType();
	}


	public void SetHexType(){

		switch(hexType){
		case BlockType.Flow : 
			blockMaterial   = Resources.Load<Material>("Materials/Flow_blockMaterial" );
			points = 30;
			break;
		case BlockType.Stone : 
			blockMaterial   = Resources.Load<Material>("Materials/Stone_blockMaterial" );
			points = 30;
			break;
		case BlockType.Fire : 

			blockMaterial   = Resources.Load<Material>("Materials/Fire_blockMaterial" );
			points = 30;
			break;
		case BlockType.Lite : 

			blockMaterial   = Resources.Load<Material>("Materials/Lite_blockMaterial" );
			points = 50;
			break;
		case BlockType.Shield : 


			blockMaterial   = Resources.Load<Material>("Materials/Shield_blockMaterial" );
			points = 70;
			break;
		case BlockType.Sword : 

			blockMaterial   = Resources.Load<Material>("Materials/Sword_blockMaterial" );
			points = 40;
			break;
		case BlockType.Spear : 
			blockMaterial   = Resources.Load<Material>("Materials/Spear_blockMaterial" );
			points = 40;
			break;
		case BlockType.Wealth : 
			blockMaterial   = Resources.Load<Material>("Materials/Wealth_blockMaterial" );
			points = 200;
			break;
		case BlockType.Wisdom : 

			blockMaterial   = Resources.Load<Material>("Materials/Wisdom_blockMaterial" );
			points = 50;
			break;
		case BlockType.TimeType : 
			blockMaterial   = Resources.Load<Material>("Materials/TimeType_blockMaterial" );
			points = 20;
			break;

		case BlockType.Destruction : 

			blockMaterial   = Resources.Load<Material>("Materials/Destruction_blockMaterial" );
			points = 5;
			break;
		case BlockType.Darkness : 

			blockMaterial   = Resources.Load<Material>("Materials/Darkness_blockMaterial" );
			points = 100;
			break;

		case BlockType.Empty : 
			
			blockMaterial   = Resources.Load<Material>("Materials/Empty_blockMaterial" );
			points = 0;
			break;

		default:
			Debug.LogWarning("Hew Type Materals not found!" );
			blockMaterial   = Resources.Load<Material>("Materials/Flow_blockMaterial" );
			break;

		}


	}

	

}
