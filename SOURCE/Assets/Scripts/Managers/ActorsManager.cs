using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActorsManager : SingletonMonoBehaviour<ActorsManager>
{
    private List<BasePlayer> players = new List<BasePlayer>();

    public void RegisterPlayer(BasePlayer pPlayer)
    {
        if(!players.Contains(pPlayer))
            players.Add(pPlayer);
    }

    public List<BasePlayer> GetPlayers()
    {
        return players;
    }
}
