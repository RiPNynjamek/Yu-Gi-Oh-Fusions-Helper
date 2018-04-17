﻿using Fusions.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //var monstersList = new List<Monster>();
            for (int i = 0; i < fusionsList.Count; i++)
            {
                var currentLine = fusionsList[i];
                // string nextLine;
                if (currentLine.StartsWith("----"))
                    continue;
                if (fusionsList[i + 2].StartsWith("======"))
                    return fusions;
                if (currentLine == "" && fusionsList[i + 2].StartsWith("-------------------"))
                {
                    var monster = new Monster(fusionsList[i + 1], CardType.Base);
                    //  monstersList.Add(monster);
                    fusions.Add(new Association(monster));
                    //MonstersList = Tuple.Create(monster, null, null);
                    i++;
                    continue;
                }
                if (currentLine.Contains('='))
                {
                    var instances = currentLine.Split('=');
                    //var secondinstance = currentLine.Split('=')[1];
                    var type = CheckCardType(instances);
                    var firstInstance = CardFactory.CreateCard(instances[0], type[0]);//new Monster(instances[0], type[0]);
                    var secondInstance = CardFactory.CreateCard(instances[1], type[1]);//new Monster(instances[1], CardType.FusionResult);

                  //  var dummyMonster = fusions.Where(e => e.BaseMonster.Name == monster.Name).FirstOrDefault();
                    AddNewCombination(fusions, firstInstance, secondInstance);
                }
            }
            return fusions;
        }

        private List<CardType> CheckCardType(string[] instances)
        {
            List<CardType> cardTypes = new List<CardType>();
            for (int i = 0; i < instances.Length; i++)
            {
                if (instances[i].Contains("(Trap)"))
                {
                    cardTypes.Add(CardType.Trap);
                }
                else if (instances[i].Contains("(Equip"))
                {
                    cardTypes.Add(CardType.Equipment);
                }
                else if (instances[i].Contains("(Magic)"))
                {
                    cardTypes.Add(CardType.Magic);
                }
                else
                {
                    if(i == 0)
                        cardTypes.Add(CardType.Base);
                    else
                    {
                        cardTypes.Add(CardType.FusionResult);
                    }
                }
            }
            return cardTypes;
        }

        private void AddNewCombination(List<Association> fusions, Card AssociatedCard, Card fusionResult)
        {
            var lastItem = fusions.Last();
            lastItem.Combination.Add(new Tuple<Card, Card >(AssociatedCard, fusionResult));            
        }
    }
}