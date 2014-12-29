using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class board : MonoBehaviour {

	public GameObject hex;

	public Transform[] hexagonToken = new Transform[169];
	public List<HexBlock> TokenData = new List<HexBlock>();


	// Use this for initialization
	void Start () {
		StartGame(1);
	}




	void StartGame (int LvNum) {

		switch(LvNum){
		case 1: 
			Generator(61);
			break;
		case 2: 
			Generator(91);
			break;
		case 3: 
			Generator(127);
			break;

		case 4: 
			Generator(169);
			break;
		}
	}
	



	void Grid(){






	}


	void Generator(int hexAmount){
		GameObject go;

		for(int i = 0; i < hexAmount; i++){

			go = Instantiate(hex, new Vector3(hexagonToken[i].position.x,hexagonToken[i].position.y + 1 ,hexagonToken[i].position.z), hexagonToken[i].rotation) as GameObject;
			TokenData.Add(go.GetComponent<HexBlock>());


			// set center token to empty
			if(i == 0){
				TokenData[i].blockType = HexBlock.BlockType.Empty;
				TokenData[i].ChangeBlockType();
			}
		}


	}

	public void EmptyCheck(){

		for(int i = 0; i < TokenData.Count; i++){

			if (TokenData[i].blockType == HexBlock.BlockType.Empty){
				GameManger.CURRENTNUMEMPTY++;
			}
		}
	} 


	void RandomType(){

	}
}
