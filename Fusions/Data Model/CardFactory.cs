using System;

namespace Fusions.DataModel
{
    public static class CardFactory
    {
        public static Card CreateCard(string name, CardCategory cardType, int[] monsterStats)
        {
            switch (cardType)
            {
                case CardCategory.Base:
                case CardCategory.FusionResult:
                    return new Monster(name, monsterStats[0], monsterStats[1], cardType);
                case CardCategory.Equipment:
                    return new Equipment(name);
                case CardCategory.Magic:
                    return new Magic(name);
                case CardCategory.Trap:
                    return new Trap(name);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}