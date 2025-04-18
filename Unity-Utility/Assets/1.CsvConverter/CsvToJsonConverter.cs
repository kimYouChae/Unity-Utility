using Palmmedia.ReportGenerator.Core.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public interface ICsvParsable
{
    void Parse(string[] values);

}

public static class CsvToJsonConverter
{
    /// <summary>
    /// ***Converter 사용 전 주의사항***
    /// 1. 입력한 string, 클래스명, Recources하위의 csv 데이터 이름이 동일해야합니다.
    /// 2. Recources파일 하위에 csv데이터가 존재해야합니다.
    /// 3. csv 데이터를 파싱해서 사용할 클래스 조건
    ///     (1) 클래스여야합니다.
    ///     (2) ICsvParsable 인터페이스를 구현해야합니다.
    ///     (3) 매개변수가 없는 생성자를 가지고 있어야 합니다.
    ///
    /// </summary>


    public static void CsvConverByName(List<string> className)
    {
        // Debug.Log("CsvConverter메서드입니다");

        for (int i = 0; i < className.Count; i++)
        {
            // (에외) 비어있으면
            if (className[i] == string.Empty)
                return;

            // 여기서 type은 ? 클래스라고 생각하면 편함 
            // string에 맞는 타입 생성 
            Type type = Type.GetType(className[i]);

            if (type == null)
            {
                Debug.LogError($"해당 클래스({className[i]})를 찾을 수 없습니다.");
                continue;
            }

            if (!typeof(ICsvParsable).IsAssignableFrom(type))
            {
                Debug.LogError($"클래스({className[i]})는 ICsvParsable을 구현해야 합니다.");
                continue;
            }

            // CsvDataParsing<> 클래스의 타입을
            // MakeGeneritType : type으로 제네릭 지정
            // convertype : 즉 CsvDataParsing<클래스명>이 된다
            Type converterType = typeof(CsvDataParsing<>).MakeGenericType(type);

            // CsvDataParsing 인스턴스화 
            // 매개변수는 className[i]
            object converterInstance = Activator.CreateInstance(converterType, className[i]);

            // CsvDataParsing<>의 GetDataArray() 메서드 가져오기 
            MethodInfo method = converterType.GetMethod("GetDataArray");

            if (method != null)
            {
                // GetDataArray 메서드 Invoke
                // return 값은 List<T>이지만 object타입으로 박싱(boxing) 일어남 
                // 원본 데이터 배열 가져오기 (List<T> 타입)
                // 컴파일 타임에는 object, 런타임때는 List<T>
                object dataArray = method.Invoke(converterInstance, null);

                // 원본 타입을 유지한 채 JSON으로 변환
                string json = ListWrapperSerializer.ConvertOriginalListToJson(dataArray, type);

                // 파일로 저장
                ListWrapperSerializer.SaveJsonToFile(json, className[i]);

                Debug.Log($"{className[i]} 데이터를 성공적으로 변환했습니다.");
            }

        }
    }
}
