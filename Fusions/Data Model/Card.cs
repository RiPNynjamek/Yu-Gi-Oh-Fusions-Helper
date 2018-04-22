namespace Fusions.DataModel
{
    public abstract class Card
    {
        public abstract string Name { get; set; }
        public abstract CardCategory Type { get; set; }
    }
}
