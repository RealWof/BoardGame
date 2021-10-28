//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//using SWG.Core.UIControls;

//using Sirenix.OdinInspector;

//namespace SWG.Game.ZoneControl
//{
//    public class PlayerScorePanel : MonoBehaviour
//    {
//        [System.Serializable]
//        public class PlayerScoreUI
//        {
//            public GameObject main;
//            public Text numberText;
//            public Text scoreText;
//            public Image playerImage;
//            public Transform selector;
//            public PlayerManager.PlayerInfo playerInfo;
//            public CountPanel lifePanel;

//            public void ChangeScore(int newValue)
//            {
//                if (scoreText) scoreText.text = newValue.ToString();
//            }

//            public void ChangeLife(int newValue)
//            {
//                if (lifePanel) lifePanel.SetCount(newValue);
//            }
//        }

//        public List<PlayerScoreUI> playerScoreUIs;

//        public bool useSortByScore = false;
//        public float[] flexibleSize = new float[] { 2, 1.5f, 1.25f, 1 };
//        public bool flexibleHeight = false;

//        private void OnEnable()
//        {
//            if (useSortByScore) ApplySort();
//        }

//        private void OnValidate()
//        {
//            UpdateScoresUI();
//        }

//        public void Init(PlayerManager.PlayerInfo[] playerInfos, int countPlayers)
//        {
//            // Подписывание игроков
//            for (int i = 0; i < playerScoreUIs.Count; i++)
//            {
//                int id = i;
//                if (i < playerInfos.Length)
//                {
//                    playerScoreUIs[i].playerInfo = playerInfos[i];
//                    playerScoreUIs[i].playerImage.sprite = playerInfos[i].sprite;
//                    if (playerScoreUIs[i].lifePanel) playerScoreUIs[i].lifePanel.SetSprites(playerInfos[i].playerTemplate.data.scoreSelector);
//                    playerScoreUIs[id].playerInfo.onUpdateScore += playerScoreUIs[id].ChangeScore;
//                    playerScoreUIs[id].playerInfo.onUpdateLife += playerScoreUIs[id].ChangeLife;
//                    playerScoreUIs[i].playerInfo.Score = 0;
//                    playerScoreUIs[i].playerInfo.Life = 3;
//                }
//            }
//            // Включение нужных и выключение не нужных
//            for (int i = 0; i < playerScoreUIs.Count; i++)
//            {
//                playerScoreUIs[i].main.SetActive(i < countPlayers);
//            }
//        }

//        public void DeInit()
//        {
//            for (int i = 0; i < playerScoreUIs.Count; i++)
//            {
//                playerScoreUIs[i].playerInfo.onUpdateScore -= playerScoreUIs[i].ChangeScore;
//                playerScoreUIs[i].playerInfo.onUpdateLife -= playerScoreUIs[i].ChangeLife;
//            }
//        }

//        public void SetCurrentPlayer(int id)
//        {
//            for (int i = 0; i < playerScoreUIs.Count; i++)
//            {
//                playerScoreUIs[i].selector.gameObject.SetActive(id == i);
//            }
//        }

//        [Button]
//        private void ApplySort()
//        {
//            List<PlayerScoreUI> toSort = new List<PlayerScoreUI>(playerScoreUIs);
//            // Сортировка по очкам, от большего к меньшему
//            toSort.Sort((x, y) => y.playerInfo.Score.CompareTo(x.playerInfo.Score));
//            // Сортировка по жизням, если очки одинаковые
//            for (int i = 0; i < toSort.Count - 1; i++)
//            {
//                if (toSort[i].playerInfo.Score == toSort[i + 1].playerInfo.Score
//                    && toSort[i].playerInfo.Life < toSort[i + 1].playerInfo.Life)
//                {
//                    PlayerScoreUI temp = toSort[i];
//                    toSort[i] = toSort[i + 1];
//                    toSort[i + 1] = temp;
//                }
//            }
//            // Сортировка UI элементов
//            for (int i = 0; i < toSort.Count; i++)
//            {
//                toSort[i].main.transform.SetSiblingIndex(i);
//                toSort[i].playerImage.sprite = toSort[i].playerInfo.sprite;
//                if (toSort[i].numberText) toSort[i].numberText.text = (i + 1).ToString();
//                //id++;
//                LayoutElement layoutElement = toSort[i].main.GetComponent<LayoutElement>();
//                if (layoutElement)
//                {
//                    if (flexibleHeight)
//                    {
//                        layoutElement.flexibleHeight = flexibleSize[i];
//                    }
//                    else
//                    {
//                        layoutElement.flexibleWidth = flexibleSize[i];
//                    }
//                }
//            }
//        }

//        [Button]
//        private void UpdateScoresUI()
//        {
//            if (playerScoreUIs == null || playerScoreUIs.Count == 0)
//            {
//                playerScoreUIs = new List<PlayerScoreUI>();
//                int count = transform.childCount;
//                for (int i = 0; i < count; i++)
//                {
//                    PlayerScoreUI playerScoreUI = new PlayerScoreUI();
//                    playerScoreUI.main = transform.GetChild(i).gameObject;
//                    playerScoreUI.scoreText = playerScoreUI.main.GetComponentInChildren<Text>();
//                    playerScoreUI.playerImage = playerScoreUI.main.GetComponentInChildren<Image>();
//                    playerScoreUI.lifePanel = playerScoreUI.main.GetComponent<CountPanel>();
//                    playerScoreUI.selector = playerScoreUI.main.transform.Find("Selector");
//                    playerScoreUIs.Add(playerScoreUI);
//                }
//            }
//        }
//    }
//}