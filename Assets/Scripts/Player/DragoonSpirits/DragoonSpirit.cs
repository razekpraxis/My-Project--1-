using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "DragoonSpirit", menuName = "Scriptable Objects/DragoonSpirit")]
public class DragoonSpirit : ScriptableObject
{
    private string _spiritName;
    private Sprite _spiritSprite;

    
    public List<MagicSpell> _magicSpells;
    public List<DAttack> _dragoonAttacks;

        [System.Serializable]
    public struct StatBlock
    {
        public int physicalAttack;
        public int physicalDefense;
        public int magicAttack;
        public int magicDefense;
    }

    public StatBlock dStats;
}
