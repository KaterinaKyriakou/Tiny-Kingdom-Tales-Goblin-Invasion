using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header ("Params")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI DiplayNameText;
    private static DialogueManager instance;
    private Story currentStory;
    private bool dialogueIsPlaying;
    private Coroutine displayLineCoroutine;
    private bool canContinueToNextLine = true;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private const string SPEAKER_TAG ="speaker";
    private const string PORTRAIT_TAG ="portrait";

    bool SelectedChoice = false;


    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one Dialogue Manager");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }
    /* -------------- Start ---------------- */
    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        //choices
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices )
        {
            choicesText[index] = choice.GetComponentInChildren <TextMeshProUGUI>();
            index++;
        }
    }
    /* -------------- Update ---------------- */
    private void Update()
    {
        
        if(!dialogueIsPlaying){
            return;
        }
        if (string.IsNullOrEmpty(dialogueText.text) )
        {
            ContinueStory();
        }
        if (canContinueToNextLine  && (Input.GetKeyDown(KeyCode.E) || SelectedChoice == true)){
            ContinueStory();
        }
    }
    /* -------------- EnterDialogue ---------------- */
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        dialoguePanel.SetActive(true);
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
  
    }
    /* -------------- ExitDialogue ---------------- */
    public void ExitDialoguMode(){
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }
    /* -------------- ContinueStory ---------------- */
    private void ContinueStory(){
        SelectedChoice = false;
        if(currentStory.canContinue)
        {
    
        //    dialogueText.text = currentStory.Continue();
        if(displayLineCoroutine != null){
            StopCoroutine(displayLineCoroutine);
        }
        displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
           //display choices (if any)
        //    DisplayChoices();
           HandleTags(currentStory.currentTags);
        }else{
            ExitDialoguMode();
            
        }
    }
    /* -------------- Display line---------------- */
    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text ="";
        HideChoices();
        canContinueToNextLine = false;
        foreach(char letter in line.ToCharArray())
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                dialogueText.text = line;
                break;
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        DisplayChoices();
        canContinueToNextLine = true;
    }
    /* -------------- HideChoices ---------------- */
    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }
    /* -------------- DisplayChoices ---------------- */
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if( currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support");
        } 

        int index = 0 ;

        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;     
        }
        //going through the choices
        for(int i = index; i<choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelecFirstChoice());
    }
    /* -------------- SelectFirstChoise ---------------- */
    private IEnumerator SelecFirstChoice()
    {
        
        //Event System, clear first -> wait (1frame) before set the selected object
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
       
    }
    /* -------------- MakeChoice ---------------- */
    public void MakeChoice(int choiceIndex)
    {
        if(canContinueToNextLine)
        {
            SelectedChoice= true;
            currentStory.ChooseChoiceIndex(choiceIndex);
        }
    }
    /* -------------- HandleTags ---------------- */
    private void HandleTags(List<string> currentTags)
    {
        // loop through each tag and handle it accordingly
        foreach (string tag in currentTags) 
        {
            // parse the tag
            // string[] splitTag = tag.Split(':');
            string[] splitTag = tag.Split(new char[] { ':' }, 2); // Split by the first colon only
            if (splitTag.Length != 2) 
            {
                Debug.LogError("Tag could not be appropriately parsed: " + splitTag.Length);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            
            // handle the tag
            DiplayNameText.text = tagValue;
            // switch (tagKey) 
            // {
            //     case SPEAKER_TAG:
            //         diplayNameText.text = tag;
            //         break;
            //     case PORTRAIT_TAG: 
            //         break;
            //     default:
            //         Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
            //         break;
            // }
        }
    }
}
