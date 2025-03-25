using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityExtension 
{
    /// <summary>
    ///  ��� Ŭ������ ����� Ȯ���� �� �ִ� �޼���
    ///  
    /// </summary>

    #region Vector2 / Vector3

    public static void Reset(this Vector2 vector) 
    {
        
    }
    #endregion


    #region GameObject
    // T Ÿ���� ������Ʈ�� return, ������ �߰� �� return
    public static T AddAndReturnComponent<T>(this GameObject gameObject) where T : Component
    {
        var component = gameObject.GetComponent<T>();
        if(component == null)
            gameObject.AddComponent<T>();

        return component;   
    }

    // T Ÿ���� ������Ʈ�� ������ true ����, ������ false ���� 
    public static bool HasComponent<T>(this GameObject gameObject) 
    {
        return gameObject.GetComponent<T>() != null;
    }

    #endregion

    #region List

    #endregion

    #region Trasnform

    // �θ� �ִٸ� �θ� ���� �� ��ġ, ȸ��, ũ�� ���� 
    public static void InitTransform(this Transform trs, Transform parent = null) 
    {
        if (parent != null)
            trs.SetParent(parent);

        trs.localPosition = Vector3.zero;
        trs.localRotation = Quaternion.identity;
        trs.localScale = Vector3.one;
    }

    // Transform�� SetActive
    public static void SetActive(this Transform trs, bool value) 
    {
        if (trs != null)
            trs.gameObject.SetActive(value);
    }

    // Transform�� onoff ���� return
    public static bool IsActive(this Transform trs) 
    {
        return trs.gameObject.activeSelf;
    }

    #endregion

    #region Enum 
    // Enum�� ���� return
    public static int Length(this Enum enumType) 
    {
        return System.Enum.GetValues(enumType.GetType()).Length;
    }

    // Enum�� Array �� return
    public static Array ToArray(this Enum enumType) 
    { 
        Array array = Enum.GetValues(enumType.GetType());
        return array;
    }

    #endregion
}
