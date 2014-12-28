namespace UnityEngine.Advertisements.HTTPLayer {
  using UnityEngine;
  using UnityEngine.Advertisements;
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.Reflection;
  using System.Text;

  internal class HTTPRequest {
    public string url;

    // Switch to true to print network requests to debug log
    private bool networkLogging = false;

    private enum HTTPMethod {
      GET,
      POST 
    };
    private HTTPMethod method;
    private byte[] postData;
    private Dictionary<string, string> headers;

    public HTTPRequest(string newUrl) {
      url = newUrl;
      method = HTTPMethod.GET;
    }

    public HTTPRequest(string newMethod, string newUrl) {
      url = newUrl;

      method = HTTPMethod.GET;

      if(newMethod.Equals("POST")) {
        method = HTTPMethod.POST;
        headers = new Dictionary<string, string>();
      }
    }

    public HTTPRequest getClone() {
      HTTPRequest clone;

      if(method == HTTPMethod.GET) {
        clone = new HTTPRequest(url);
      } else { // HTTPMethod.POST
        clone = new HTTPRequest("POST", url);
      }

      if(postData != null) {
        if(postData.Length > 0) {
          clone.setPayload(postData);
        }
      }

      if(headers != null) {
        if(headers.Count > 0) {
          foreach(KeyValuePair<string, string> pair in headers) {
            clone.addHeader((string)pair.Key, (string)pair.Value);
          }
        }
      }

      return clone;
    }

    public void setPayload(string payload) {
      postData = Encoding.UTF8.GetBytes(payload);
    }

    public void setPayload(byte[] payload) {
      postData = payload;
    }

    public void addHeader(string header, string value) {
      headers.Add(header, value);
    }

    public void execute(System.Action<HTTPResponse> callback) {
      if(Application.internetReachability == NetworkReachability.NotReachable) {
        HTTPResponse response = new HTTPResponse();
        response.url = url;
        response.error = true;
        response.errorMsg = "Internet not reachable";

        callback(response);

        return;
      }

      if(method == HTTPMethod.POST) {
        if(postData == null) {
          postData = new byte[0];
        }

        AsyncExec.runWithCallback<HTTPRequest,HTTPResponse>(executePost, this, callback);
      } else {
        AsyncExec.runWithCallback<HTTPRequest,HTTPResponse>(executeGet, this, callback);
      }
    }

    private IEnumerator executeGet(HTTPRequest req, System.Action<HTTPResponse> callback) {
      if(networkLogging) {
        printUp("GET", req.url);
      }

      WWW www = new WWW(req.url);

      yield return www;

      HTTPResponse response = processWWW(www);
      response.url = req.url;

      if(networkLogging) {
        printDown(req.url, www.bytes);
      }

      callback(response);
    }

    private IEnumerator executePost(HTTPRequest req, System.Action<HTTPResponse> callback) {
      WWW www = null;

      if(networkLogging) {
        printUp("POST", req.url);
      }

      Type wwwType = typeof(UnityEngine.WWW);
      ConstructorInfo wwwConstructor = wwwType.GetConstructor(new Type[] {typeof(string), typeof(byte[]), typeof(Dictionary<string, string>)});
      ConstructorInfo wwwConstructorOld = wwwType.GetConstructor(new Type[] {typeof(string), typeof(byte[]), typeof(Hashtable)});
      if(wwwConstructor == null && wwwConstructorOld != null) {
        Hashtable tempHeaders = new Hashtable(req.headers);
        www = (WWW)wwwConstructorOld.Invoke(new object[] {req.url, req.postData, tempHeaders});
      } else {
        www = (WWW)wwwConstructor.Invoke(new object[] {req.url, req.postData, req.headers});
      }

      yield return www;

      HTTPResponse response = processWWW(www);
      response.url = req.url;

      if(networkLogging) {
        printDown(req.url, www.bytes);
      }

      callback(response);
    }

    private HTTPResponse processWWW(WWW www) {
      HTTPResponse response = new HTTPResponse();

      if(!string.IsNullOrEmpty(www.error)) {
        response.error = true;
        response.errorMsg = www.error;
      } else {
        response.error = false;
        response.errorMsg = null;
        response.data = www.bytes;
        response.dataLength = www.bytes.Length;
    response.headers = new Dictionary<string,string>();
    response.etag = "";

    if(www.responseHeaders != null) {
      foreach(KeyValuePair<string,string> header in www.responseHeaders) {
      response.headers.Add(header.Key.ToUpper(), header.Value);
      }
      
      if(response.headers.ContainsKey("ETAG")) {
      response.etag = response.headers["ETAG"];
      }
    }
      }

      return response;
    }

    private void printUp(string method, string url) {
      Debug.Log(Time.realtimeSinceStartup + " -> " + method + " [" + url + "]");
    }

    private void printDown(string url, byte[] data) {
      if(data.Length < 16384) {
        Debug.Log(Time.realtimeSinceStartup + " <- [" + url + "]: " + Encoding.UTF8.GetString (data, 0, data.Length));
      } else {
        Debug.Log(Time.realtimeSinceStartup + " <- [" + url + "]: " + data.Length + " bytes");
      }
    }
  }
}
