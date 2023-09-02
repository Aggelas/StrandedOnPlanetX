// Script for having a typewriter effect for UI
// Prepared by Nick Hwang (https://www.youtube.com/nickhwang)
// Want to get creative? Try a Unicode leading character(https://unicode-table.com/en/blocks/block-elements/)
// Copy Paste from page into Inpector

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class TypewriterUI : MonoBehaviour
{
	private QuestConversation questConversation;
    private ImageWindowSize imageWindowSize;
    private PlayerAnim playerAnim;
    private RobotAnim robotAnim;
	private ResourceCollection resourceCollection;

	private string leadingChar = "|";
	[SerializeField] private bool leadingCharBeforeDelay = false;

	[Header("Player")]
	public string playerWriter;
	private Text playerText;
	private float playerDelayBeforeStart = 0.5f;
	private float playerTimeBtwChars = 0.065f;
	//public bool playerFinis = false;

	[Header("Artie (RT-59)")]
	public string artieWriter;
	private Text artieText;
	private float artieDelayBeforeStart = 0f;
	private float artieTimeBtwChars = 0.05f;
    public bool activeCheck = false;

    void Awake()
	{
        playerAnim = GameObject.FindWithTag("Player").gameObject.transform.GetChild(0).gameObject.GetComponent<PlayerAnim>();
        robotAnim = GameObject.FindGameObjectWithTag("PlayerAI").GetComponent<RobotAnim>();
		resourceCollection = GameObject.FindWithTag("Player").GetComponent<ResourceCollection>();

		questConversation = GetComponent<QuestConversation>();
        imageWindowSize = GetComponent<ImageWindowSize>();

		artieText = gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
		playerText = gameObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Text>();
    }

    IEnumerator TypeArtieWriterText()
	{
		//Debug.Log("<color=red>Artie: </color><color=yellow>" + artieWriter + "</color>. " + " <color=red>Player: </color><color=yellow>" + playerWriter + "</color>");

		if (questConversation.conversationPoint != 2 || questConversation.conversationPoint != 3)
		{
			robotAnim.StartConversation();
		}
		else
		{
			robotAnim.TalkingQuest();
        }

		questConversation.conversationStopped = false;
        questConversation.ArtieNamePostion();

		if (activeCheck == false)
		{
			imageWindowSize.PlayerConverseWindow();
            activeCheck = true;
        }
		imageWindowSize.SetArtieImageSize();

        artieText.text = leadingCharBeforeDelay ? leadingChar : "";

		yield return new WaitForSeconds(artieDelayBeforeStart);

		foreach (char c in artieWriter)
		{
			if (artieText.text.Length > 0)
			{
				artieText.text = artieText.text.Substring(0, artieText.text.Length - leadingChar.Length);
			}
			artieText.text += c;
			artieText.text += leadingChar;
			yield return new WaitForSeconds(artieTimeBtwChars);
		}

		if(leadingChar != "")
        {
			artieText.text = artieText.text.Substring(0, artieText.text.Length - leadingChar.Length);
		}

        robotAnim.StopConversation();
        
        StartCoroutine("TypePlayerWriterText");
    }

	IEnumerator TypePlayerWriterText()
	{
		//robotAnim.StopConversation();

		playerAnim.TalkToArtie();

		questConversation.PlayerTextActive();

		//Debug.Log("<color=red>Artie: </color>" + artieWriter + ". " + " <color=red>Player: </color>" + playerWriter);

		playerText.text = leadingCharBeforeDelay ? leadingChar : "";

		questConversation.PlayerNamePostion();

		imageWindowSize.SetPlayerImageSize();

		yield return new WaitForSeconds(playerDelayBeforeStart);

		foreach (char c in playerWriter)
		{
			if (playerText.text.Length > 0)
			{
				playerText.text = playerText.text.Substring(0, playerText.text.Length - leadingChar.Length);
			}
			playerText.text += c;
			playerText.text += leadingChar;
			yield return new WaitForSeconds(playerTimeBtwChars);
		}

		if (leadingChar != "")
		{
			playerText.text = playerText.text.Substring(0, playerText.text.Length - leadingChar.Length);
		}
		//Stop Animations
		if (playerText.text.Length > 0)
		{
			playerAnim.NotTalkToArtie();

			if (questConversation.conversationPoint == 2 || questConversation.conversationPoint == 3)
			{
				robotAnim.NotTalkingPlayer();
			}
		}
		//Start new "slides"
		questConversation.conversationStopped = true;
	}

	public void NewArtieText()
    {
		artieText.text = "\"At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga.Et harum quidem rerum facilis est et expedita distinctio.Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae.Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.\"";
        //artieText.text = questConversation.artieText;
        artieWriter = artieText.text;
		StartCoroutine("TypeArtieWriterText");
	}
	public void NewPlayerText()
    {
		playerText.text = "\"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\"";
		//playerText.text = questConversation.playerText;
		playerWriter = playerText.text;
		StartCoroutine("TypePlayerWriterText");
	}

	public void StarterText()
	{
		/*
        if (artieText != null)
        {
            artieWriter = artieText.text;
            artieText.text = "";
			Debug.Log(artieWriter);
        }
        if (playerText != null)
        {
            playerWriter = playerText.text;
            playerText.text = "";
			Debug.Log(playerWriter);
        }
		*/
		if (artieText != null || playerText != null)
		{
			questConversation.QuestConversationText();
		}
    }
}
