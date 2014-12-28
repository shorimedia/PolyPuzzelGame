namespace UnityEngine.Advertisements {
  using UnityEngine;
  using System.Collections;

  internal class AsyncExec {
    private static GameObject asyncExecGameObject;
    private static MonoBehaviour coroutineHost;
    private static AsyncExec asyncImpl;
    private static bool init = false;

    private static MonoBehaviour getImpl() {
      if(!init) {
        asyncImpl = new AsyncExec();
        asyncExecGameObject = new GameObject("Unity Ads Coroutine Host") { hideFlags = HideFlags.HideAndDontSave };
        coroutineHost = asyncExecGameObject.AddComponent<MonoBehaviour>();
        Object.DontDestroyOnLoad(asyncExecGameObject);
        init = true;
      }

      return coroutineHost;
    }

    private static AsyncExec getAsyncImpl() {
      if(!init) {
        getImpl();
      }

      return asyncImpl;
    }

    public static void run(IEnumerator method) {
      getImpl().StartCoroutine(method);
    }

    public static void runWithCallback<T>(System.Func<System.Action<T>,IEnumerator> asyncMethod, System.Action<T> callback) {
      getImpl().StartCoroutine(asyncMethod(callback));
    }

    public static void runWithCallback<K,T>(System.Func<K,System.Action<T>,IEnumerator> asyncMethod, K arg0, System.Action<T> callback) {
      getImpl().StartCoroutine(asyncMethod(arg0, callback));
    }

    public static void runWithDelay(int delay, System.Action callback) {
      getImpl().StartCoroutine(getAsyncImpl().delayedCallback(delay, callback));
    }

    private IEnumerator delayedCallback(int delay, System.Action callback) {
      float start = Time.realtimeSinceStartup;

      while(Time.realtimeSinceStartup < start + delay) {
        yield return null;
      }

      callback();
    }
  }
}
