using System.Collections.Generic;
using UnityEngine;

public class NpcDialogue : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;
    private bool playerHit;
    public DialogueSettings dialogue;
    private Player player;

    // Lista de strings das sentenças da lista "Dialogues" do "DialogueSettings"
    private List<string> sentences = new List<string>();
    private List<string> actorName = new List<string>();
    private List<Sprite> actorSprite = new List<Sprite>();

    void Start()
    {
        GetNPCInfo();
        player = FindAnyObjectByType<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerHit)
        {
            //Chamar "Speech" do DialogueController, passando as strings das sentenças
            DialogueController.instance.Speech(sentences.ToArray(), actorName.ToArray(), actorSprite.ToArray());
        }
    }

    void GetNPCInfo()
    {
        //Para cada sentença dentro da lista "dialogues" do DialogueSettings
        for (int i = 0; i < dialogue.dialogues.Count; i++)
        {
            switch (DialogueController.instance.language)
            {
                case DialogueController.idiomas.pt:
                    //Obter a string da sentença em português
                    sentences.Add(dialogue.dialogues[i].sentence.portuguese);
                    break;
                case DialogueController.idiomas.eng:
                    //Obter a string da sentença em inglês
                    sentences.Add(dialogue.dialogues[i].sentence.english);
                    break;
                case DialogueController.idiomas.span:
                    //Obter a string da sentença em espanhol
                    sentences.Add(dialogue.dialogues[i].sentence.spanish);
                    break;
            }
            actorName.Add(dialogue.dialogues[i].actorName);
            actorSprite.Add(dialogue.dialogues[i].profile);
        }
    }

    void FixedUpdate()
    {
        ShowDialogue();
    }

    void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);
        if (hit != null)
        {
            playerHit = true;
        }
        else
        {
            playerHit = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}
