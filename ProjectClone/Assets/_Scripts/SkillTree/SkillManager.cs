using System;
using UniRx;

public class SkillManager : Singleton<SkillManager>
{
    [NonSerialized] public AddedSkillStats currentSkillStats;

    public void GetSkill(SkillData skillData)
    {
        currentSkillStats ??= GameManager.Instance.clone.gameObject.GetComponent<AddedSkillStats>() ??
                              GameManager.Instance.clone.gameObject.AddComponent<AddedSkillStats>();
        
        switch (skillData.skillType)
        {
            case SkillData.SkillType.IncreaseSpeed:
                currentSkillStats.speed += skillData.value;
                break;
            case SkillData.SkillType.IncreaseMaxHp:
                currentSkillStats.healthPoint += skillData.value;
                break;
            case SkillData.SkillType.IncreaseMaxMp:
                currentSkillStats.mentalPoint += skillData.value;
                break;
            case SkillData.SkillType.IncreaseMiningPower:
                currentSkillStats.miningPower += (int)skillData.value;
                break;
            case SkillData.SkillType.IncreaseMiningSpeed:
                currentSkillStats.miningSpeed += skillData.value;
                break;
            case SkillData.SkillType.IncreaseMiningRange:
                currentSkillStats.miningRange += skillData.value;
                break;
            case SkillData.SkillType.IncreaseBuildingRange:
                currentSkillStats.buildingRange += skillData.value;
                break;
            case SkillData.SkillType.IncreaseBuildingSpeed:
                currentSkillStats.buildingSpeed += skillData.value;
                break;
            case SkillData.SkillType.IncreaseAdditionalExp:
                currentSkillStats.additionalExp += skillData.value;
                break;
            case SkillData.SkillType.AddExtraMineral:
                currentSkillStats.canGetExtraMineral = true;
                break;
            case SkillData.SkillType.AddAutoAttacking:
                currentSkillStats.gotAutoAttack = true;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}