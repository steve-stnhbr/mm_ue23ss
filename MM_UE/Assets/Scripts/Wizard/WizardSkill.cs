using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WizardSkill : MonoBehaviour
{
    [Header("UI elements")]
    public Sprite UISprite;
    public Sprite UISpriteSelected;
    public Color UIColor;

    public abstract string skillName { get; }

    public abstract void OnExecute(GameObject wizard);
}
