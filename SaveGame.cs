using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEditor;

public class SaveGame : MonoBehaviour
{
    [Header("Scripts")]
    private GameMenuOptions gameMenuOptions;
    private PlayerDetails playerDetails;

    [Header("Reset Buttons")]
    private Button r1;
    private Button r2;
    private Button r3;
    private Button r4;

    [Header("")]
    public string dir;
    //C:\Users\cryst\AppData\LocalLow\Crystal Woods\Stranded On Planet X
    private bool forBoys = false;
    private GameObject forBoysGO;
    private GameObject notForBoysGO;
    [Header("Save/Load Text")]
    public string[] savedInfo = new string[6];
    public string contents;
    private Text text;
    private Text loadText;
    private string saveText;
    private string saveNo;
    [Header("Saved Data")]
    private Vector3 playerPosition;
    private string fileName;

    void Start()
    {       
        forBoysGO = this.transform.GetChild(0).gameObject;
        notForBoysGO = this.transform.GetChild(1).gameObject;

        gameMenuOptions = this.transform.parent.GetComponent<GameMenuOptions>();
        forBoys = gameMenuOptions.forBoys;

        playerDetails = GameObject.Find("GameAssets").GetComponent<PlayerDetails>();

        if (forBoys)
        {
            notForBoysGO.SetActive(false);
            forBoysGO.SetActive(true);

            r1 = this.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>();
            r2 = this.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>();
            r3 = this.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Button>();
            r4 = this.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<Button>();
        }
        else
        {
            forBoysGO.SetActive(false);
            notForBoysGO.SetActive(true);

            r1 = this.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Button>();
            r2 = this.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Button>();
            r3 = this.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Button>();
            r4 = this.transform.GetChild(1).GetChild(3).GetChild(0).GetComponent<Button>();
        }
    }
    void Update()
    {        

    }
    
