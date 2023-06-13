using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] GameMenu gameMenu;

    SliderInt volumeSlider;
    Label volumeLabel;

    private void OnEnable()
    {

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button buttonResume = root.Q<Button>("ButtonResume");
        Button buttonLevelSelector = root.Q<Button>("ButtonLevelSelector");
        Button buttonQuit = root.Q<Button>("ButtonQuit");
        volumeSlider = root.Q<SliderInt>("VolumeSlider");
        volumeLabel = root.Q<Label>("VolumeLabel");

        buttonResume.clicked += () => gameMenu.resumeGame();
        buttonLevelSelector.clicked += () => gameMenu.openLevelSelector();
        buttonQuit.clicked += () => Application.Quit();
        buttonQuit.clicked += () => Debug.Log("Quit does not work in the editor");

        volumeSlider.value = (int) (AudioListener.volume * 100f);
        volumeLabel.text = (int) (AudioListener.volume * 100f) + "%";
    }

    private void Update()
    {
        AudioListener.volume = volumeSlider.value / 100f;
        volumeLabel.text = volumeSlider.value + "%";
    }
}
