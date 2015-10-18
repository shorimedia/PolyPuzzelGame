using UnityEngine;
using System.Collections;

public class PObjectDestroy : MonoBehaviour {

    public float time = 1.7f;
    void OnEnable()
    {
        Invoke("Destroy", time);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
