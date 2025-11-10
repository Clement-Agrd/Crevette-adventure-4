using Passives.PassiveHealing;
using Scripts.Passives;
using UnityEngine;

namespace Scripts
{
    public abstract class PassiveData : ScriptableObject
    {
        [field: SerializeField]
        public string Title { get; private set; }

        public abstract Passive CreatePassive(Hero user);
    }
}