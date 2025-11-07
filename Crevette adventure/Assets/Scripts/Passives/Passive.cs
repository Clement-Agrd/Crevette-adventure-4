namespace Scripts.Passives
{
    public abstract class Passive
    {
        protected Hero user;

        protected Passive(Hero user)
        {
            this.user = user;
        }
    
    }
}
