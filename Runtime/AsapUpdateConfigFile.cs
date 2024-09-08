using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsapUpdateConfigFile : ScriptableObject
{
#if ENABLE_INPUT_SYSTEM
    public bool AllowManualInputSystemUpdate = true;
#endif
}
