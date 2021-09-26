using System;
using System.Collections;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;

public class SkillManager : Singleton<SkillManager>
{
    [NonSerialized] public AddedSkillStats currentSkillStats;
    [NonSerialized] public SkillExpModel expModel;
    [SerializeField] private TMP_Text currExp, needExp, getExpPerSec;
    [SerializeField] private TMP_Text skillTitle, skillText;
    [SerializeField] private GameObject skillTooltipPanel;

    private IDisposable _disposableCurrExpTextEvent;

    protected override void OnAwake()
    {
        base.OnAwake();
        expModel = gameObject.AddComponent<SkillExpModel>();

        _disposableCurrExpTextEvent = expModel.CurrExp.Subscribe(value => currExp.text = $"현재 경험치 : {value:F01}")
            .AddTo(currExp);
        expModel.NeedExp.Subscribe(value => needExp.text = $"다음 스킬 습득에 필요한 경험치 : {value}")
            .AddTo(needExp);
        expModel.GetExpPerSec.Subscribe(value => getExpPerSec.text = $"초당 경험치 획득량 : {value}")
            .AddTo(getExpPerSec);
        expModel.Initialize();
    }

    public void GetSkill(SkillData skillData, out bool canGetSkill)
    {
        currentSkillStats = GameManager.Instance.clone.gameObject.GetComponent<AddedSkillStats>() ??
                              GameManager.Instance.clone.gameObject.AddComponent<AddedSkillStats>();

        canGetSkill = expModel.GetSkill();

        if (!canGetSkill) return;
        
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
            case SkillData.SkillType.AddExtraLife:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (SkillGridInitializer.Instance.Cells.All(cell => cell.isUnlocked))
        {
            _disposableCurrExpTextEvent.Dispose();
            needExp.text = "다음 스킬 습득에 필요한 경험치 : MAX";
            expModel.canGetSkillShowEvent.Dispose();
        }
    }

    public Transform ShowTooltip(SkillData data)
    {
        skillTitle.text = data.skillTitle;
        skillText.text = data.value > 0 ? string.Format(data.skillText, data.value) : data.skillText;
        skillTooltipPanel.SetActive(true);
        return skillTooltipPanel.transform;
    }
}