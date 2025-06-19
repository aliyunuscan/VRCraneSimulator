using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public Button continueButton;        

    [TextArea(2, 5)]
    public string[] dialogueLines;

    private int currentLine = 0;

    void Start()
    {
        ShowLine();
        continueButton.onClick.AddListener(ContinueDialogue);
    }

    void ShowLine()
    {
        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
        }
        else
        {
            dialogueText.text = "";
            continueButton.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void ContinueDialogue()
    {
        currentLine++;
        ShowLine();
    }
}
