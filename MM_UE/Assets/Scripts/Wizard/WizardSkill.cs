using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WizardSkill : MonoBehaviour
{
    [Header("UI elements")]
    public Sprite UISprite;
    public Color UIColor;

    public abstract void OnExecute(GameObject wizard);
}
