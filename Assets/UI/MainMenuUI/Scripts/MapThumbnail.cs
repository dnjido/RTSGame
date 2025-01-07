using RTS;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MapThumbnail : MonoBehaviour
{
    private StartGameProperties startGameProperties;
    //private PlayerPropertiesStruct properties => new PlayerPropertiesStruct();

    [Inject]
    public void Init(StartGameProperties sg)
    {
        startGameProperties = sg;
        startGameProperties.ChangeMapEvent += SetThumbnail;
    }

    private void SetThumbnail(MapProperties map) =>
        GetComponent<Image>().sprite = Sprite.Create(
            map.thumbnail, new Rect(0, 0, map.thumbnail.width, map.thumbnail.height), new Vector2(0.5f, 0.5f));
}
