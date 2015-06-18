using UnityEngine;
using System.Collections;

public class PegTypeMach : MonoBehaviour {


	public SpriteRenderer SpriteObject;

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

	public BlockType blockType = BlockType.Flow;

	public HexType hexType;


	void Awake(){
		
		
		hexType = new HexType(HexType.BlockType.Fire);
		
	}


	// Use this for initialization
	void Start () {
		ChangeBlockType();
	}


	public void ChangeBlockType(){
		
		switch(blockType){
		case BlockType.Flow : 
			SpriteObject.enabled = true;
			hexType.ChangeHexType(HexType.BlockType.Flow);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
		case BlockType.Stone :
			SpriteObject.enabled = true;
			hexType.ChangeHexType(HexType.BlockType.Stone);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
		case BlockType.Fire : 
			SpriteObject.enabled = true;
			hexType.ChangeHexType(HexType.BlockType.Fire);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
		case BlockType.Lite : 
			SpriteObject.enabled = true;
			hexType.ChangeHexType(HexType.BlockType.Lite);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
		case BlockType.Shield : 
			
			hexType.ChangeHexType(HexType.BlockType.Shield);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
		case BlockType.Sword : 
			SpriteObject.enabled = true;
			hexType.ChangeHexType(HexType.BlockType.Sword);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
		case BlockType.Spear : 
			SpriteObject.enabled = true;
			hexType.ChangeHexType(HexType.BlockType.Spear);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
		case BlockType.Wealth : 
			SpriteObject.enabled = true;
			hexType.ChangeHexType(HexType.BlockType.Wealth);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
		case BlockType.Wisdom : 
			SpriteObject.enabled = true;
			hexType.ChangeHexType(HexType.BlockType.Wisdom);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
		case BlockType.TimeType : 
			SpriteObject.enabled = true;
			hexType.ChangeHexType(HexType.BlockType.TimeType);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
			
		case BlockType.Destruction : 
			SpriteObject.enabled = true;
			hexType.ChangeHexType(HexType.BlockType.Destruction);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;

		case BlockType.Darkness : 
			SpriteObject.enabled = true;
			hexType.ChangeHexType(HexType.BlockType.Darkness);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
			
		case BlockType.Empty : 
			SpriteObject.enabled = false;

			hexType.ChangeHexType(HexType.BlockType.Empty);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
			
		default:
			hexType.ChangeHexType(HexType.BlockType.Fire);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
			
		}


	}


// What can defeat this peg

	public  bool CompareType( BlockType peg){

		bool canDefeat = true;
		
		switch(peg){
			
		case BlockType.Flow : 
			
			if( hexType.Flow == true){ canDefeat = true; } else {canDefeat = false;}
			
			break;
			
		case BlockType.Stone : 
			if( hexType.Stone == true){ canDefeat = true; } else {canDefeat =  false;}
			break;
			
		case BlockType.Fire : 
			if( hexType.Fire == true){ canDefeat = true; } else {canDefeat =  false;}
			break;
			
		case BlockType.Lite : 
			if( hexType.Lite == true){ canDefeat = true; } else {canDefeat =  false;}
			break;
			
		case BlockType.Shield : 
			
			if( hexType.Shield == true){ canDefeat = true; } else {canDefeat =  false;}
			break;
			
		case BlockType.Sword : 
			if( hexType.Sword == true){ canDefeat = true; } else {canDefeat =  false;}
			break;
			
		case BlockType.Spear : 
			if( hexType.Spear == true){ canDefeat = true; } else {canDefeat =  false;}
			break;
			
		case BlockType.Wealth : 
			if( hexType.Wealth == true){ canDefeat = true; } else {canDefeat =  false;}
			break;
			
		case BlockType.Wisdom : 
			if( hexType.Wisdom == true){ canDefeat = true; } else {canDefeat =  false;}
			
			break;
		case BlockType.TimeType : 
			
			if( hexType.TimeType == true){ canDefeat = true; } else {canDefeat =  false;}
			break;
			
		case BlockType.Destruction : 
			
			if( hexType.Destruction == true){ canDefeat = true; } else {canDefeat =  false;}
			break;
			
		case BlockType.Darkness : 
			if( hexType.Darkness == true){ canDefeat =  true; } else {canDefeat =  false;}
			
			break;
			
		case BlockType.Empty : 
			
			if( hexType.Flow == true){ canDefeat =  true; } else {canDefeat =  false;}
			break;
			
		default : if( hexType.Flow == true){ canDefeat =  true; } else {canDefeat =  false;}
			break;
		}
		
		return canDefeat;
	}
}
