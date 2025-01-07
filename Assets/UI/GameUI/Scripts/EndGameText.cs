using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameText : MonoBehaviour
{
    public void SetText(string text)
    {
        gameObject.SetActive(true);
        GetComponent<TMP_Text>().text = text;
    }
}
