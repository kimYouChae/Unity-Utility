using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PopupPlayer : UiPopUp
{
    [Space]
    [Header("===Ui Component===")]
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private TextMeshProUGUI textName;

    [SerializeField] private Button levelUpButton;
    [SerializeField] private Button closeButton;

    // 플레이어 정보
    [SerializeField] private PlayerTestClass player;

    // 스크립트 마다 다름   
    private Action levelUpAction;

    private void Start()
    {
        // 끄는 버튼 이벤트는 한번만 하면됨.
        closeButton.onClick.AddListener(base.OffPanel);
    }

    // Initalize는 1회 실행
    // ChangePopup은 초기화 필요할 때 마다 실행 (OnEnable)
    public void Initialize(PlayerTestClass player, Action onLevelUp = null) 
    {
        // 필드 초기화
        this.player = player;
        this.levelUpAction = onLevelUp;

        // 버튼 이벤트 등록
        levelUpButton.onClick.AddListener(OnLevelUp);

        // 업데이트
        UpdatePopUpField();
    }

    // Ui 업데이트
    private void UpdatePopUpField() 
    {
        textLevel.text = $"Level : {player.level}";
        textName.text = $"Name : {player.name}";
    }

    public void OnLevelUp() 
    {
        levelUpAction?.Invoke();
    }
}
