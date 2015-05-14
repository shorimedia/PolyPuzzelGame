using UnityEngine;
using System.Collections;

public class PegTypeMach : MonoBehaviour {



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
			hexType.ChangeHexType(HexType.BlockType.Flow);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
		case BlockType.Stone : 
			hexType.ChangeHexType(HexType.BlockType.Stone);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
		case BlockType.Fire : 
			hexType.ChangeHexType(HexType.BlockType.Fire);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
		case BlockType.Lite : 
			
			hexType.ChangeHexType(HexType.BlockType.Lite);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
		case BlockType.Shield : 
			
			hexType.ChangeHexType(HexType.BlockType.Shield);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
		case BlockType.Sword : 
			
			hexType.ChangeHexType(HexType.BlockType.Sword);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
		case BlockType.Spear : 
			hexType.ChangeHexType(HexType.BlockType.Spear);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
		case BlockType.Wealth : 
			hexType.ChangeHexType(HexType.BlockType.Wealth);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
		case BlockType.Wisdom : 
			
			hexType.ChangeHexType(HexType.BlockType.Wisdom);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
		case BlockType.TimeType : 
			hexType.ChangeHexType(HexType.BlockType.TimeType);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
			
		case BlockType.Destruction : 
			
			hexType.ChangeHexType(HexType.BlockType.Destruction);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
		case BlockType.Darkness : 
			
			hexType.ChangeHexType(HexType.BlockType.Darkness);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
			
		case BlockType.Empty : 
			
			hexType.ChangeHexType(HexType.BlockType.Empty);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
			
		default:
			hexType.ChangeHexType(HexType.BlockType.Fire);
			GetComponent<Renderer>().material = hexType.blockMaterial;
			break;
			
		}


	}

	public  bool CompareType( BlockType peg){
		
		switch(peg){
			
		case BlockType.Flow : 
			
			if( hexType.Flow == true){ return true; } else {return false;}
			
			break;
			
		case BlockType.Stone : 
			if( hexType.Stone == true){ return true; } else {return false;}
			break;
			
		case BlockType.Fire : 
			if( hexType.Fire == true){ return true; } else {return false;}
			break;
			
		case BlockType.Lite : 
			if( hexType.Lite == true){ return true; } else {return false;}
			break;
			
		case BlockType.Shield : 
			
			if( hexType.Shield == true){ return true; } else {return false;}
			break;
			
		case BlockType.Sword : 
			if( hexType.Sword == true){ return true; } else {return false;}
			break;
			
		case BlockType.Spear : 
			if( hexType.Spear == true){ return true; } else {return false;}
			break;
			
		case BlockType.Wealth : 
			if( hexType.Wealth == true){ return true; } else {return false;}
			break;
			
		case BlockType.Wisdom : 
			if( hexType.Wisdom == true){ return true; } else {return false;}
			
			break;
		case BlockType.TimeType : 
			
			if( hexType.TimeType == true){ return true; } else {return false;}
			break;
			
		case BlockType.Destruction : 
			
			if( hexType.Destruction == true){ return true; } else {return false;}
			break;
			
		case BlockType.Darkness : 
			if( hexType.Darkness == true){ return true; } else {return false;}
			
			break;
			
		case BlockType.Empty : 
			
			if( hexType.Flow == true){ return true; } else {return false;}
			break;
			
		default : if( hexType.Flow == true){ return true; } else {return false;}
			break;
		}
		
		
	}
}
