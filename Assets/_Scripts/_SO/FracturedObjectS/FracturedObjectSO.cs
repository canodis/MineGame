using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FracturedObject", menuName = "ScriptableObjects/FracturedObject", order = 1)]

public class FracturedObjectSO : ScriptableObject
{
    [field : SerializeField] public string Name;
}
