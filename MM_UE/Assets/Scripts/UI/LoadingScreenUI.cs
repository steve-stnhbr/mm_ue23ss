using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadingScreenUI : MonoBehaviour
{
    [SerializeField] GameMenu gameMenu;
    ProgressBar loadingProgressBar;
    Label loadingLabel;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        loadingProgressBar = root.Q<ProgressBar>("LoadingProgressBar");
        loadingLabel = root.Q<Label>("LevelTitle");
    }

    public void updateLevelTitle(string title)
    {
        loadingLabel.text = "Loading " + title;
    }

    public void updateProgress(float progress)
    {
        loadingProgressBar.value = Mathf.Clamp01(progress);
        loadingProgressBar.title = (progress * 100f) + " %";
    }
}
