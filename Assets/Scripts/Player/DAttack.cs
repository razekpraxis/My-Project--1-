using UnityEngine;

[CreateAssetMenu(fileName = "DAttack", menuName = "Scriptable Objects/DAttack")]
public class DAttack : ScriptableObject
{
    public string _attackName;
    private const int _attackLevel = 5;
    public int _attackDamage;
    
}
