using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Gpm.Ui.Internal
{
    [Serializable]
    public struct TabRoot
    {
        [NonSerialized]
        internal TabGroup group;
    }
}
