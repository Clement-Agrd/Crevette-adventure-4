using UnityEngine;

namespace Scripts.Buffs
{
    public class StatBuff
    {
        public int AtkBonus;
        public int DefBonus;
        public int RemainingTurns;

        public StatBuff(int atkBonus, int defBonus, int turns)
        {
            AtkBonus = atkBonus;
            DefBonus = defBonus;
            RemainingTurns = turns;
        }
    }
}