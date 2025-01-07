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
        public StartGameProperties startGameProperties;
        public HasPlaying hasPlaying;
        public AIManadger[] AI;
        public Relationship[] relationships;

        private PlayerPropertiesStruct GetProperties(int id) => startGameProperties.playerProperties[id];

        public List<StartPoint> startPoints;

        //public StartGame(StartGameProperties s, AIManadger[] ai, Relationship[] r)
        //{
        //    startGameProperties = s;
        //    AI = ai;
        //    relationships = r;
        //    Start();
        //}

        public void Start()
        {
            SetProperties();
            startPoints.ToList().ForEach( s =>
                s.startPoint.GetComponent<StartPlayer>().start = s );
        }

        public void SetProperties()
        {
            for(int i = 0; i < startGameProperties.map.playerCount; i++) 
            {
                if (GetProperties(i).active == true)
                {
                    hasPlaying.status[i] = true;
                    SetPoints(i);
                    SetAI(i);
                    SetRelantioship(i);
                }
            }

            for (int i = 0; i < startGameProperties.map.playerCount; i++) 
                relationships[i].SetRelationship(relationships, i);
        }

        public void SetPoints(int i)
        {
            StartPoint point = new StartPoint();
            point.team = GetProperties(i).team;
            point.startPoint = GameObject.Find("MapPoint" + (GetProperties(i).startPoint + 1));
            startPoints.Add(point);
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
            //relationships[i].SetRelationship(relationships, i);
        }
    }

    [Serializable]
    public struct StartPoint
    {
        public GameObject startPoint;
        public int team;
    }
}
