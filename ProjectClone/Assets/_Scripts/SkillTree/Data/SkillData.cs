using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/Skill Data")]
public class SkillData : ScriptableObject
{
    public enum SkillType : int
    {
        IncreaseSpeed,
        IncreaseMaxHp,
        IncreaseMaxMp,
        IncreaseMiningPower,
        DecreaseMiningSpeed,
        IncreaseBuildingRange,
        IncreaseAdditionalExp,
        IncreaseMiningAmount,
        AddAutoAttacking
    }

    public SkillType skillType;
    public float value;
    public string skillTitle;
    
    [Multiline] public string skillText;
}
