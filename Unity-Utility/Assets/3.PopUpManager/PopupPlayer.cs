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

    // �÷��̾� ����
    [SerializeField] private PlayerTestClass player;

    // ��ũ��Ʈ ���� �ٸ�   
    private Action levelUpAction;

    private void Start()
    {
        // ���� ��ư �̺�Ʈ�� �ѹ��� �ϸ��.
        closeButton.onClick.AddListener(base.OffPanel);
    }

    // Initalize�� 1ȸ ����
    // ChangePopup�� �ʱ�ȭ �ʿ��� �� ���� ���� (OnEnable)
    public void Initialize(PlayerTestClass player, Action onLevelUp = null) 
    {
        // �ʵ� �ʱ�ȭ
        this.player = player;
        this.levelUpAction = onLevelUp;

        // ��ư �̺�Ʈ ���
        levelUpButton.onClick.AddListener(OnLevelUp);

        // ������Ʈ
        UpdatePopUpField();
    }

    // Ui ������Ʈ
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
