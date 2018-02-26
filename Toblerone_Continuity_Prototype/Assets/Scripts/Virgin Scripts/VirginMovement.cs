using System;
using UnityEngine;

public class VirginMovement : MonoBehaviour {

    public MasterManager masterManager;

    public void OnHeroEntersFrame(int heroDirection)
    {
        if (canEscape(heroDirection))
        {
            escape(heroDirection);
        }
        else
        {
            if (isInPuzzleRoom())
            {
                if (isPuzzleSolved())
                {
                    levelCompleted();
                }
                else
                {
                    killHero();
                }
            }
            else
            {
                killHero();
            }
        }
    }

    private void levelCompleted()
    {
        masterManager.LevelCompleted();
    }

    private bool isPuzzleSolved()
    {
        return masterManager.puzzleManager.IsPuzzleSolved;
    }

    private void killHero()
    {
        masterManager.KillHero();
    }

    private bool isInPuzzleRoom()
    {
        bool result = false;
        
        if (masterManager.framesManager.PuzzleFrame.row == masterManager.framesManager.VirginFrame.row)
        {
            if (masterManager.framesManager.PuzzleFrame.col == masterManager.framesManager.VirginFrame.col)
            {
                result = true;
            }
        }

        return result;
    }

    private void escape(int heroDirection)
    {
        masterManager.framesManager.VirginEscapesToNextFrame(heroDirection);
    }

    private bool canEscape(int heroDirection)
    {
        return masterManager.framesManager.CanVirginEscape(heroDirection);
    }
}
