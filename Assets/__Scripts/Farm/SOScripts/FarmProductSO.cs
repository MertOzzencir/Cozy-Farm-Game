using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FarmSO", menuName = "Farm Product")]
public class FarmProductSO : ScriptableObject
{
    public string NameOfProduct;
    public GameObject PreFab;
    public float GrowTimer;
    public FarmProductType Type;
}
