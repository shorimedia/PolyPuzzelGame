namespace UnityEngine.Advertisements.Event {

  using UnityEngine;
  using UnityEngine.Advertisements;
  using UnityEngine.Advertisements.HTTPLayer;
  using System.Collections;
  using System.Text;

  internal class Event {
    private static string reqIdBase;
    private static int reqIndex;
    private static System.DateTime unixEpoch;
    private string url;
    private int[] retryDelayTable;
    private string jsonData;
    private System.Action<bool> callback;

    // No retries after 20 minutes since first delivery attempt
    private int deadlineDelay = 20 * 60;

    public Event(string eventUrl, int[] delays, bool useReqId, string eventJson, string infoJson) {
      url = eventUrl;
      retryDelayTable = delays;

      prepareJsonData(useReqId, eventJson, infoJson);
    }

    private void prepareJsonData(bool useReqId, string eventJson, string infoJson) {
      StringBuilder sb = new StringBuilder("{ ");

      if(useReqId) {
        sb.Append("\"req_id\": \"");
        sb.Append(reqIdBase);
        sb.Append("-");
        sb.Append((int)(System.DateTime.UtcNow - unixEpoch).TotalMilliseconds);
        sb.Append("-");
        sb.Append(reqIndex++);
        sb.Append("\", ");
      }

      sb.Append("\"event\": ");
      sb.Append(eventJson);
      sb.Append(", \"info\": ");
      sb.Append(infoJson);
      sb.Append(" }");

      jsonData = sb.ToString();
    }

    public static void init() {
      reqIndex = 1;

      unixEpoch = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);

      string bundle = DeviceInfo.bundleID();
      string deviceId = DeviceInfo.deviceID();
      string hashSrc;

      if(bundle.Length > 0 && deviceId.Length > 0) {
        reqIdBase = "a-";
        hashSrc = bundle + "-" + deviceId;
      } else {
        System.Random rng = new System.Random();
        reqIdBase = "b-";
        hashSrc = (int)((System.DateTime.UtcNow - unixEpoch).TotalMilliseconds) + "-" + rng.Next();
      }

      byte[] srcBytes = System.Text.Encoding.UTF8.GetBytes(hashSrc);

      System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
      byte[] destBytes = md5.ComputeHash(srcBytes);

      string finalHash = System.BitConverter.ToString(destBytes).Replace("-", string.Empty);

      reqIdBase += finalHash + "-";
    }

    public void execute(System.Action<bool> eventCallback) {
      callback = eventCallback;

      HTTPRequest req = new HTTPRequest("POST", url);
      req.addHeader("Content-Type", "application/json");
      req.setPayload(jsonData);

      HTTPManager.sendRequest(req, HTTPCallback, retryDelayTable, deadlineDelay);
    }

    private void HTTPCallback(HTTPResponse res) {
      if(res.error) {
        callback(false);
      } else {
        callback(true);
      }
    }
  }
}