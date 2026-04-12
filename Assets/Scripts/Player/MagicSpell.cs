using UnityEngine;

[CreateAssetMenu(fileName = "MagicSpell", menuName = "Scriptable Objects/MagicSpell")]
public class MagicSpell : ScriptableObject
{

    public string _spellName;
    public int _spellCost;
    public string _spellDescription;
    public int _spellLevelRequired;
}
