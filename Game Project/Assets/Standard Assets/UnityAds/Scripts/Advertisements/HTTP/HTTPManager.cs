namespace UnityEngine.Advertisements.HTTPLayer {
  using UnityEngine;
  using System.Collections;

  internal class HTTPManager {
    // request: HTTP request to be sent and retried
    // callback: callback method to call with server response (or failure)
    // delays: table with retry delays in seconds, allows exponential retry strategy e.g. [10, 20, 40, 80, 160, 320] means six retries with increasing intervals
    // maxDelay: maximum amount of seconds until callback is sent and all pending retries are considered timed out
    
		public static void sendFileRequest(HTTPRequest request, System.Action<HTTPResponse> callback, int[] delays, int maxDelay) {
			RetryFileRequest req = new RetryFileRequest(delays, maxDelay, request);
			req.execute(callback);
		}

		public static void sendRequest(HTTPRequest request, System.Action<HTTPResponse> callback, int[] delays, int maxDelay) {
      RetryRequest req = new RetryRequest(delays, maxDelay, request);

      req.execute(callback);
    }

    // convenience method for generating simple retry strategy
    public static void sendRequest(HTTPRequest request, System.Action<HTTPResponse> callback, int retries, int delay, int maxDelay) {
      int[] delays = new int[retries];

      for(int i = 0; i < retries; i++) {
        delays[i] = delay;
      }

      sendRequest(request, callback, delays, maxDelay);
    }
  }
}