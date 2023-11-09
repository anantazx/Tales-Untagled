using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{

    [Header("Params")]
    [SerializeField] private float TypingSpeed = 0.04f;

    [Header("load Global JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private GameObject ContinueIcon;
    [SerializeField] private TextMeshProUGUI DialogueText;
    [SerializeField] private TextMeshProUGUI DisplayNameText;
    [SerializeField] private Animator PotraitAnimator;
    private Animator LayoutAnimator;
    
    [Header("Choice UI")]
    [SerializeField] private GameObject[] choices;

    [Header("Audio")]
    [SerializeField] private AudioClip[] dialogueTypingSoundClips;
    [Range(1, 5)]
    [SerializeField] private int frequencyLevel = 2;
    [Range(-3, 3)]
    [SerializeField] private float minPitch = 0.5f;
    [Range(-3, 3)]
    [SerializeField] private float maxPitch = 3f;
    [SerializeField] private bool stopAudioSource;
    [SerializeField] private bool makePredictable;

    private AudioSource audioSource;
    private TextMeshProUGUI[] ChoicesText;


    private Story CurrentStory;

    private bool CanContinueToNextLine = false;

    public bool DialogueIsPlaying { get; private set; }
    private Coroutine DisplayLineCourutine;
    
    private bool CanSkip = false;
    private bool SubmitSkip;

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string POTRAIT_TAG = "potrait";
    private const string LAYOUT_TAG = "layout";
    private DialogueVariables dialogueVariables;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found More Than One Dialogue Manager in the scene");
        }
        instance = this;

        dialogueVariables = new DialogueVariables(loadGlobalsJSON);

        audioSource = this.gameObject.AddComponent<AudioSource>();
    }

   public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        DialogueIsPlaying = false;
        DialoguePanel.SetActive(false);

        LayoutAnimator = DialoguePanel.GetComponent<Animator>();

        ChoicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            ChoicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SubmitSkip = true;
        }

        if (!DialogueIsPlaying)
        {
            return;
        }

        if ( CanContinueToNextLine && CurrentStory.currentChoices.Count == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }

    }

    public void EnterDialogueMode(TextAsset InkJSON)
    {
        CurrentStory = new Story(InkJSON.text);
        DialogueIsPlaying = true;
        DialoguePanel.SetActive(true);

        dialogueVariables.Startlistening(CurrentStory);


        DisplayNameText.text = "???";
        PotraitAnimator.Play("Default");
        LayoutAnimator.Play("right");   

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialogueVariables.Stoplistening(CurrentStory);

        DialogueIsPlaying = false;
        DialoguePanel.SetActive(false);
        DialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (CurrentStory.canContinue)
        {
            if (DisplayLineCourutine != null)
            {
                StopCoroutine(DisplayLineCourutine);
            }
            DisplayLineCourutine = StartCoroutine(DisplayLine(CurrentStory.Continue()));

            HandleTags(CurrentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string Line)
    {
        DialogueText.text = Line;
        DialogueText.maxVisibleCharacters = 0;

        ContinueIcon.SetActive(false);
        HideChoices();

        SubmitSkip = false;
        CanContinueToNextLine = false;

        bool IsAddingRichTextTag = false;

        StartCoroutine(Skiptext());

        foreach (char letter in Line.ToCharArray())
        {
            if (CanSkip && SubmitSkip)
            {
                SubmitSkip = false;
                DialogueText.maxVisibleCharacters = Line.Length;
                break;
            }

            if (letter == '<' || IsAddingRichTextTag)
            {
                IsAddingRichTextTag = true;
                if (letter == '>')
                {
                    IsAddingRichTextTag = false;
                }
            }
            else
            {
                PlayDialogueSound(DialogueText.maxVisibleCharacters, DialogueText.text[DialogueText.maxVisibleCharacters]);
                DialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(TypingSpeed);
            }
            
        }
        
        ContinueIcon.SetActive(true);
        DisplayChoices();
        CanContinueToNextLine = true;
        CanSkip = false;
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] SplitTag = tag.Split(':');
            if (SplitTag.Length != 2)
            {
                Debug.LogError("Tag Could Not Be Appropriately: " + tag);
            }

            string TagKey = SplitTag[0].Trim();
            string TagValue = SplitTag[1].Trim();

            switch (TagKey)
            {
                case SPEAKER_TAG:
                    DisplayNameText.text = TagValue;
                    break;
                case POTRAIT_TAG:
                    PotraitAnimator.Play(TagValue);
                    break;
                case LAYOUT_TAG:
                    LayoutAnimator.Play(TagValue);
                    break;
                default:
                    Debug.LogWarning("Tag Came In But Is Not Currently Being Handled: " + tag);
                    break;  
            }
        }
    }

    private void PlayDialogueSound(int currentDisplayedCharacterCount, char currentCharacter)
    {
        if (currentDisplayedCharacterCount % frequencyLevel == 0)
        {
            if (stopAudioSource)
            {
                audioSource.Stop();
            }
            AudioClip soundClip = null;
            // membuat untuk gampang mengecek audio dalam hashing
            if (makePredictable)
            {
                int hashCode = currentCharacter.GetHashCode();

                int predictableIndex = hashCode % dialogueTypingSoundClips.Length; 
                soundClip = dialogueTypingSoundClips[predictableIndex];

                int minPitchInt = (int)(minPitch * 100);
                int maxPitchInt = (int)(maxPitch * 100);
                int pitchRangeInt = maxPitchInt - minPitchInt;

                if (pitchRangeInt != 0 )
                {
                    int predictablePitchInt = (hashCode % pitchRangeInt) + minPitchInt;
                    float prediablePitch = predictablePitchInt / 100f;
                    audioSource.pitch = prediablePitch;
                }
                else
                {
                    audioSource.pitch = minPitch;
                }
            }
            // lainnya, maka akan mengacak audio
            else 
            {
                // sound clip
                int randomIndex = Random.Range(0, dialogueTypingSoundClips.Length);
                 soundClip = dialogueTypingSoundClips[randomIndex];
                // pitch sound
                audioSource.pitch = Random.Range(minPitch, maxPitch);
                //play audio
                
            }
            audioSource.PlayOneShot(soundClip);
        }
    }

    private void HideChoices()
    {
        foreach (GameObject ChoiceButton in choices)
        {
            ChoiceButton.SetActive(false);
        }
    }

    private void DisplayChoices()
    {
        List<Choice> CurrentChoices = CurrentStory.currentChoices;

        if (CurrentChoices.Count > choices.Length)
        {
            Debug.LogError("More Choices Were Given Than The UI Can Support. Number Of Choices Given :" + CurrentChoices.Count);
        }


        int index = 0;
        foreach (Choice choice in CurrentChoices)
        {
            choices[index].gameObject.SetActive(true);
            ChoicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());

    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoices(int ChoiceIndex)
    {
        if (CanContinueToNextLine)
        {
            CurrentStory.ChooseChoiceIndex(ChoiceIndex);

            Input.GetKeyDown(KeyCode.Space);
            ContinueStory();
        }
        
    }

    private IEnumerator Skiptext()
    {
        CanSkip = false;
        yield return new WaitForSeconds(0.05f);
        CanSkip = true;
    }


}
