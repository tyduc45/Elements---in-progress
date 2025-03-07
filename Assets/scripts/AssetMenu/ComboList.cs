using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboList", menuName = "Scriptable Objects/ComboATKList")]
public class ComboATKList : ScriptableObject
{
    public List<SingleAttackSO> ComboList = new List<SingleAttackSO>();
}
