using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatusPopUp : UiPopUp
{
    [Header("===Component===")]
    [SerializeField] TextMeshProUGUI[] stateAmountText;
    [SerializeField] Button backButton;

    [Header("===State===")]
    [SerializeField] Character currCharacter;

    private void Start()
    {
        backButton.onClick.AddListener(base.OffPanel);
    }

    // 인벤토리 업데이트
    public void SetUpPlayerStatus(Character ch)
    {
        UpdateStatus(ch);
    }

    public void UpdateStatus(Character character)
    {
        try
        {
            stateAmountText[0].text = character.Attack.ToString();
            stateAmountText[1].text = character.Defence.ToString();
            stateAmountText[2].text = character.Hp.ToString();
            stateAmountText[3].text = character.CriticalHit.ToString();
        }
        catch (Exception e) { Debug.Log($"CharacterStatusPopUp : {e}"); }

    }

}
