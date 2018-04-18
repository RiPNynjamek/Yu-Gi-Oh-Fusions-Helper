using System;

namespace Fusions.DataModel
{
    public static class CardFactory
    {
        public static Card CreateCard(string name, CardType cardType, int[] monsterStats)
        {
            switch (cardType)
            {
                case CardType.Base:
                case CardType.FusionResult:
                    return new Monster(name, monsterStats[0], monsterStats[1], cardType);
                case CardType.Equipment:
                    return new Equipment(name);
                case CardType.Magic:
                    return new Magic(name);
                case CardType.Trap:
                    return new Trap(name);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}