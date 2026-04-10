using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Money Setting")]
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] private float duration = 0.5f;
    private float targetMoney;
    private float currentMoney = 0;
    private bool isAddingMoney = false;
    private float moneyAddSpeed = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentMoney = PlayerPrefs.GetFloat("Money", 0);
        targetMoney = PlayerPrefs.GetFloat("Money", 0);
        moneyText.SetText(currentMoney.ToString());
        Debug.Log(DateTime.UtcNow);
    }

    void Update()
    {
        if (isAddingMoney)
        {
            if(currentMoney < targetMoney)
            {
                currentMoney += moneyAddSpeed * Time.deltaTime;
                if(currentMoney >= targetMoney)
                {
                    currentMoney = targetMoney;
                    isAddingMoney = false;
                    PlayerPrefs.SetFloat("Money", currentMoney);
                    PlayerPrefs.Save();
                }
            }
            moneyText.SetText(((int)currentMoney).ToString());
        }
    }

    public void AddMoney(float value)
    {
        targetMoney += value;
        moneyAddSpeed = (targetMoney - currentMoney) / duration;
        isAddingMoney = true;
    }
}
