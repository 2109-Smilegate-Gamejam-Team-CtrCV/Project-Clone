using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : Singleton<OptionManager>
{
    public float Master { get; set; } = 1;
    public float BGM { get; set; } = 1;
    public float VFX { get; set; } = 1;


}
