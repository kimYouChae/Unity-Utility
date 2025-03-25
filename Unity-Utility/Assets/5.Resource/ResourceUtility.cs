using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUtility  
{
    public static Sprite[] ReturnResource(string fileName) 
    {
        var list = Resources.LoadAll<Sprite>(fileName);

        return list;
    }
}
