using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerTurn
{
    Player1,
    Player2,
    Player3,
    Player4,
    // Add more players if needed
}

public class PlayerData
{
    public int score;
    public int turn;
    public PlayerData(int turn)
    {
        this.turn = turn;
        score = 0;
    }
}

public class TurnManager : MonoBehaviour
{

    public int players = 1;

    private List<PlayerData> playerTurns;
    private int currentPlayerIndex;

    public PlayerData playerData
    {
        get { return playerTurns[currentPlayerIndex]; }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initializeTurns()
    {
        playerTurns = new List<PlayerData>();

        if(players > 4)
        {
            players = 4;
        }

        for(int i = 0; i < players; i++ )
        {
            playerTurns.Add(new PlayerData(i));
        }
        currentPlayerIndex = 0;
    } 
    public void switchTurns()
    {
        currentPlayerIndex++;

        if(currentPlayerIndex >= playerTurns.Count)
        {
            currentPlayerIndex = 0;
        }
    }
}
