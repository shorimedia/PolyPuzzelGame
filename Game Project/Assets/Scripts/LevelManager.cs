//Shori Media Game 2014 - 2015
//
//Setup each level and  etlls the board what a level can or cannot have
//

using UnityEngine;
using System.Collections;


[System.Serializable]
public class LevelManager  {

	public int levelNumber;
	public int startEmptyNum;
	public int typeVaries;
	public bool randomLoc = false;

	private float randomTypeNumA;
	private float randomTypeNumB;


	public LevelManager (int num){
		levelNumber = num;
		LeveSetup();
	}



public void UpdateLevelNum (int num){
		levelNumber = num;
		LeveSetup();
	}

	#region  Level Setup
	public void LeveSetup(){

		if(levelNumber == 1 || levelNumber == 6 || levelNumber == 11 || levelNumber == 16 || levelNumber == 22 || levelNumber == 27){
			startEmptyNum = 3;
			typeVaries = 1;

			if(levelNumber != 16 || levelNumber != 22 || levelNumber != 27){
			randomLoc = false;
			}else{ 
				randomLoc = true;
			}
			return;
		}

		if(levelNumber == 2 || levelNumber == 7 || levelNumber == 12 || levelNumber == 17 || levelNumber == 23 || levelNumber == 28){
			startEmptyNum = 2;
			typeVaries = 1;

			if(levelNumber != 17 ){
				randomLoc = false;
			}else{ 
				randomLoc = true;
			}
			return;
		}

		if(levelNumber == 3 || levelNumber == 8 || levelNumber == 13 || levelNumber == 18 ){
			startEmptyNum = 2;
			typeVaries = 1;
			randomLoc = true;
			return;
		}

		if(levelNumber == 4 || levelNumber == 9 || levelNumber == 14 || levelNumber == 19 || levelNumber == 24 || levelNumber == 29){
			startEmptyNum = 1;
			typeVaries = 1;
			randomLoc = false;
			return;
		}

		if(levelNumber == 5 || levelNumber == 10 || levelNumber == 15 || levelNumber == 20 || levelNumber == 25 || levelNumber == 30){
			startEmptyNum = 1;
			typeVaries = 1;
			randomLoc = true;
			return;
		}

		if(levelNumber == 21 || levelNumber == 26 ){
			startEmptyNum = 4;
			typeVaries = 1;

			if(levelNumber != 21 ){
				randomLoc = false;
			}else{ 
				randomLoc = true;
			}
			return;
		}


	}
	#endregion

