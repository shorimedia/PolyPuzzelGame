using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraMechinics : MonoBehaviour {

	public float smooth;
	public Transform target;

	public Transform[] CameraLocs = new Transform[5];
	public float offset;
	public bool canMove = true;
	public int posIndex = 1;

	public Button[] PosBtn = new Button[5];




	
	public int PositionIndex
	{
		get{return posIndex;}
		set{posIndex = value;}
	}

	public bool MoveCamera
	{
		get{return canMove;}
		set{canMove = value;}
	}

	void Update() {
		transform.LookAt(target);

		if(canMove == true){
			StartCoroutine(ChangeCamPosition(posIndex));
			canMove = false;
		}
	}


	IEnumerator  ChangeCamPosition(int camPos){

		while(Vector3.Distance(transform.position,  CameraLocs[camPos].position) > 0.05f)
		{

		switch(camPos){
			case 0:  transform.position = Vector3.Lerp(transform.position, CameraLocs[0].position, smooth * Time.deltaTime); break;
		case 1: transform.position = Vector3.Lerp(transform.position, CameraLocs[1].position, smooth * Time.deltaTime);  break;
		case 2: transform.position = Vector3.Lerp(transform.position, CameraLocs[2].position, smooth * Time.deltaTime);  break;
		case 3: transform.position = Vector3.Lerp(transform.position, CameraLocs[3].position, smooth * Time.deltaTime);  break;
		case 4: transform.position = Vector3.Lerp(transform.position, CameraLocs[4].position, smooth * Time.deltaTime);  break;
		}
			yield return null;
		}
		Debug.Log("Reached the target.");
		yield return new WaitForSeconds(3f);
	}


}
