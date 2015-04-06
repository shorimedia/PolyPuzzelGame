using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public void SetScene ( string SceneName) {
	
		Application.LoadLevel(SceneName);

	}
	
}
