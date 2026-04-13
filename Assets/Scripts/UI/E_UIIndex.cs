using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class E_UIIndex : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] private Image image;
    private ItemData itemData;

    public void Init(Sprite sprite, string text)
    {
        image.sprite = sprite;
        this.text.SetText(text);
    }
}