    //Save
    public void SaveOne()
    {
        SaveFileOne();
        SaveText();
    }
    public void SaveTwo()
    {
        SaveFileTwo();
        SaveText();
    }
    public void SaveThree()
    {
        SaveFileThree();
        SaveText();
    }
    public void SaveFour()
    {
        SaveFileFour();
        SaveText();
    }
    //
    void SaveFileCreate() 
    {
        fileName = "\\SaveFile-#"+ saveNo+".txt";

        File.WriteAllText(dir + fileName, contents);
        savedInfo = File.ReadAllLines(dir + fileName);
    }
    public void SaveFolderCreate()
    {
        dir = Application.persistentDataPath + "/SaveFiles";

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }
    void SaveText()
    {
        SaveFolderCreate();

        GameObject levels = GameObject.Find("Levels");
        string levelName = "";
        //ResourceCollection resourceCollection = GameObject.FindWithTag("Player").GetComponent<ResourceCollection>();
        RepairShip repairShip = GameObject.FindObjectOfType<RepairShip>();
        QuestConversation questConversation = GameObject.Find("Canvas").transform.GetChild(2).gameObject.GetComponent<QuestConversation>(); ;
        float percentage = 0;
        //float percentageIncrease = 2.628947368421053f;
        float percentageIncrease = 2.626315789473684f;

        playerPosition = GameObject.FindWithTag("Player").transform.position;
        percentage = percentageIncrease * questConversation.conversationPoint;

        //Active Level
        if (levels.transform.GetChild(0).gameObject.activeSelf) 
        {
            levelName = levels.transform.GetChild(0).gameObject.name;
        }
        if (levels.transform.GetChild(1).gameObject.activeSelf) 
        {
            levelName = levels.transform.GetChild(1).gameObject.name;
        }
        if (levels.transform.GetChild(2).gameObject.activeSelf) 
        {
            levelName = levels.transform.GetChild(2).gameObject.name;
        }
        if (levels.transform.GetChild(3).gameObject.activeSelf) 
        {
            levelName = levels.transform.GetChild(3).gameObject.name;
        }
        if (levels.transform.GetChild(4).gameObject.activeSelf) 
        {
            levelName = levels.transform.GetChild(4).gameObject.name;
        }
        if (levels.transform.GetChild(5).gameObject.activeSelf) 
        {
            levelName = levels.transform.GetChild(5).gameObject.name;
        }

        if(questConversation.conversationPoint >= 50)
        {
            percentage = 100.0f;
        }
        else if(questConversation.conversationPoint > 38)
        {
            percentage = 99.9f;
        }

        text.text = "<size=30>" + playerDetails.playerName + "</size> - <size=27>" + levelName + "</size>\n<size=24>Story Completion: " + percentage.ToString("F1") + "%\n</size><size=20> " + System.DateTime.Now.ToString("dddd dd/MM/yyyy") + " " + System.DateTime.Now.ToString("hh:mm:ss tt") + "</size>";

        loadText.text = text.text;
        PlayerPrefs.SetString(saveNo, text.text);

        // +"\n"+
        Debug.Log("Place Save Data! "+saveNo);
        //SaveData
        contents = playerDetails.playerName + "\n" + playerPosition.x + "\n" + playerPosition.y + "\n"+ levelName+"\n";

        SaveFileCreate();
    }
    void SaveFileOne()
    {
        if (gameMenuOptions.forBoys)
        {
            text = this.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            loadText = this.transform.parent.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            saveNo = "B1";
        }
        else
        {
            text = this.transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            loadText = this.transform.parent.transform.GetChild(1).transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            saveNo = "1";
        }
    }
    void SaveFileTwo() 
    {
        if (gameMenuOptions.forBoys)
        {
            text = this.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            loadText = this.transform.parent.transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            saveNo = "B2";
        }
        else
        {
            text = this.transform.GetChild(1).transform.GetChild(1).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            loadText = this.transform.parent.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            saveNo = "2";
        }
    }
    void SaveFileThree() 
    {
        if (gameMenuOptions.forBoys)
        {
            text = this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            loadText = this.transform.parent.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            saveNo = "B3";
        }
        else
        {
            text = this.transform.GetChild(1).transform.GetChild(2).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            loadText = this.transform.parent.transform.GetChild(1).transform.GetChild(1).transform.GetChild(2).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            saveNo = "3";
        }
    }
    void SaveFileFour() 
    {
        if (gameMenuOptions.forBoys)
        {
            text = this.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            loadText = this.transform.parent.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            saveNo = "B4";
        }
        else
        {
            text = this.transform.GetChild(1).transform.GetChild(3).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            loadText = this.transform.parent.transform.GetChild(1).transform.GetChild(1).transform.GetChild(3).transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
            saveNo = "4";
        }
    }
    //Reset
    public void ResetOne()
    {
        SaveFileOne();
        ResetText();
    }
    public void ResetTwo()
    {
        SaveFileTwo();
        ResetText();
    }
    public void ResetThree()
    {
        SaveFileThree();
        ResetText();
    }
    public void ResetFour()
    {
        SaveFileFour();
        ResetText();
    }

    void ResetText()
    {
        if (string.IsNullOrEmpty(dir))
        {
            dir = Application.persistentDataPath + "/SaveFiles";
        }
        fileName = "\\SaveFile-#" + saveNo + ".txt";

        text.text = "No Save File"; 
        loadText.text = text.text;

        text.fontSize = 28; 
        loadText.fontSize = 28;

        //FileUtil.DeleteFileOrDirectory(dir + fileName);
        Directory.Delete(dir + fileName);
        PlayerPrefs.DeleteKey(saveNo);

        if (!File.Exists(dir + "\\SaveFile-#1.txt") && !File.Exists(dir + "\\SaveFile-#2.txt") && !File.Exists(dir + "\\SaveFile-#3.txt") && !File.Exists(dir + "\\SaveFile-#4.txt") && !File.Exists(dir + "\\SaveFile-#B1.txt") && !File.Exists(dir + "\\SaveFile-#B2.txt") && !File.Exists(dir + "\\SaveFile-#B3.txt") && !File.Exists(dir + "\\SaveFile-#B4.txt"))
        {
            Directory.Delete(dir);
            Debug.Log("Directory Deleted!");
        }
    }

    public void ResetAll()
    {
        ResetOne();
        ResetTwo();
        ResetThree();
        ResetFour();
    }
}
