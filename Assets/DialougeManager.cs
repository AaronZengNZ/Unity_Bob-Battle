using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialougeManager : MonoBehaviour
{

    private Queue<string> sentences;
    private Queue<string> names;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialougeText;
    public Animator animator;
    public AudioSource blipSfx;
    public float blipEveryNumberOfChar = 3f;
    public BossFollow bossScript;
    public GameObject bossImg;
    public GameObject playerImg;


    bool isRunningDialogue = false;

    string currentEvent;

    float typeSpeed = 0.02f;

    float currentChar = 0;

    float pitch;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
        bossImg.SetActive(false);
        playerImg.SetActive(false);
    }

    public void StartDialogue(Dialouge dialogue, float speed, float pitch)
    {
        animator.SetBool("IsOpen", true);
        typeSpeed = 0.02f / speed;
        this.pitch = pitch;
        currentEvent = dialogue.specialEvent;
        if (dialogue.names[0] == "bob" || dialogue.names[0] == "???")
        {
            playerImg.SetActive(false);
            bossImg.SetActive(true);
        }
        else
        {
            bossImg.SetActive(false);
            playerImg.SetActive(true);
        }

        sentences.Clear();
        names.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }
        StartCoroutine(WaitAndStart());
    }


    IEnumerator WaitAndStart()
    {
        dialougeText.text = "";
        yield return new WaitForSeconds(1f);
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        blipSfx.pitch = pitch;
        if (isRunningDialogue == false)
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();
            string name = names.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence, name));
        }
    }

    //make addition function
    //do something

    IEnumerator TypeSentence(string sentence, string name)
    {
        isRunningDialogue = true;
        dialougeText.text = "";
        nameText.text = name;
        if (name == "bob" || name == "???")
        {
            playerImg.SetActive(false);
            bossImg.SetActive(true);
        }
        else
        {
            bossImg.SetActive(false);
            playerImg.SetActive(true);
        }
        foreach (char letter in sentence.ToCharArray())
        {
            string character = letter.ToString();
            if (character != " ")
            {
                currentChar += 1;
            }
            if (currentChar == blipEveryNumberOfChar)
            {
                currentChar = 0;
                blipSfx.Play();
            }
            dialougeText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        isRunningDialogue = false;
    }

    void EndDialogue()
    {
        if(currentEvent == "start")
        {
            bossScript.Phase1();
        }
        if(currentEvent == "two")
        {
            bossScript.Phase2();
        }
        if(currentEvent == "three")
        {
            bossScript.Phase3();
        }
        if (currentEvent == "four")
        {
            bossScript.Phase4();
        }
        if (currentEvent == "five")
        {
            bossScript.Phase5();
        }
        playerImg.SetActive(false);
        bossImg.SetActive(false);
        animator.SetBool("IsOpen", false);
    }

}
