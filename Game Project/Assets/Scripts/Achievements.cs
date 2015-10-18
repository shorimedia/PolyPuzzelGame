using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class Achievements : MonoBehaviour {


    public static void PegAchievements(int pegCountx, float timeX)
    {
        PlayGamesPlatform.Activate();

        switch (pegCountx)
        {
            case 1: 

                //unlock achievement
                //Just One Peg
                 Social.ReportProgress(
                        "CgkI1Kbp77AXEAIQAw", 100.0f,
                        (bool success) =>
                        {
                            // handle success or failure
                        });

                 //increment achievement
                 //Solitaire Novice
                 ((PlayGamesPlatform)Social.Active).IncrementAchievement("CgkI1Kbp77AXEAIQBA", 1,
                    (bool success) =>
                    {
                        // handle success or failure
                    });

                 //increment achievement
                 //Solitaire 
                 ((PlayGamesPlatform)Social.Active).IncrementAchievement("CgkI1Kbp77AXEAIQBQ", 1,
                    (bool success) =>
                    {
                        // handle success or failure
                    });

                 //increment achievement
                 //Solitaire Master
                 if (timeX <= 60.0f)//if time is less then 1 miutes
                 {
                     ((PlayGamesPlatform)Social.Active).IncrementAchievement("CgkI1Kbp77AXEAIQCQ" , 5,
                        (bool success) =>
                        {
                            // handle success or failure
                        });
                 }
                
                
                break;


            case 2:
                //unlock achievement
                //two Peg
                Social.ReportProgress(
                       "CgkI1Kbp77AXEAIQAg", 100.0f,
                       (bool success) =>
                       {
                           // handle success or failure
                       });

                break;

            case 3:
                //unlock achievement
                //two Peg
                Social.ReportProgress(
                       "CgkI1Kbp77AXEAIQAQ", 100.0f,
                       (bool success) =>
                       {
                           // handle success or failure
                       });

                break;

        }


        // Timed achievements

        if(timeX <= 180.0f ) // 3 mins (time in seconds)
        {
            Social.ReportProgress(
                     "CgkI1Kbp77AXEAIQBw", 100.0f,
                     (bool success) =>
                     {
                         // handle success or failure
                     });
        }


        if (timeX <= 120.0f) // 2 mins (time in seconds)
        {
            Social.ReportProgress(
                     "CgkI1Kbp77AXEAIQCA", 100.0f,
                     (bool success) =>
                     {
                         // handle success or failure
                     });
        }

        if (timeX <= 60.0f) // 1 mins (time in seconds)
        {
            Social.ReportProgress(
                     "CgkI1Kbp77AXEAIQCQ", 100.0f,
                     (bool success) =>
                     {
                         // handle success or failure
                     });
        }

    
    }


    public static void MaxPoints()
    {
        Social.ReportProgress(
                    "CgkI1Kbp77AXEAIQCg", 100.0f,
                    (bool success) =>
                    {
                        // handle success or failure
                    });
    
    }


    public static void  HighScoreLeaderBoard(int score)
    {
        Social.ReportScore(score,
            "CgkI1Kbp77AXEAIQCw",
                  (bool success) =>
                      {
                          // handle success or failure
                      });
    }

    public static void FastTimeLeaderBoard(int score)
    {
        Social.ReportScore(score,
            "CgkI1Kbp77AXEAIQDA",
                  (bool success) =>
                  {
                      // handle success or failure
                  });
    }

}
