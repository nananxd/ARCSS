using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ContentData", menuName = "Scriptable Objects/ContentData")]
public abstract class ContentData : ScriptableObject
{

    public abstract Type GetContentType();
}
