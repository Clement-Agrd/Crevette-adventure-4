using UnityEngine;

namespace Scripts.Buffs
{
    public class BuffCamouflage
    {
        private Hero hero;
        private int remainingTurns;
        private const float DamageBonus = 0.2f;

        public bool IsActive => remainingTurns > 0;

        public BuffCamouflage(Hero hero, int duration)
        {
            this.hero = hero;
            this.remainingTurns = duration;

            // On s'abonne aux events
            hero.OnTurnStart += OnTurnStart;
        }

        private void OnTurnStart(Hero h)
        {
            if (h != hero) return;

            remainingTurns--;

            if (remainingTurns <= 0)
            {
                Remove();
            }
        }

        public void Remove()
        {
            hero.OnTurnStart -= OnTurnStart;
            Debug.Log($"{hero.Name} sort de son camouflage !");
        }

        // ✅ Méthode appelée lors d’une attaque
        public int ApplyDamageBonus(int dmg)
        {
            if (IsActive)
            {
                int newDmg = Mathf.CeilToInt(dmg * (1 + DamageBonus));
                Debug.Log($"{hero.Name} attaque depuis le camouflage (+20%) : {dmg} → {newDmg}");
                return newDmg;
            }
            return dmg;
        }
    }
    public class Blind
    {
        private Hero target;
        public int Stacks { get; private set; } = 0;
        public bool IsActive => Stacks > 0;

        public Blind(Hero target)
        {
            this.target = target;
        }

        public void AddStack()
        {
            Stacks++;
            Debug.Log($"{target.Name} est aveuglé ! (Stacks : {Stacks})");
        }

        public void RemoveStack()
        {
            Stacks--;
            if (Stacks < 0) Stacks = 0;
        }

        public void ClearStacks()
        {
            Stacks = 0;
        }
    }
}