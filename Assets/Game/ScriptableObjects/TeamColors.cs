using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TeamColors", menuName = "ScriptableObjects/TeamColors", order = 1)]
public class TeamColors : ScriptableObject
{
    public Material[] materials;
}
