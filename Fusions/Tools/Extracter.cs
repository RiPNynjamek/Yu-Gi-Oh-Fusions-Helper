using Fusions.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fusions.Tools
{
    public class Extracter
    {
        public List<Association> Fusions { get; set; }

        public Extracter()
        {
            Fusions = new List<Association>();
        }

        public void ExtractData(Dictionary<string, string[]> text)
        {
            foreach (var item in text)
            {
                var subItem = new List<string>();
                for (int i = 0; i < item.Value.Length; i++)
                {
                    subItem.Add(item.Value[i]);
                }
            }
            var fusionsList = text.Values.ElementAt(5).ToList();
            fusionsList.RemoveRange(0, 12);

            Fusions = GetFusionByMonster(fusionsList);
        }

        private List<Association> GetFusionByMonster(List<string> fusionsList)
        {
            var fusions = new List<Association>();

            for (int i = 0; i < fusionsList.Count; i++)
            {
                var currentLine = fusionsList[i];

                if (currentLine.StartsWith("----"))
                    continue;
                if (fusionsList[i + 2].StartsWith("======"))
                    return fusions;
                if (currentLine == "" && fusionsList[i + 2].StartsWith("-------------------"))
                {
                    //var monster = new Monster(fusionsList[i + 1], CardType.Base);

                    //fusions.Add(new Association(monster));
                    var mstr = new Monster(GetShortName(fusionsList[i + 1]), CardType.Base);

                    int[] numbers = GetMonsterStats(fusionsList[i + 1]);
                    mstr.Attack = numbers[0];
                    mstr.Defense = numbers[1];
                    fusions.Add(new Association(mstr));
                    i++;
                    continue;
                }
                if (currentLine.Contains('='))
                {
                    var instances = currentLine.Split('=');
                    List<Card> monsters = new List<Card>() ;
                    var isFirst = true;
                    for (int j = 0; j < instances.Length; j++)
                    {
                        var Type = CheckCardType(instances[j], isFirst);
                        var cardName = GetShortName(instances[j]);
                        monsters.Add(CardFactory.CreateCard(cardName, Type, GetMonsterStats(instances[j])));
                        isFirst = false;
                    }
                    //var type = CheckCardType(instances);
                    //var firstInstance = CardFactory.CreateCard(instances[0], type[0]);
                    //var secondInstance = CardFactory.CreateCard(instances[1], type[1]);

                    AddNewCombination(fusions, monsters[0], monsters[1]);
                }
            }
            return fusions;
        }

        private string GetShortName(string v)
        {
            return v.Split('(')[0];
        }

        private int[] GetMonsterStats(string cardStats)
        {
            int[] stats = new int[2];
            string[] testValues = { "Trap", "Equipment", "Magic" };
            if (cardStats.Contains("Trap") || cardStats.Contains("Equip") || cardStats.Contains("Magic") 
                || cardStats.Contains("(?)") || cardStats.Contains("Ritual"))
            {
                return stats;
            }
            if (cardStats.Contains("("))
            {
                var output = cardStats.Split('(', ')')[1];
                for (int i = 0; i < stats.Length; i++)
                {
                    try
                    {
                        stats[i] = Convert.ToInt32(output.Split('/')[i]);
                    }
                    catch (FormatException)
                    {
                        return stats;
                    }
                }
            }
            return stats;
        }

        private CardType CheckCardType(string stringToCheck, bool isFirst)
        {
            //List<CardType> cardTypes = new List<CardType>();
            if (stringToCheck.Contains("(Trap)"))
            {
                return CardType.Trap;
            }
            else if (stringToCheck.Contains("(Equip"))
            {
                return CardType.Equipment;
            }
            else if (stringToCheck.Contains("(Magic)"))
            {
                return CardType.Magic;
            }
            else
            {
                if(isFirst)
                    return CardType.Base;
                else
                {
                    return CardType.FusionResult;
                }
            }         
        }

        private void AddNewCombination(List<Association> fusions, Card AssociatedCard, Card fusionResult)
        {
            var lastItem = fusions.Last();

            //Ignore duplicates
            if(lastItem.Combination.Any(e => e.Key == AssociatedCard))
            {
                return;
            }
            lastItem.Combination.Add(AssociatedCard, fusionResult);
            //lastItem.Combination.Add(new KeyValuePair<Card, Card>(AssociatedCard, fusionResult));            
        }
    }
}
