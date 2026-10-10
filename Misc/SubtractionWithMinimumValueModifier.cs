namespace Grimoire.Misc
{
    public class SubtractionWithMinimumValueModifier(bool dmgDealt, int toDecrease, int minimum) : IntValueModifier(dmgDealt ? 13 : 69)
    {
        public override int Modify(int value)
        {
            if (value < minimum)
                return value;
            
            return Mathf.Max(value - toDecrease, minimum);
        }
    }
}