	#region Level Guide
	public void LevelTypeSelect(HexBlock hex){

		int numberType = 0;
		float randomNumHolder = RandomNumber();

	
		if(levelNumber >= 1 && levelNumber <= 5){
			#region Levels 1 to 5
			if(randomTypeNumA  <= 0.33f){
				numberType = 1;
			}
			
			if(randomTypeNumA  >  0.33f && randomTypeNumA  <= 0.66f){
				numberType = 2;
			}
			
			if(randomTypeNumA  >  0.66f && randomTypeNumA  <= 1){
				numberType = 3;
			}
			
			switch(numberType){
			case 1: hex.blockType = HexBlock.BlockType.Fire; break;
			case 2: hex.blockType = HexBlock.BlockType.Flow; break;
			case 3: hex.blockType = HexBlock.BlockType.Stone; break;
			}
			#endregion
		}else

		if(levelNumber >= 6 && levelNumber <= 10){

			#region Levels 6 to 10
			//Randomly altnate between two types
			//Type one
			if( randomNumHolder <= 0.70f){
				
				// pick one of three
				if(randomTypeNumA  <= 0.33f){
					numberType = 1;
				}
				
				if(randomTypeNumA  >  0.33f && randomTypeNumA  <= 0.66f){
					numberType = 2;
				}
				
				if(randomTypeNumA  >  0.66f && randomTypeNumA  <= 1){
					numberType = 3;
				}
			} 
			
			
			//Type two
			if( randomNumHolder > 0.70f && randomNumHolder <= 1.0f){
				
				// pick one of three 
				if(randomTypeNumB  <= 0.33f){
					numberType = 1;
				}
				
				if(randomTypeNumB  >  0.33f && randomTypeNumA  <= 0.66f){
					numberType = 2;
				}
				
				if(randomTypeNumB  >  0.66f && randomTypeNumA  <= 1){
					numberType = 3;
				}
				
			} 
			
			switch(numberType){ 
			case 1: hex.blockType = HexBlock.BlockType.Fire; break;
			case 2: hex.blockType = HexBlock.BlockType.Flow; break;
			case 3: hex.blockType = HexBlock.BlockType.Stone; break;
			}
			#endregion
		}else

		if(levelNumber >= 11 && levelNumber <= 15){
			#region Levels 11 to 15
			//Randomly altnate between two types
			//Type one
			if( randomNumHolder <= 0.65f){
				
				// pick one of three
				if(randomTypeNumA  <= 0.33f){
					numberType = 1;
				}
				
				if(randomTypeNumA  >  0.33f && randomTypeNumA  <= 0.66f){
					numberType = 2;
				}
				
				if(randomTypeNumA  >  0.66f && randomTypeNumA  <= 1){
					numberType = 3;
				}
			} 
			
			
			//Type two
			if( randomNumHolder > 0.65f && randomNumHolder <= 1.0f){
				
				numberType = Random.Range(1,5);

			} 
			
			switch(numberType){ 
			case 1: hex.blockType = HexBlock.BlockType.Fire; break;
			case 2: hex.blockType = HexBlock.BlockType.Flow; break;
			case 3: hex.blockType = HexBlock.BlockType.Stone; break;
			case 4: hex.blockType = HexBlock.BlockType.Lite; break;
			}
			#endregion
			
		}else
		if(levelNumber >= 16 && levelNumber <= 20){
			#region Levels 16 to 20
			//Randomly altnate between two types
			//Type one
			if( randomNumHolder <= 0.60f){
				
				// pick one of three
				if(randomTypeNumA  <= 0.33f){
					numberType = 1;
				}
				
				if(randomTypeNumA  >  0.33f && randomTypeNumA  <= 0.66f){
					numberType = 2;
				}
				
				if(randomTypeNumA  >  0.66f && randomTypeNumA  <= 1){
					numberType = 3;
				}
			} 
			
			
			//Type two
			if( randomNumHolder > 0.60f && randomNumHolder <= 1.0f){
				
				numberType = Random.Range(1,8);
				
			} 
			
			switch(numberType){ 
			case 1: hex.blockType = HexBlock.BlockType.Fire; break;
			case 2: hex.blockType = HexBlock.BlockType.Flow; break;
			case 3: hex.blockType = HexBlock.BlockType.Stone; break;
			case 4: hex.blockType = HexBlock.BlockType.Lite; break;
			case 5: hex.blockType = HexBlock.BlockType.Shield; break;
			case 6: hex.blockType = HexBlock.BlockType.Sword; break;
			case 7: hex.blockType = HexBlock.BlockType.Spear; break;

			}
			#endregion
			
		}else
		if(levelNumber >= 21 && levelNumber <= 25){
			#region Levels 21 to 25
			//Randomly altnate between two types
			//Type one
			if( randomNumHolder <= 0.60f){
				
				// pick one of three
				if(randomTypeNumA  <= 0.33f){
					numberType = 1;
				}
				
				if(randomTypeNumA  >  0.33f && randomTypeNumA  <= 0.66f){
					numberType = 2;
				}
				
				if(randomTypeNumA  >  0.66f && randomTypeNumA  <= 1){
					numberType = 3;
				}
			} 
			
			
			//Type two
			if( randomNumHolder > 0.60f && randomNumHolder <= 1.0f){
				
				numberType = Random.Range(1,10);
				
			} 
			
			switch(numberType){ 
			case 1: hex.blockType = HexBlock.BlockType.Fire; break;
			case 2: hex.blockType = HexBlock.BlockType.Flow; break;
			case 3: hex.blockType = HexBlock.BlockType.Stone; break;
			case 4: hex.blockType = HexBlock.BlockType.Lite; break;
			case 5: hex.blockType = HexBlock.BlockType.Shield; break;
			case 6: hex.blockType = HexBlock.BlockType.Sword; break;
			case 7: hex.blockType = HexBlock.BlockType.Spear; break;
			case 8: hex.blockType = HexBlock.BlockType.Wealth; break;
			case 9: hex.blockType = HexBlock.BlockType.Wisdom; break;

				
			}
			#endregion
		}else

		if(levelNumber >= 26 && levelNumber <= 30){
			#region Levels 26 to 30
			//Randomly altnate between two types
			//Type one
			if( randomNumHolder <= 0.55f){
				
				// pick one of three
				if(randomTypeNumA  <= 0.33f){
					numberType = 1;
				}
				
				if(randomTypeNumA  >  0.33f && randomTypeNumA  <= 0.66f){
					numberType = 2;
				}
				
				if(randomTypeNumA  >  0.66f && randomTypeNumA  <= 1){
					numberType = 3;
				}
			} 
			
			
			//Type two
			if( randomNumHolder > 0.55f && randomNumHolder <= 1.0f){
				
				numberType = Random.Range(1,13);
				
			} 
			
			switch(numberType){ 
			case 1: hex.blockType = HexBlock.BlockType.Fire; break;
			case 2: hex.blockType = HexBlock.BlockType.Flow; break;
			case 3: hex.blockType = HexBlock.BlockType.Stone; break;
			case 4: hex.blockType = HexBlock.BlockType.Lite; break;
			case 5: hex.blockType = HexBlock.BlockType.Shield; break;
			case 6: hex.blockType = HexBlock.BlockType.Sword; break;
			case 7: hex.blockType = HexBlock.BlockType.Spear; break;
			case 8: hex.blockType = HexBlock.BlockType.Wealth; break;
			case 9: hex.blockType = HexBlock.BlockType.Wisdom; break;
			case 10: hex.blockType = HexBlock.BlockType.TimeType; break;
			case 11: hex.blockType = HexBlock.BlockType.Darkness; break;
			case 12: hex.blockType = HexBlock.BlockType.Destruction; break;
				
			}
			#endregion
		}

	}

#endregion


	public void SetRandomNum(float numA, float numB){

		if( numA != numB){
		randomTypeNumA = numA;
		randomTypeNumB = numB;
		}else{
			randomTypeNumA = numA;
			randomTypeNumB = Random.Range(0f,1.0f);;

		}

	}

	public float RandomNumber(){
		return Random.Range(0f,1.0f);
	}

}
