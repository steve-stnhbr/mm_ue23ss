using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalLevelLoader : InteractableOnCollision
{
    [SerializeField] GameMenu gameMenu;
    bool once = true;
    protected override void WhileCollision(EnumActor actor)
    {
        if (once) {
            once = false;
            gameMenu.loadNextLevel();
        }
        
    }
}
