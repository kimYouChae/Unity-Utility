using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;      // ��Ʈ��

public class UiPopUp : MonoBehaviour
{
    [Header("===State===")]
    [SerializeField] protected float onStartSize;   // ON : �˾� ���� ������
    [SerializeField] protected float oriSize;       // ���� ������ ( �⺻ : 1f )
    [SerializeField] protected float offEndSize;    // Off : �˾� �� ������

    [SerializeField] protected float startTime;     // On�� �� �˾� ���� �ӵ�
    [SerializeField] protected float endTime;       // Off�� �� �˾� ���� �ӵ�

    [SerializeField] protected Ease popUpEase;      // �˾� �ε巯�� �̵� ( �⺻ : Ease.OutBack )


    // Mono�� �����ֱ� �Լ� awake -> onEnable -> start
    protected virtual void Awake()
    {
        onStartSize = 0.8f;
        oriSize     = 1f;
        offEndSize  = 0.8f;
        startTime   = 0.25f;
        endTime     = 0.2f;

        popUpEase = Ease.OutBack;

        // ��ġ ���� �ʿ� �� �������� �ʱ�ȭ 
        InitPopUpState();
    }

    protected virtual void OnEnable() 
    {
        // ���� ��
        PlayShowAnimation(onStartSize , oriSize, startTime , popUpEase);
    }

#region �ʿ��ϸ� �������� override
    
    // popup ��ġ ���� �ʿ��� �� 
    protected virtual void InitPopUpState() { }

#endregion

    protected void PlayShowAnimation(float startSize, float endSize, float speed, Ease ease) 
    {
        // Debug.Log($"{startSize} ���� {endSize} ���� ũ�� Ŀ�� {speed}�� �ӵ��� ");
        // �� �� onStartSize  -> oriSize ��ŭ       , startTime �� �ӵ���
        // �� �� oriSize      -> offEndSize ��ŭ    , endTime�� �ӵ��� 
        transform.localScale = Vector3.one * startSize;
        transform.DOScale(Vector3.one * endSize, speed).SetEase(ease);
    }

    protected void OffPanel() 
    {
        // ���� �� 
        PlayShowAnimation(oriSize, offEndSize, endTime, popUpEase);

        Invoke("SetActiveFalsePanel", endTime / 2);
    }

    private void SetActiveFalsePanel() 
    {
        gameObject.SetActive(false); 
    }

}
