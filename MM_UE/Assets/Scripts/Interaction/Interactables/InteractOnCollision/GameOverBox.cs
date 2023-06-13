using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverBox : InteractableOnCollision
{
    [SerializeField] GameMenu gameMenu;

    protected override void WhileCollision(EnumActor actor)
    {
        gameMenu.openGameOverMenu();
    }
}
