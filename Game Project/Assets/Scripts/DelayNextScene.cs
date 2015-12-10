using UnityEngine;
using System.Collections;

public class DelayNextScene : MonoBehaviour {

    public float delayTime = 0.5f;
    public string nextSceneName = "Game_Main";

    public bool isAnimation = false;
    public float animateSpeed = 1;
    public FadeScreen animateFadeOut;

    private float time;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        time = Time.time;

        if (time >= delayTime && isAnimation == false)
        {
            Application.LoadLevel(nextSceneName);
        } else
            if ( isAnimation == true)
        {

            if (time >= (delayTime - animateSpeed))
            {
                animateFadeOut.IsOpen = false;
                    
                    }

            if (time >= delayTime)
            {
                Application.LoadLevel(nextSceneName);

            }

        }
	
	}
}
