using UnityEngine;
using System.Collections;

public class OpenWeb : MonoBehaviour {

   public string websiteURL;

  public  void OpenWebsite()
   {
       Application.OpenURL(websiteURL);
   }

}
