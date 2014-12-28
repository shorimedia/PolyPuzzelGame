namespace UnityEngine.Advertisements.HTTPLayer {

  using UnityEngine;
  using UnityEngine.Advertisements.Event;
  using System.Collections;

  internal class RetryRequest {
    protected int retryPosition;
		protected int[] retryDelayTable;
		protected HTTPRequest request;
		protected System.Action<HTTPResponse> callback;
		protected bool keepRetrying;
		protected bool callbackDelivered;
		protected bool useDeadline = false;
		protected float retryDeadline = 0;
		protected int deadlineDelay = 0;

    public RetryRequest(int[] delays, int maxDelay, HTTPRequest req) {
      retryPosition = 0;
      retryDelayTable = delays;

      if(maxDelay > 0) {
        deadlineDelay = maxDelay;
        useDeadline = true;
      }

      request = req;
    }

    public void execute(System.Action<HTTPResponse> eventCallback) {
      callback = eventCallback;

      keepRetrying = true;
      callbackDelivered = false;

      if(useDeadline) {
        retryDeadline = Time.realtimeSinceStartup + deadlineDelay;
      }

      retry();

      if(useDeadline) {
        AsyncExec.runWithDelay(deadlineDelay, executeDeadline);
      }
    }

		protected virtual void HTTPCallback(HTTPResponse res) {
      // network error
      if(res.error) {
        if(!keepRetrying && !callbackDelivered) {
          failedCallback("Network error");
        }
  
        return;
      }

      EventJSON jsonResponse = new EventJSON(System.Text.Encoding.UTF8.GetString(res.data, 0, res.data.Length));

      // check that server response has status "ok"
      if(jsonResponse.hasInt("status")) {
        if(jsonResponse.getInt("status") == 200) {
          // event delivery successful
          keepRetrying = false;

          if(!callbackDelivered) {
            callbackDelivered = true;
            callback(res);
          }

          return;
        } 
      }

      // if we didn't get status "ok", then whatever we got will be treated as error

      if(jsonResponse.hasBool("retryable")) {
        bool retry = jsonResponse.getBool("retryable");
        if(!retry) {
          // We have received an error and retrying has been explicitly forbidden
          keepRetrying = false;
          if(!callbackDelivered) {
            failedCallback("Retrying forbidden by remote server");
          }
          return;
        }
      }

      // We have received an error so if there are no more retries, deliver the callback
      if(!keepRetrying && !callbackDelivered) {
        failedCallback("Error");
      }
    }

    protected void retry() {
      if(!keepRetrying) {
        return;
      }

      HTTPRequest req = request.getClone();
      req.execute(HTTPCallback);

      if(retryPosition < retryDelayTable.Length && (!useDeadline || Time.realtimeSinceStartup < retryDeadline)) {
        int delay = retryDelayTable[retryPosition++];

        if(delay > 0) {
          AsyncExec.runWithDelay(delay, retry);
        } else {
          keepRetrying = false;
        }
      } else {
        keepRetrying = false;
      }
    }

		protected void executeDeadline() {
      keepRetrying = false;

      if(!callbackDelivered) {
        failedCallback("Retry deadline exceeded");
      }
    }

		protected void failedCallback(string msg) {
      callbackDelivered = true;

      HTTPResponse res = new HTTPResponse();
      res.url = request.url;
      res.error = true;
      res.errorMsg = msg;

      callback(res);
    }
  }

	internal class RetryFileRequest : RetryRequest {
		public RetryFileRequest(int[] delays, int maxDelay, HTTPRequest req):base (delays, maxDelay, req) {}
		protected override void HTTPCallback(HTTPResponse res) {
			// network error
			if(res.error) {
				if(!keepRetrying && !callbackDelivered) {
					failedCallback("Network error");
				}
				
				return;
			}

			if(res.dataLength != 0) {
				keepRetrying = false;
				
				if(!callbackDelivered) {
					callbackDelivered = true;
					callback(res);
				}
				
				return;
			} 
			if(!keepRetrying && !callbackDelivered) {
				failedCallback("Error");
			}
		}
	}
}
