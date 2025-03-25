using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum EColor
{
    Black, White, Gray, Red, Green, Blue, Yellow, Cyan, Magenta
}
public static class EditorUtility 
{
    // 메모 박스를 직접 그리기 위한 간단한 유틸리티 메서드
    public static void DrawMemoBox(string[] contents, EColor boxColor = EColor.Gray, EColor textColor = EColor.White)
    {
        GUIStyle labelStyle = EditorStyles.boldLabel;
        Color originalTextColor = labelStyle.normal.textColor;
        int originalFontSize = labelStyle.fontSize;

        // 박스 설정
        float lineHeight = 18f;
        float paddingTop = 8f;
        float paddingBottom = 8f;
        float paddingLeft = 8f;

        // 박스 치수 계산
        float textHeight = contents.Length * lineHeight;
        float boxHeight = textHeight + paddingTop + paddingBottom;
        float boxWidth = EditorGUILayout.GetControlRect().width;

        // 배경 박스 그리기
        Rect boxRect = EditorGUILayout.GetControlRect(false, boxHeight);
        boxRect.width = boxWidth;

        // 열거형 색상을 Unity 색상으로 변환
        Color boxColorValue = GetColorFromEnum(boxColor);
        Color textColorValue = GetColorFromEnum(textColor);

        EditorGUI.DrawRect(boxRect, boxColorValue);

        // 텍스트 스타일 적용
        labelStyle.normal.textColor = textColorValue;
        labelStyle.fontSize = 12;

        // 각 텍스트 줄 그리기
        float curPosY = boxRect.y + paddingTop;
        foreach (var text in contents)
        {
            Rect curRect = new Rect(boxRect.x + paddingLeft, curPosY, boxWidth - paddingLeft, lineHeight);
            EditorGUI.LabelField(curRect, text, labelStyle);
            curPosY += lineHeight;
        }

        // 원래 스타일 복원
        labelStyle.normal.textColor = originalTextColor;
        labelStyle.fontSize = originalFontSize;
    }

    private static Color GetColorFromEnum(EColor color)
    {
        switch (color)
        {
            case EColor.Black: return Color.black;
            case EColor.White: return Color.white;
            case EColor.Gray: return Color.gray;
            case EColor.Red: return Color.red;
            case EColor.Green: return Color.green;
            case EColor.Blue: return Color.blue;
            case EColor.Yellow: return Color.yellow;
            case EColor.Cyan: return Color.cyan;
            case EColor.Magenta: return Color.magenta;
            default: return Color.white;
        }
    }
}
