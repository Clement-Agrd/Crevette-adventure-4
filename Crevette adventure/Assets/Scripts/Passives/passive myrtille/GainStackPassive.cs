using UnityEngine;

namespace Scripts.Passives
{
    public class GainStackPassive : Passive
    {
        private GainStackPassiveData data;
        public int CurrentStacks { get; private set; } = 0;

        public GainStackPassive(Hero user, GainStackPassiveData data) : base(user)
        {
            this.data = data;
            user.OnTurnStart += OnTurnStartHandler;
        }

        private void OnTurnStartHandler(Hero h)
        {
            if (h != user) return;

            CurrentStacks += data.StacksPerTurn;
            Debug.Log($"{user.Name} gagne {data.StacksPerTurn} stack(s) (Total : {CurrentStacks})");
        }

        // Permet de consommer les stacks après l'attaque
        public int ConsumeStacks()
        {
            int temp = CurrentStacks;
            CurrentStacks = 0;
            return temp;
        }
    }
}