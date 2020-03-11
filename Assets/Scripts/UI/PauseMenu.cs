﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : BaseMenu
{
    private bool pauseActive = false;
    private bool menuActive = false;
    private GameSettings settings;
    private LevelRunner levelRunner;

    private QuitMenu quitMenu;

    private void Start()
    {
        quitMenu = FindObjectOfType<QuitMenu>();
        gameObject.transform.localScale = Vector3.zero;
        settings = FindObjectOfType<GameSettings>();
        levelRunner = FindObjectOfType<LevelRunner>();
    }

    private void Update()
    {
        if (!levelRunner.isDead)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseActive = !pauseActive;
                menuActive = !menuActive;
            }

            if (pauseActive)
            {
                if (menuActive && gameObject.transform.localScale.x < 1)
                {
                    LeanTween.scale(gameObject, Vector3.one, 0.4f);
                }
                settings.isPaused = true;
            }
            else
            {
                OnClose();
            }
        }
    }

    public void OnClose()
    {
        LeanTween.scale(gameObject, Vector3.zero, 0.4f);
        if (settings.isPaused)
        {
            settings.isPaused = false;
        }
        pauseActive = false;
        menuActive = false;
    }

    public override void OpenQuit()
    {
        quitMenu.menu = this;
        LeanTween.scale(gameObject, Vector3.zero, 0.4f);
        LeanTween.scale(quitMenu.gameObject, Vector3.one, 0.4f);
    }

    public override void CloseQuit()
    {
        LeanTween.scale(gameObject, Vector3.one, 0.4f);
        LeanTween.scale(quitMenu.gameObject, Vector3.zero, 0.4f);
    }
}
