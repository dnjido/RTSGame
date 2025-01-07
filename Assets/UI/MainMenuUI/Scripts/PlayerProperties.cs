using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace RTS
{
    [Serializable]
    public class PlayerPropertiesStruct
    {
        public bool active;
        public int team;
        public bool isBot;
        public AIDifficultyList difficulty;
        public int startPoint;
        public Teams relationship;
    }

    public class PlayerProperties : MonoBehaviour, IUICreate<int>
    {
        [SerializeField] private int ID;
        [SerializeField] private TMP_Dropdown playerList, positionList, difficultyList, relationshipList;

        private StartGameProperties startGameProperties;

        private Func<GameObject, TMP_Dropdown> droplist = obj => obj.GetComponent<TMP_Dropdown>();
        private PlayerPropertiesStruct properties => startGameProperties.playerProperties[ID];
        //private PlayerPropertiesStruct properties => new PlayerPropertiesStruct();

        [Inject]
        public void Init(StartGameProperties sg)
        {
            startGameProperties = sg;
            startGameProperties.ChangeMapEvent += SetActivePlayer;
            startGameProperties.ChangeMapEvent += SetStartPositions;
        }

        public void Create(int id) 
        {
            ID = id;
            properties.team = ID + 1;
            InitPlayer();
        }

        public void SetIsBotList(TMP_Dropdown ai) => SetPlayerType(ai);
        public void SetDifficulty(TMP_Dropdown d) => properties.difficulty = (AIDifficultyList)d.value;
        public void SetStartPoint(TMP_Dropdown s) => properties.startPoint = s.value;
        public void SetRelationship(TMP_Dropdown t) => properties.relationship = (Teams)t.value;

        public void SetActivePlayer(MapProperties map)
        {
            foreach (Transform child in transform)
                droplist(child.gameObject).interactable = ID < map.playerCount;
        }

        private void SetPlayerType(TMP_Dropdown dd)
        {
            properties.isBot = dd.value == 1 ? true : false;
            properties.active = dd.value != 0 ? true : false;

            (positionList.interactable, 
                difficultyList.interactable, 
                relationshipList.interactable) = (dd.value != 0, dd.value == 1, dd.value != 0);
        }

        public void SetStartPositions(MapProperties map)
        {
            var list = positionList.options;
            list.Clear();

            for (int i = 0; i < map.playerCount; i++)
            {
                TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData((i + 1).ToString());
                list.Add(data);
            }

            if (positionList.interactable) 
            {
                properties.startPoint = ID;
                positionList.value = ID;
            }

            SetPlayerType(playerList);
        }

        private void InitPlayer()
        {
            if (ID == 0)
            {
                playerList.interactable = false;
                playerList.value = 2;   
            }
        }
    }
}
