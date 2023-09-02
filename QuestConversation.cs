using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestConversation : MonoBehaviour
{
    [Header("Scripts")]
    private TimeToRepair timeToRepair;
    private ResourceCollection resourceCollection;
    private ButtonActions buttonActions;
    private TypewriterUI typewriterUI;
    private ImageWindowSize imageWindowSize;
    private ShipRepairQuests shipRepairQuests;
    private PlayerDetails playerDetails;
    private QuestVariables questVariables;
    private RobotAnim robotAnim;
    private PlayerAnim playerAnim;

    [Header("Conversation Text")]

    public string artieText;
    public string playerText;

    [Header("Player Name")]
    public float nameY;
    private float nameX;

    [Header("Artie Name")]
    public float artieNameY;
    private float artieNameX;

    [Header("Conversation Point")]

    public int conversationPoint = 0;
    public bool conversationStopped = false;
    private bool checkActive = false;

    private float conversationTimer = 5.0f;
    private float imageBuffer = 5.0f;

    [Header("Time To Repair")]
    public int ihours;
    public int iminutes;
    public float fminutes;

    public string questName;
    private bool questCompleted = false;

    private GameObject click2Continue;
    void Start()
    {
        resourceCollection = GameObject.FindWithTag("Player").GetComponent<ResourceCollection>();
        playerAnim = GameObject.FindWithTag("Player").gameObject.transform.GetChild(0).gameObject.GetComponent<PlayerAnim>();
        robotAnim = GameObject.FindGameObjectWithTag("PlayerAI").GetComponent<RobotAnim>();

        timeToRepair = GameObject.Find("GameAssets").GetComponent<TimeToRepair>();
        buttonActions = GameObject.Find("GameAssets").GetComponent<ButtonActions>();
        questVariables = GameObject.Find("GameAssets").GetComponent<QuestVariables>();
        playerDetails = GameObject.Find("GameAssets").GetComponent<PlayerDetails>();
        typewriterUI = GetComponent<TypewriterUI>();
        imageWindowSize = GetComponent<ImageWindowSize>();
        shipRepairQuests = GameObject.FindGameObjectWithTag("Ship").GetComponent<ShipRepairQuests>();
        if (shipRepairQuests != null)
        {
            Debug.Log("Script (shipRepairQuests) Set in Start");
        }
        conversationPoint = 0;
        typewriterUI.StarterText();
        //QuestConversationText();
        typewriterUI.StartCoroutine("TypeArtieWriterText");
        //typewriterUI.StartCoroutine("TypePlayerWriterText");

        //nameY = imageWindowSize.playerName.transform.position.y;
        //artieNameY = imageWindowSize.artieName.transform.position.y;

        click2Continue = GameObject.Find("Click2Continue");
        click2Continue.SetActive(false);
    }

    void Update()
    {
        if(artieText == null)
        {
            QuestConversationText();
            Debug.Log("Text set in update!");
        }
        if (shipRepairQuests == null)
        {
            shipRepairQuests = GameObject.FindGameObjectWithTag("Ship").GetComponent<ShipRepairQuests>();

            if (shipRepairQuests != null)
            {
                Debug.Log("Script (shipRepairQuests) Set in Update");
            }
        }
        if(click2Continue == null)
        {
            click2Continue = GameObject.Find("Click2Continue");
            Debug.Log(click2Continue + " set in Update!");
            click2Continue.SetActive(false);
        }
        //Quest Given
        if (conversationPoint == 2)
        {
            if (conversationStopped)
            {
                if(conversationTimer > 0)
                {
                    conversationTimer -= Time.deltaTime;
                    Debug.Log("Quest Given: Window closes in " + conversationTimer);
                }
                else
                {
                    conversationTimer = 5.0f;
                    conversationPoint++;
                    buttonActions.ConversationButton();
                }
            }
        }
        //Quest Completion
        else if (conversationPoint == 3)
        {
            if (conversationStopped)
            {
                if (questCompleted)
                {
                    if (conversationTimer > 2.0f)
                    {
                        conversationTimer = 2.0f;
                    }

                    if (conversationTimer > 0)
                    {
                        conversationTimer -= Time.deltaTime;
                        Debug.Log("Quest Completed: Window closes in " + conversationTimer);
                    }
                    else
                    {
                        conversationTimer = 5.0f;

                        DroidRepairsShip();
                        conversationPoint++;
                        buttonActions.ConversationButton();
                        questCompleted = false;
                    }
                }
                else
                {
                    if (conversationTimer > 0)
                    {
                        conversationTimer -= Time.deltaTime;
                        Debug.Log("Quest Not Completed: Window closes in " + conversationTimer);
                    }
                    else
                    {
                        conversationTimer = 5.0f;
                        buttonActions.ConversationButton();
                    }
                }
            }
            /*
            else
            {
                if (conversationTimer > 0)
                {
                    conversationTimer -= Time.deltaTime;
                    Debug.Log("Quest Not Enough Resource: Window closes in " + conversationTimer);
                }
                else
                {
                    conversationTimer = 5.0f;
                    buttonActions.ConversationButton();
                }
            }
            */
        }
        //All others
        else if (conversationStopped)
        {
            if (Input.GetMouseButtonDown(0))
            {
                conversationPoint++;
                QuestConversationText();
            }
            if (Input.GetMouseButtonDown(1))
            {
                conversationPoint--;
                QuestConversationText();
            }
            click2Continue.SetActive(true);
        }
        else
        {
            click2Continue.SetActive(false);
        }

        nameY = imageWindowSize.playerName.transform.position.y;
        artieNameY = imageWindowSize.artieName.transform.position.y;

        if (!conversationStopped)
        {
            click2Continue.SetActive(false);
        }
    }

    public void QuestConversationText()
    {

        typewriterUI.StopCoroutine("TypeArtieWriterText");
        typewriterUI.StopCoroutine("TypePlayerWriterText");

        artieText = string.Empty;
        playerText = string.Empty;

        //Debug.Log("<color=red>Artie: </color><color=yellow>" + typewriterUI.artieWriter + "</color>. " + " <color=red>Player: </color><color=yellow>" + typewriterUI.playerWriter + "</color>");

        if (conversationPoint == 0)
        {
            artieText = "Wonderful, Mistress " + playerDetails.playerName + ". Another \"happy\" landing. \nAt least I’m still in one piece.";
            typewriterUI.artieWriter = artieText;
            imageWindowSize.artieWidth = 1046f;
            imageWindowSize.artieHeight = 106f;

            playerText = "Artie, if you can walk away from a landing, it's a good landing.";
            typewriterUI.playerWriter = playerText;
            imageWindowSize.playerWidth = 1048f;
            imageWindowSize.playerHeight = 55f;
        } 
        else if (conversationPoint == 1)
        {
            artieText = "Too bad it wasn’t an \"outstanding\" landing, Mistress " + playerDetails.playerName + ".";
            typewriterUI.artieWriter = artieText;
            imageWindowSize.artieWidth = 1050f;
            imageWindowSize.artieHeight = 60f;

            playerText = "Artie, what damage did the ship take?";
            typewriterUI.playerWriter = playerText;
            imageWindowSize.playerWidth = 660f;
            imageWindowSize.playerHeight = 60f;
        }
        //Power Generator
        else if (conversationPoint == 2)
        {
            artieText = "First and foremost, Mistress " + playerDetails.playerName + ". The ship appears to have no power. \nLikely the power generator was damaged. Without this system, I am unable to access any other system. \nIt will need to be repaired, first. \nTo repair this system it will require " + questVariables.Q01R1 + " Diamond, " + questVariables.Q01R2 + " Stibium, " + questVariables.Q01R3 + " Palladium and " + questVariables.Q01R4 + " Caesium.";
            typewriterUI.artieWriter = artieText;
            imageWindowSize.artieWidth = 1720f;
            imageWindowSize.artieHeight = 200f;

            playerText = "Okay, Artie. Watch over the ship. I’ll see what I can find.";
            typewriterUI.playerWriter = playerText;
            imageWindowSize.playerWidth = 950f;
            imageWindowSize.playerHeight = 55f;
        }
        else if (conversationPoint == 3)
        {
            resourceCollection.PowerGeneratorComplete();
            //If enough resources
            if (resourceCollection.enough == true)
            {
                QuestIsCompleted();
            } else
            {
                resourceCollection.PowerGeneratorIncomplete();
                QuestIsIncompleted();
            }
        }
        //Short-Range Scanners Quest
        else if (conversationPoint == 4)
        {
            resourceCollection.ShortRangeScannersComplete();

            //If enough resources
            if (resourceCollection.enough == true)
            {
                QuestIsCompleted();
            }
            else
            {
                resourceCollection.ShortRangeScannersIncomplete();
                QuestIsIncompleted();
            }
        }

        /*
        else if (conversationPoint == ) 
        resourceCollection.ShortRangeCommsComplete();

        else if (conversationPoint == ) 
        resourceCollection.TeleportComplete();

        else if (conversationPoint == ) 
        resourceCollection.ShieldComplete();

        else if (conversationPoint == ) 
        resourceCollection.HullComplete();

        else if (conversationPoint == ) 
        resourceCollection.LongRangeScannersComplete();

        else if (conversationPoint == ) 
        resourceCollection.EnginesComplete();

        else if (conversationPoint == ) 
        resourceCollection.ArtificialGravityComplete();

        else if (conversationPoint == ) 
        resourceCollection.LifeSupportComplete();

        else if (conversationPoint == ) 
        resourceCollection.LongRangeCommsComplete();

         */





        else if (conversationPoint == 300)
        {
            artieText = "aaaaaaaaaaaa";
            typewriterUI.artieWriter = artieText;
            imageWindowSize.artieWidth = 300f;
            imageWindowSize.artieHeight = 70f;

            playerText = "bbbbbbbbbbbbbb";
            typewriterUI.playerWriter = playerText;
            imageWindowSize.playerWidth = 350f;
            imageWindowSize.playerHeight = 80f;
        }
        else
        {
            artieText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            typewriterUI.artieWriter = artieText;
            imageWindowSize.artieWidth = 1800f;
            imageWindowSize.artieHeight = 250f;

            playerText = "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis...";
            typewriterUI.playerWriter = playerText;
            imageWindowSize.playerWidth = 1800f;
            imageWindowSize.playerHeight = 250f;
        }
        typewriterUI.StartCoroutine("TypeArtieWriterText");
    }
    public void ArtieNamePostion()
    {
        if(artieNameX == 0)
        {
            artieNameX = imageWindowSize.artieName.transform.position.x;
        }

        artieNameY = imageWindowSize.artieImage.gameObject.transform.position.y + imageWindowSize.artieHeight + imageBuffer;
        imageWindowSize.artieName.transform.position = new Vector3(artieNameX, artieNameY, 0);
    }
    public void PlayerNamePostion()
    {
        if(nameX == 0)
        {
            nameX = imageWindowSize.playerName.transform.position.x;
        }
        nameY = imageWindowSize.playerImage.gameObject.transform.position.y - imageWindowSize.playerHeight - imageBuffer;
        imageWindowSize.playerName.transform.position = new Vector3(nameX, nameY, 0);
    }

    public void PlayerTextActive()
    {
        if (!checkActive)
        {
            imageWindowSize.PlayerConverseWindow();
            checkActive = true;
        }
    }

    void QuestIsIncompleted()
    {
        artieText = "I'm sorry, Mistress " + playerDetails.playerName + ". But you need " + resourceCollection.QR1 + resourceCollection.QR2 + resourceCollection.QR3 + resourceCollection.QR4 + "more resources. Before I'm able to repair the " + questName + ".";
        typewriterUI.artieWriter = artieText;
        imageWindowSize.artieWidth = 1760f;
        imageWindowSize.artieHeight = 100f;

        playerText = "Okay. I'll head back out, then.";
        typewriterUI.playerWriter = playerText;
        imageWindowSize.playerWidth = 515f;
        imageWindowSize.playerHeight = 55f;
    }

    void QuestIsCompleted()
    {
        artieText = "Very well, Mistress. It will take approximately " + ihours + " hours and " + fminutes + " minutes to repair the " + questName + ".";
        typewriterUI.artieWriter = artieText;
        imageWindowSize.artieWidth = 1800f;
        imageWindowSize.artieHeight = 70f;

        playerText = "Fine, Artie. While you do that I’ll continue searching for more resources.";
        typewriterUI.playerWriter = playerText;
        imageWindowSize.playerWidth = 1300f;
        imageWindowSize.playerHeight = 80f;

        conversationTimer = 2.0f;
        questCompleted = true;
    }

    void DroidRepairsShip()
    {
        robotAnim.Repairing();
    }
}
