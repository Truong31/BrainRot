using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] List<E_UIIndex> list = new List<E_UIIndex>();
    [Header("Button")]
    [SerializeField] Button commonIndexBtn;
    [SerializeField] Button unCommonIndexBtn;
    [SerializeField] Button rareIndexBtn;

    private List<ItemData> items => GameData.instance.listItem;

    void Start()
    {
        commonIndexBtn.onClick.AddListener(() => { GetIndex(I_Rarity.Common); });
        unCommonIndexBtn.onClick.AddListener(() => { GetIndex(I_Rarity.UnCommon); });
        rareIndexBtn.onClick.AddListener(() => { GetIndex(I_Rarity.Rare); });
        GetIndex(I_Rarity.Common);
    }

    void GetIndex(I_Rarity rarity)
    {
        for(int i = 0; i < list.Count; i++)
        {
            list[i].gameObject.SetActive(false);
        }

        int index = 0;
        for(int i = 0; i < items.Count; i++)
        {
            if(items[i].rarity == rarity)
            {
                list[index].gameObject.SetActive(true);
                list[index].Init(items[i].sprite, items[i].itemName);
                index++;
            }
        }
    }

}
