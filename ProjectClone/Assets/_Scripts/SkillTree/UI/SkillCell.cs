using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCell : MonoBehaviour
{
    public bool isUnlocked, canUnlock;
    public SkillData skillData;
    public List<SkillCell> adjCell;
}
