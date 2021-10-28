using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.GameManagment
{
    public class BaseGame : ScriptableObject
    {
        public virtual void StartGame(bool newGame)
        {

        }

        public virtual void StopGame()
        {

        }

        public virtual void Pause(bool showPopup = false)
        {

        }

        public virtual void ContinueGame()
        {

        }

        public virtual void RestartGame()
        {

        }
        


    }
}