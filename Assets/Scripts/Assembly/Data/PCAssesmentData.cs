using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PCAssesmentData", menuName = "Scriptable Objects/PCAssesmentData")]
public class PCAssesmentData : ScriptableObject
{
    public string assesmentID;
    public List<AssemblyStep> assemblySteps = new List<AssemblyStep>();
    public List<AssemblyStep> disassemblySteps = new List<AssemblyStep>();
}


[System.Serializable]
public class AssemblyStep
{
    public PCParts partID;
}

public enum PCParts
{
    None,
    CPU,
    FAN,  
    MOTHERBOARD,
    RAM1,
    RAM2,
    POWERSUPPLY,
    HARDDRIVE,
    OPTICDISK
}

public enum PCMainCam
{
    NONE,
    MOTHERBOARD,
    CASE,
    CABLE
}
