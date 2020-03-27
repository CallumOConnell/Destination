﻿using UnityEngine;

namespace Destination
{
    public enum ItemType
    {
        Equipment,
        Health,
        Default
    }

    public abstract class ItemObject : ScriptableObject
    {
        public int id;

        public GameObject prefab;

        public ItemType type;

        [TextArea(15, 20)]
        public string description;
    }
}