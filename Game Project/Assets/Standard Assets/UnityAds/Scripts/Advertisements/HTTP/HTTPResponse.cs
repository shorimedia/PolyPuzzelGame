namespace UnityEngine.Advertisements.HTTPLayer {
  using UnityEngine;
  using System.Collections;
  using System.Collections.Generic;

  internal class HTTPResponse {
    public string url;
    public byte[] data;
    public int dataLength;
    public bool error;
    public string errorMsg;
    public Dictionary<string,string> headers;
	public string etag;
  }
}