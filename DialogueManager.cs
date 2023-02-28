using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// reusable class to manage dialogue
// many settings and options to control speed of text, whether or not text should play after previous, or if it should wait for an event
public class DialogueManager : MonoBehaviour
{
    [System.Serializable]
    private class Dialogue
    {
        [TextArea(15, 20)]
        public string text;
        public bool playAfterPrevious;
    }

    [SerializeField] private List<Dialogue> dialogues;
    [SerializeField] private float charactersPerSecond = 10;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private GameObject dialogueBox;
    private int current = -1;
    private bool playingDialogue;
    private float t;

    void Start()
    {
        if (dialogues.Count > 0 && dialogues[0].playAfterPrevious) TriggerDialogue(0);
    }

    public void TriggerDialogue(int index)
    {
        if (index != current + 1) return;
        if (dialogueBox.activeSelf) 
        {
            dialogues[index].playAfterPrevious = true;
        }
        else
        {
            current = index;
            StartCoroutine(PlayDialogue());
        }
    }

    private void TriggerDialogueFromClick(int index)
    {
        if (index != current + 1) return;
        current = index;
        StartCoroutine(PlayDialogue());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnClick();
        }
    }

    private void OnClick()
    {
        if (playingDialogue)
        {
            playingDialogue = false;
        }
        else if (current < dialogues.Count - 1 && dialogues[current + 1].playAfterPrevious)
        {
            TriggerDialogueFromClick(current + 1);
        }
        else
        {
            dialogueBox.SetActive(false);
        }
    }

    private IEnumerator PlayDialogue()
    {
        transform.Find("Garbriel").Find("TalkingSound").GetComponent<AudioSource>().Play();
        dialogueBox.SetActive(true);
        playingDialogue = true;
        string text = dialogues[current].text;
        float duration = text.Length / charactersPerSecond;
        t = 0;
        while (t < duration)
        {
            if (!playingDialogue) break;
            string sub = text.Substring(0, Mathf.RoundToInt(text.Length * t / duration));
            textMesh.text = sub;
            t += Time.deltaTime;
            yield return null;
        }
        textMesh.text = text;
        playingDialogue = false;
    }
}
