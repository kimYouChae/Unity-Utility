using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityExtension 
{
    /// <summary>
    ///  모든 클래스의 기능을 확장할 수 있는 메서드
    ///  
    /// </summary>

    #region Vector2 / Vector3

    public static void Reset(this Vector2 vector) 
    {
        
    }
    #endregion


    #region GameObject
    // T 타입의 컴포넌트를 return, 없으면 추가 후 return
    public static T AddAndReturnComponent<T>(this GameObject gameObject) where T : Component
    {
        var component = gameObject.GetComponent<T>();
        if(component == null)
            gameObject.AddComponent<T>();

        return component;   
    }

    // T 타입의 컴포넌트가 있으면 true 리턴, 없으면 false 리턴 
    public static bool HasComponent<T>(this GameObject gameObject) 
    {
        return gameObject.GetComponent<T>() != null;
    }

    #endregion

    #region List

    #endregion

    #region Trasnform

    // 부모가 있다면 부모 지정 후 위치, 회전, 크기 리셋 
    public static void InitTransform(this Transform trs, Transform parent = null) 
    {
        if (parent != null)
            trs.SetParent(parent);

        trs.localPosition = Vector3.zero;
        trs.localRotation = Quaternion.identity;
        trs.localScale = Vector3.one;
    }

    // Transform의 SetActive
    public static void SetActive(this Transform trs, bool value) 
    {
        if (trs != null)
            trs.gameObject.SetActive(value);
    }

    // Transform의 onoff 여부 return
    public static bool IsActive(this Transform trs) 
    {
        return trs.gameObject.activeSelf;
    }

    #endregion

    #region Enum 
    // Enum의 길이 return
    public static int Length(this Enum enumType) 
    {
        return System.Enum.GetValues(enumType.GetType()).Length;
    }

    // Enum을 Array 로 return
    public static Array ToArray(this Enum enumType) 
    { 
        Array array = Enum.GetValues(enumType.GetType());
        return array;
    }

    #endregion
}
