using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDef", menuName = "Scriptable Objects/CharacterDef")]
public class CharacterDef : ScriptableObject
{

    // -- basic character info --
    private string _characterName;
    private Sprite _characterSprite;
    public int _characterLevel;
    public int _characterDLevel;
    public int _characterMaxHitPoints;
    public int _characterCurrentHitPoints;
    public int _characterMaxMagicPoints;
    public int _characterCurrentMagicPoints;
    public int _characterCurrentExperiencePoints;
    public DragoonSpirit _characterDragoonSpirit;
    public bool _hasDragoonSpirit = false;

    // -- character stats --
    // used during combat, influenced by equipment.
    
    // -- body stats --
    [System.Serializable]
    public struct StatBlock
    {
        public int physicalAttack;
        public int physicalDefense;
        public int magicAttack;
        public int magicDefense;
        public int speed;
    }


    // -- total stats --

    public StatBlock bodyStats;
    public StatBlock weaponStats;
    public int _totalPhysicalAttack => bodyStats.physicalAttack + weaponStats.physicalAttack;
    public int _totalPhysicalDefense => bodyStats.physicalDefense + weaponStats.physicalDefense;  
    public int _totalMagicAttack => bodyStats.magicAttack + weaponStats.magicAttack;
    public int _totalMagicDefense => bodyStats.magicDefense + weaponStats.magicDefense;
    public int _totalSpeed => bodyStats.speed + weaponStats.speed;

    // -- dragoon stats --
    // these stats stay constant, and are determined by the dragoonSpirit class - this code is used for the character sheet, and the dragoon spirit's stats are used during combat.
    public int _dragoonPhysicalAttack => _characterDragoonSpirit.dStats.physicalAttack;  
    public int _dragoonPhysicalDefense => _characterDragoonSpirit.dStats.physicalDefense;
    public int _dragoonMagicAttack => _characterDragoonSpirit.dStats.magicAttack;
    public int _dragoonMagicDefense => _characterDragoonSpirit.dStats.magicDefense;  



}
