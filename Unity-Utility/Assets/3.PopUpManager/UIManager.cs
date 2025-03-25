using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;      // list.Any ��� 
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // �̱���
     private static UIManager instance;
    public static UIManager Instance
    {
        get 
        {
            if (instance != null)
                return instance;

            instance = new GameObject("UIManager").AddComponent<UIManager>();
            return instance;
        }
    }

    // �˾� ���� ����Ʈ 
    [SerializeField] private List<UiPopUp> popUps;

    // ĵ���� ������Ʈ
    [SerializeField] private GameObject canvas;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        popUps = new List<UiPopUp>();

        // ĵ���� ã�� (�̸�����)
        canvas = GameObject.Find("Canvas");
        if (canvas == null)
            Debug.LogError($"UIManager : canvas�� �ҷ����µ� ���� �߽��ϴ�");
    }

    #region �Լ� �߰� 
    // �ش� UiPopUp�� ����Ʈ�� �ִ��� �˻� 
    private bool ContainsUiPopup(string name) 
    {
        // typeof : ������Ÿ�ӿ�
        // Gettype() : ��Ÿ�ӿ�
        // ������ true, ������ false
        return popUps.Any(pop => pop.GetType().Name == name);
    }

    // string�� �ش��ϴ� UiPopup �� return
    private UiPopUp ReturnPopUpByName(string name) 
    {
        // Find : ����Ʈ�� ������ �˻�
        return popUps.Find(pop => pop.GetType().Name == name);
    }

    #endregion

    // ���� �Լ��̸� : ShowPopUp
    public T ShowPopUp<T>() where T : UiPopUp 
    {
        // T�� �Ű������� �ѱ���� ���� , �ѱ���� ���÷��� ���
        // �迭�� ������ 
        if (ContainsUiPopup(typeof(T).Name)) 
        {
            UiPopUp temp = ReturnPopUpByName(typeof(T).Name);
            temp.gameObject.SetActive(true);
            return temp as T;
        }

        // 1. Ŭ���� �̸��� ���ڿ��� ������
        // 2. UiPopUp Ÿ���� T Ÿ������ ��ȯ�ؼ� return
        // ex) T�� PopupPlayer�� ��,
        //     ShowPopup���� return�� UiPopup�� PopupPlayer�� �ٲ㼭 return
        return InstancePopUp(typeof(T).Name) as T;
    }

    // �˾� ������ ������ (Resource/PopUp �� )
    public UiPopUp InstancePopUp(string popupName) 
    {
        // ##TODO : ���߿� Resource �δ� ����� ������ ?
        // Resource ���� PopUp �������� �̸����� ������ �������� 
        GameObject upPrefab = Resources.Load($"PopUp/{popupName}", typeof(GameObject)) as GameObject;

        // null�̸� return
        if (upPrefab == null) 
        {
            Debug.LogWarning($"UIManager : InstancePopUp ���� {popupName} �ҷ����� ����");
            return null;
        }

        // ������Ʈ �ν��Ͻ�ȭ, canvas ������
        GameObject popupInstance = null;
        try
        {
            popupInstance = Instantiate(upPrefab, canvas.transform);
        }
        catch (Exception e) { Debug.Log($"UIManager : {upPrefab.name} ������ ���� ���� {e}"); }

        // ����Ʈ�� ���� �� return 
        return AddtoPopUpList(popupInstance);
    }

    // ����Ʈ�� �ֱ�
    private UiPopUp AddtoPopUpList(GameObject instance) 
    {
        UiPopUp uiPopUp = null;
        try 
        {
            uiPopUp = instance.GetComponent<UiPopUp>();
        }
        catch (Exception e) { Debug.Log($"UIManager : {instance.name} �����տ��� GetComponent ���� {e}"); }

        // �׻� ù��°�� ���� insert
        popUps.Insert(0, uiPopUp);

        // �ѱ�
        instance.SetActive(true);

        return uiPopUp;
    }
}
