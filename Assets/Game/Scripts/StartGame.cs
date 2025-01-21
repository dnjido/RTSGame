using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.ComponentModel;

namespace RTS
{
    [Serializable]
    public class StartGame
    {
        private StartGameProperties startGameProperties;
        private HasPlaying hasPlaying;
        private AIManadger[] AI;
        private Relationship[] relationships;

        private PlayerPropertiesStruct GetProperties(int id) => startGameProperties.playerProperties[id];

        public List<StartPoint> startPoints;

        public void Init(StartGameProperties sg, AIManadger[] ai, Relationship[] r, HasPlaying p)
        {
            startGameProperties = sg;
            AI = ai;
            relationships = r;
            hasPlaying = p;
            Start();
        }

        public void InitEmpty(AIManadger[] ai, Relationship[] r, HasPlaying p)
        {
            AI = ai;
            relationships = r;
            hasPlaying = p; 
            StartEmpty();
        }

        public void Start()
        {
            if (startGameProperties == null) return;

            SetProperties();
        }

        public void SetProperties()
        {
            for(int i = 0; i < startGameProperties.map.playerCount; i++)
                if (GetProperties(i).active == true)
                {
                    hasPlaying.status[i] = true;
                    SetPoints(i);
                    SetAI(i);
                    SetRelantioship(i);
                }

            for (int i = 0; i < startGameProperties.map.playerCount; i++) 
                relationships[i].SetRelationship(relationships, i);
        }

        public void SetPoints(int i)
        {
            StartPoint point = new StartPoint();
            point.team = GetProperties(i).team;
            point.startPoint = GameObject.Find("MapPoint" + (GetProperties(i).startPoint + 1));

            point.startPoint.GetComponent<StartPlayer>().start = point;
        }

        public void SetAI(int i)
        {
            bool isAI = GetProperties(i).isBot;
            if(isAI) AI[i].Start(GetProperties(i).difficulty);
        }

        public void SetRelantioship(int i)
        {
            Teams rel = GetProperties(i).relationship;
            relationships[i].team = rel;
        }

        public void StartEmpty()
        {
            AI.ToList().ForEach(item => item.Start());

            for (int i = 0; i < relationships.Length; i++)
                relationships[i].SetRelationship(relationships, i);
        }
    }

    [Serializable]
    public struct StartPoint
    {
        public GameObject startPoint;
        public int team;
    }
}
