using System.Collections.Generic;
using DialogSystem;
using Navigation;
using Settings;
using TMPro;
using UI.Screens;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : DefaultScreen
{
    public TextMeshProUGUI dialogText;
    public Image speakerImage;
    public GameObject dialogPanel;
    public GameObject optionsPanel;
    public Button optionButtonPrefab;

    private Dialog.DialogNode currentDialogNode;

    private void Awake()
    {
        dialogPanel.SetActive(true);
        optionsPanel.SetActive(false);
        DisplayDialogNode(SettingsProvider.Get<Dialog>().dialogNodes);
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

        foreach (Transform child in optionsPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var option in options)
        {
            Button optionButton = Instantiate(optionButtonPrefab, optionsPanel.transform);
            optionButton.GetComponentInChildren<TextMeshProUGUI>().text = option.optionText;
            optionButton.onClick.AddListener(() => OnOptionSelected(option.nextDialog));
        }
    }

    void OnOptionSelected(Dialog nextDialog)
    {
        if (nextDialog != null)
        {
            DisplayDialogNode(nextDialog.dialogNodes);
        }
        else
        {
            Back();
        }
    }

    public void Back()
    {
        NavigationController.Instance.ScreenTransition<CityScreen>();
    }
}