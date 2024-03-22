using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    [Header("Scripts")]
    public PlayerMovement playerMovement;
    private SaveGame saveGame;
    private PlayerDetails playerDetails;
    private GameMenuOptions gameMenuOptions;

    [Header("Reset Buttons")]
    private Button r1;
    private Button r2;
    private Button r3;
    private Button r4;

    [Header("Load Buttons")]
    private Button l1;
    private Button l2;
    private Button l3;
    private Button l4;

    [Header("")]
    public GameObject shipLocation;
    private bool forBoys = false;
    private GameObject forBoysGO;
    private GameObject notForBoysGO;
    public string saveNo;
    public string[] details = new string[6];

    void Start()
    {
        forBoysGO = this.transform.GetChild(0).gameObject;
        notForBoysGO = this.transform.GetChild(1).gameObject;

        playerDetails = GameObject.Find("GameAssets").GetComponent<PlayerDetails>();
        gameMenuOptions = this.transform.parent.GetComponent<GameMenuOptions>();
        forBoys = gameMenuOptions.forBoys;

        if (forBoys)
        {
            notForBoysGO.SetActive(false);
            forBoysGO.SetActive(true);

            r1 = this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>();
            r2 = this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>();
            r3 = this.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Button>();
            r4 = this.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Button>();

            l1 = this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(2).GetComponent<Button>();
            l2 = this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(2).GetComponent<Button>();
            l3 = this.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(2).GetComponent<Button>();
            l4 = this.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(2).GetComponent<Button>();
        }
        else
        {
            forBoysGO.SetActive(false);
            notForBoysGO.SetActive(true);

            r1 = this.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Button>();
            r2 = this.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Button>();
            r3 = this.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Button>();
            r4 = this.transform.GetChild(1).GetChild(3).GetChild(0).GetComponent<Button>();

            l1 = this.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<Button>();
            l2 = this.transform.GetChild(1).GetChild(1).GetChild(2).GetComponent<Button>();
            l3 = this.transform.GetChild(1).GetChild(2).GetChild(2).GetComponent<Button>();
            l4 = this.transform.GetChild(1).GetChild(3).GetChild(2).GetComponent<Button>();
        }
    }

    public void LoadOne()
    {
        Text loadText;

        if (gameMenuOptions.forBoys)
        {
            loadText = this.transform.parent.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            saveNo = "B1";
        }
        else
        {
            loadText = this.transform.parent.transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            saveNo = "1";
        }

        if (loadText.text != "No Save File")
        {






            LoadPlayer();
        }
    }
    public void LoadTwo()
    {
        Text loadText;

        if (gameMenuOptions.forBoys)
        {
            loadText = this.transform.parent.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            saveNo = "B2";
        }
        else
        {
            loadText = this.transform.parent.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            saveNo = "2";
        }

        if (loadText.text != "No Save File")
        {






            LoadPlayer();
        }
    }
    public void LoadThree()
    {
        Text loadText;

        if (gameMenuOptions.forBoys)
        {
            loadText = this.transform.parent.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            saveNo = "B3";
        }
        else
        {
            loadText = this.transform.parent.transform.GetChild(1).transform.GetChild(1).transform.GetChild(2).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            saveNo = "3";
        }

        if (loadText.text != "No Save File")
        {






            LoadPlayer();
        }
    }
    public void LoadFour()
    {
        Text loadText;

        if (gameMenuOptions.forBoys)
        {
            loadText = this.transform.parent.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            saveNo = "B4";
        }
        else
        {
            loadText = this.transform.parent.transform.GetChild(1).transform.GetChild(1).transform.GetChild(3).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            saveNo = "4";
        }

        if (loadText.text != "No Save File")
        {






            LoadPlayer();
        }
        else
        {
            gameMenuOptions.MainMenu();
        }
    }

    void LoadPlayer()
    {
        if(saveGame == null)
        {
            saveGame = this.transform.parent.gameObject.transform.GetChild(0).gameObject.GetComponent<SaveGame>(); ;
        }
        if (string.IsNullOrEmpty(playerDetails.playerName))
        {
            playerDetails.playerName = GameObject.Find("Canvas").transform.GetChild(2).transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<Text>().text;
        }
        if (string.IsNullOrEmpty(saveGame.dir))
        {
            saveGame.SaveFolderCreate();
        }

        string fileName = "\\SaveFile-#" + saveNo + ".txt";
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject levels = GameObject.Find("Levels");
        Debug.Log("shipLocation " + shipLocation);

        //Read file
        details = File.ReadAllLines(saveGame.dir + fileName);

        //Load Details
        //Name
        playerDetails.playerName = details[0];
        gameMenuOptions.PlayGame();
        Debug.Log("LoadPlayer @ PlayGame");

        //Level
        if(shipLocation == null)
        {
            shipLocation = GameObject.Find("Ship").gameObject;
            Debug.Log("shipLocation " + shipLocation);
        }

        if (levels.transform.GetChild(0).gameObject.name == details[3])
        {
            levels.transform.GetChild(0).gameObject.SetActive(true);
            levels.transform.GetChild(1).gameObject.SetActive(false);
            levels.transform.GetChild(2).gameObject.SetActive(false);
            levels.transform.GetChild(3).gameObject.SetActive(false);
            levels.transform.GetChild(4).gameObject.SetActive(false);
            levels.transform.GetChild(5).gameObject.SetActive(false);
            shipLocation.SetActive(true);
        }
        if (levels.transform.GetChild(1).gameObject.name == details[3])
        {
            Debug.Log("Fix: 1 " + details[3]);
            levels.transform.GetChild(0).gameObject.SetActive(false);
            levels.transform.GetChild(1).gameObject.SetActive(true);
            levels.transform.GetChild(2).gameObject.SetActive(false);
            levels.transform.GetChild(3).gameObject.SetActive(false);
            levels.transform.GetChild(4).gameObject.SetActive(false);
            levels.transform.GetChild(5).gameObject.SetActive(false);
            shipLocation.SetActive(false);
        }
        if (levels.transform.GetChild(2).gameObject.name == details[3])
        {
            Debug.Log("Fix: 2 " + details[3]);            
            levels.transform.GetChild(0).gameObject.SetActive(false);
            levels.transform.GetChild(1).gameObject.SetActive(false);
            levels.transform.GetChild(2).gameObject.SetActive(true);
            levels.transform.GetChild(3).gameObject.SetActive(false);
            levels.transform.GetChild(4).gameObject.SetActive(false);
            levels.transform.GetChild(5).gameObject.SetActive(false);
            shipLocation.SetActive(false);
        }
        if (levels.transform.GetChild(3).gameObject.name == details[3])
        {
            Debug.Log("Fix: 3 " + details[3]);
            levels.transform.GetChild(0).gameObject.SetActive(false);
            levels.transform.GetChild(1).gameObject.SetActive(false);
            levels.transform.GetChild(2).gameObject.SetActive(false);
            levels.transform.GetChild(3).gameObject.SetActive(true);
            levels.transform.GetChild(4).gameObject.SetActive(false);
            levels.transform.GetChild(5).gameObject.SetActive(false);
            shipLocation.SetActive(false);
        }
        if (levels.transform.GetChild(4).gameObject.name == details[3])
        {
            Debug.Log("Fix: 4 " + details[3]);
            levels.transform.GetChild(0).gameObject.SetActive(false);
            levels.transform.GetChild(1).gameObject.SetActive(false);
            levels.transform.GetChild(2).gameObject.SetActive(false);
            levels.transform.GetChild(3).gameObject.SetActive(false);
            levels.transform.GetChild(4).gameObject.SetActive(true);
            levels.transform.GetChild(5).gameObject.SetActive(false);
            shipLocation.SetActive(false);
        }
        if (levels.transform.GetChild(5).gameObject.name == details[3])
        {
            Debug.Log("Fix: 5 " + details[3]);
            levels.transform.GetChild(0).gameObject.SetActive(false);
            levels.transform.GetChild(1).gameObject.SetActive(false);
            levels.transform.GetChild(2).gameObject.SetActive(false);
            levels.transform.GetChild(3).gameObject.SetActive(false);
            levels.transform.GetChild(4).gameObject.SetActive(false);
            levels.transform.GetChild(5).gameObject.SetActive(true);
            shipLocation.SetActive(false);
        }

        //Player Position
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        //X Position
        string numberString = details[1];
        float ppX = float.Parse(numberString);
        //Y Position
        numberString = details[2];
        float ppY = float.Parse(numberString);
        //Move Player
        //player.transform.position = new Vector3(ppX, ppY, 0);
        playerMovement.transform.position = new Vector3(ppX, ppY, 0);
        Debug.Log("Player X: " + player.transform.position.x + " Player Y: " + player.transform.position.y);






        Debug.Log("Fix: LoadPlayer");
    }
}
