using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systems.GameShell.Pause;

namespace Systems.GameShell
{
    public class GameLoopManager
    {
        //singleton
        public static GameLoopManager Instance { get; private set; }

        private KeyCode pauseKeycode = KeyCode.Escape;
        public A_GamePause GamePause { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            Instance = new GameLoopManager();
            Instance.GamePause = new GamePauseDefault();

            var updateFont = new GameObject("[Flux Inputs]");
            GameObject.DontDestroyOnLoad(updateFont);
            updateFont.AddComponent<UpdateFont>();
        }

        private void UpdateInputs()
        {
            if (Input.GetKeyDown(pauseKeycode))
            {
                if (GamePause.IsPaused)
                {
                    GamePause.UnpauseGame();
                }
                else
                {
                    GamePause.PauseGame();
                }
            }
        }


        private class UpdateFont : MonoBehaviour
        {
            private void Update()
            {
                Instance.UpdateInputs();
            }
        }
    }
}
