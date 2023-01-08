using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Gpm.Ui.Internal
{
    public class TabLinkObject : MonoBehaviour
    {
        [Header("Tab Setting", order = 1)]
        [SerializeField]
        internal TabRoot root;

        public TabGroup GetGroup()
        {
            return root.group;
        }

        internal bool SetGroup(TabGroup group, bool change = true)
        {
            if (root.group != group )
            {
                if (root.group != null)
                {
                    if (change == true)
                    {
                        if (root.group.IsLinked(this) == true)
                        {
                            root.group.Release(this);
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                root.group = group;
            }

            return true;
        }

        internal bool SetGroup(object group, bool change = true)
        {
            if (group is TabGroup)
            {
                return SetGroup(group as TabGroup, change);
            }
            return false;
        }

        public bool IsValidGroup()
        {
            if (root.group != null)
            {
                if (root.group.IsLinked(this) == false)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsSameGroup(TabLinkObject linkObject)
        {
            return GetGroup() == linkObject.GetGroup();
        }

        public void UpdateValidGroup()
        {
            if(IsValidGroup() == false)
            {
                root.group = null;
            }
        }

        public void ReleaseGroup()
        {
            if (root.group != null)
            {
                root.group.Release(this);
                root.group = null;
            }
        }

        public bool UnLink()
        {
            if (root.group != null)
            {
                if(root.group.GetLinkCount(this) <= 1)
                {
                    ReleaseGroup();
                    return true;
                }
            }

            return true;
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public bool GetActive()
        {
            return gameObject.activeSelf;
        }
    }
}
