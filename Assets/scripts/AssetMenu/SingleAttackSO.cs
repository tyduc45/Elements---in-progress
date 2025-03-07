using UnityEngine;

[CreateAssetMenu(fileName = "SingleAttackSO", menuName = "Scriptable Objects/SingleAttackSO")]
public class SingleAttackSO : ScriptableObject
{
    public AnimatorOverrideController animOVC;
    public float damage;                        // damage of this atk movement
}