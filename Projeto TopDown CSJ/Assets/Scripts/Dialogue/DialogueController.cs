using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [System.Serializable]
    public enum idiomas
    {
        pt,
        eng,
        span
    }
    public idiomas language;

    [Header("Components")]
    public GameObject dialogueObj; //janela de diálogo
    public TMP_Text actorNameText; //nome do NPC
    public Image profileSprite; //sprite do NPC
    public TMP_Text speechText; //texto do diálogo

    [Header("Settings")]
    public float dialogueSpeed; //velocidade do diálogo

    //Variáveis de controle
    public bool isShowing; //visibilidade
    private int index; //índice dos diálogos
    private string[] sentences; //falas do NPC
    private string[] currentActorName; 
    private Sprite[] currentActorProfile;

    private Player player;
    private PlayerAnim playerAnim;
    public static DialogueController instance;

    // É chamado antes de todos os Start() na hierarquia de execução de scripts
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = FindAnyObjectByType<Player>();
        playerAnim = player.GetComponent<PlayerAnim>();
    }

    //Fala Inicial Do NPC
    public void Speech(string[] txt, string[] actorName, Sprite[] actorProfile)
    {
        if (!isShowing)
        {
            player.IsRunning = false;
            player.Speed = 0;
            playerAnim.OnDialogue = true;
            dialogueObj.SetActive(true);
            player.isPaused = true;
            sentences = txt;
            currentActorName = actorName;
            currentActorProfile = actorProfile;
            profileSprite.sprite = currentActorProfile[index];
            actorNameText.text = currentActorName[index];
            StartCoroutine(TypeSentence());
            isShowing = true;
        }
    }

    //Escreve Com Delay
    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }

    // Escreve Próximas frases (pular)
    public void NextSentence()
    {
        if (speechText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                profileSprite.sprite = currentActorProfile[index];
                actorNameText.text = currentActorName[index];
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else
            {
                speechText.text = "";
                actorNameText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                player.isPaused = false;
                playerAnim.OnDialogue = false;
                sentences = null;
                isShowing = false;
            }
        }
    }


}
