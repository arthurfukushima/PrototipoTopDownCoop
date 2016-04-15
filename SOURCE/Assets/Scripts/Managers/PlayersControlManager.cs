using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayersControlManager : SingletonMonoBehaviour<PlayersControlManager> 
{
    public List<BasePlayer> players = new List<BasePlayer>();
    public BasePlayer currentPlayer;
    private int currentPlayerIndex;

    public void RegisterPlayer(BasePlayer pPlayer)
    {
        players.Add(pPlayer);
        pPlayer.hasControl = false;

        if(currentPlayer == null)
        {
            currentPlayer = pPlayer;
            currentPlayer.hasControl = true;
            Camera.main.GetComponent<CameraFollow>().followTarget = currentPlayer.transform;
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ControlNextPlayer();            
        }
	}

    private void ControlNextPlayer()
    {
        currentPlayer.hasControl = false;

        currentPlayerIndex++;
        if(currentPlayerIndex >= players.Count)
            currentPlayerIndex = 0;

        currentPlayer = players[currentPlayerIndex];
        currentPlayer.hasControl = true;

        Camera.main.GetComponent<CameraFollow>().followTarget = currentPlayer.transform;
    }
}
