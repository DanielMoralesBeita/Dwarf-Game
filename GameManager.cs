﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    // declares variables needed to ensure this is the only instance of the game and sets 
    // loadedGame to false, as we haven't yet loaded the game

    public static GameManager Instance = null;
    public static BoardManager boardScript;
    public static LogicManager logicScript;
    public static MeleeUnitManager meleeUnitScript;
    public static Sweeper sweeperScript;
    public static bool initiatedGame = false;  // set me to true manually to use BoardController to create map prefabs
    public static bool inMenu = false;
    public static int playerCycle = 1;


    void Awake()
    {
        if (Instance == null)
            Instance = this;

        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        meleeUnitScript = GetComponent<MeleeUnitManager>();
        logicScript = GetComponent<LogicManager>();
        sweeperScript = GetComponent<Sweeper>();
        InitGame();
    }
    void InitGame()
    {

            // always passes 0 on start, because 0 is bmid for main menu

            //boardScript.InitialiseGrid();
            boardScript.BoardController(0);
    }

    // will hold most global updates, currently updates are somewhat scattered

    private void Update()
    {
        
        if ((Input.GetKeyDown(KeyCode.R)) && playerCycle < 7)
            playerCycle++;
        if ((Input.GetKeyDown(KeyCode.L)) && playerCycle > 1)
            playerCycle--;
        
        // saves on get key down j
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameState.Current.Player1Serial = Player1Controller.player1Serial;
            GameState.Current.Player2Serial = Player2Controller.player2Serial;
            GameState.Current.Player3Serial = Player3Controller.player3Serial;
            GameState.Current.Player4Serial = Player4Controller.player4Serial;
            GameState.Current.Player5Serial = Player5Controller.player5Serial;
            GameState.Current.Player6Serial = Player6Controller.player6Serial;
            GameState.Current.Player7Serial = Player7Controller.player7Serial;
            GameState.Current.BMID = 1;
            GameState.Current.Location = "Not Implemented";
            GameState.Current.DateTime = System.DateTime.Now.ToString();
            Saves.Save();
            return;
        }

        // loads on get key down k

        if (Input.GetKeyDown(KeyCode.K))
        {
            Saves.Load();
            return;
        }

        if (Input.GetKey("escape"))
        {
            boardScript.menuText.rectTransform.transform.position = new Vector3(0, 0, 0f);
            Application.Quit();
        }
    }
}

