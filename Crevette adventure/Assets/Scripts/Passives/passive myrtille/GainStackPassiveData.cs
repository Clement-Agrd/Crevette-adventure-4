using UnityEngine;

namespace Scripts.Passives
{
    [CreateAssetMenu(menuName = "CrevetteAdventure/Passives/Gain Stack Each Turn", order = 0)]
    public class GainStackPassiveData : PassiveData
    {
        [Header("Nombre de stacks gagnés par tour")]
        public int StacksPerTurn = 1;

        public override Passive CreatePassive(Hero hero)
        {
            return new GainStackPassive(hero, this);
        }
    }
}