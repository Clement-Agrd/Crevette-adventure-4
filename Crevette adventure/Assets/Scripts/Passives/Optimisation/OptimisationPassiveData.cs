using Scripts.Passives;
using Scripts.UltiChargeOnHeal;
using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(menuName = "CrevetteAdventure/Passives/UltiChargeOnHeal")]
    public class UltiChargeOnHealPassiveData : PassiveData
    {
        [field: SerializeField, Range(1, 3)]
        public int UltiChargeAmount { get; private set; } = 1; // combien de charges d'ulti à donner par soin

        public override Passive CreatePassive(Hero user)
        {
            return new UltiChargeOnHealPassive(user, this);
        }
    }
}