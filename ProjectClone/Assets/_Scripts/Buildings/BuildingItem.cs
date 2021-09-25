using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingItem",menuName = "Building/Item")]
public class BuildingItem : ScriptableObject
{
    public Sprite Icon;
    public int Mineral;
    public int Organism;
    public Cell cell;
}
