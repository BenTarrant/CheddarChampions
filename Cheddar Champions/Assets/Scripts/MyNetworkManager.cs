using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager
{
    //public static MyNetworkManager sNM;
    //public List<int> scores;
    //int PlayerScore;

    //public List<MousePlayerController> allPlayersConnected = new List<MousePlayerController>();

    //public void Awake()
    //{
    //    sNM = this;
    //}

    //public void Update()
    //{

    //}

    //public void FindAllPlayers()
    //{
    //    foreach (var obj in ClientScene.objects)
    //    {
    //        if (obj.Value.gameObject.GetComponent<MousePlayerController>() != null)
    //        {
    //            if (allPlayersConnected.Contains(obj.Value.gameObject.GetComponent<MousePlayerController>()) == false)
    //            {
    //                allPlayersConnected.Add(obj.Value.gameObject.GetComponent<MousePlayerController>());
    //            }
    //        }
    //    }

    //    if (allPlayersConnected.Count == 2)
    //    {
    //        allPlayersConnected.Sort(SortByScore);
    //    }
    //}

    //public void EndGame()
    //{
    //    // This needs to be called when the timer reaches 0
    //    //Need to compare all the scores (int values) of each player collected in the GetMyPlayers function
    //    //put the scores in an order or compare the values to find the highest (whichever is easiest)
    //    //disconnect all players or whatever network function will appropriatly 'restart' things
    //}

    //static int SortByScore(MousePlayerController p1, MousePlayerController p2)
    //{
    //    return p1.score.CompareTo(p2.score);
    //}
}
