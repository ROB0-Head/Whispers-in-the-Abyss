using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text dialogText;
    public Image speakerImage;
    public GameObject dialogPanel;
    public GameObject optionsPanel;
    public Button optionButtonPrefab;

    private Dialog.DialogNode currentDialogNode;

    public void StartDialog(Dialog dialog)
    {
        dialogPanel.SetActive(true);
        optionsPanel.SetActive(false);
        DisplayDialogNode(dialog.dialogNodes[0]);
    }

    void DisplayDialogNode(Dialog.DialogNode node)
    {
        currentDialogNode = node;
        dialogText.text = node.dialogText;
        speakerImage.sprite = node.speakerImage;

        if (node.options.Count > 0)
        {
            DisplayOptions(node.options);
        }
        else
        {
            optionsPanel.SetActive(false);
        }
    }

    void DisplayOptions(List<Dialog.DialogOption> options)
    {
        optionsPanel.SetActive(true);

        // Clear existing buttons
        foreach (Transform child in optionsPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var option in options)
        {
            Button optionButton = Instantiate(optionButtonPrefab, optionsPanel.transform);
            optionButton.GetComponentInChildren<Text>().text = option.optionText;
            optionButton.onClick.AddListener(() => OnOptionSelected(option.nextDialog));
        }
    }

    void OnOptionSelected(Dialog nextDialog)
    {
        if (nextDialog != null)
        {
            DisplayDialogNode(nextDialog.dialogNodes[0]);
        }
        else
        {
            dialogPanel.SetActive(false);
            optionsPanel.SetActive(false);
        }
    }
}