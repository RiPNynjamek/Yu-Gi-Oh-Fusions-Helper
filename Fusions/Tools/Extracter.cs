using Fusions.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusions.Tools
{
    public class Extracter
    {
        public List<Association> Associations { get; set; }

        public Extracter()
        {
            Associations = new List<Association>();
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

            GetFusionByMonster(fusionsList);
        }

        private void GetFusionByMonster(List<string> fusionsList)
        {
            //var monstersList = new List<Monster>();
            for (int i = 0; i < fusionsList.Count; i++)
            {
                var currentLine = fusionsList[i];
                // string nextLine;
                if (currentLine.StartsWith("----"))
                    continue;
                if (currentLine == "" && fusionsList[i + 2].StartsWith("-------------------"))
                {
                    Monster monster = new Monster(fusionsList[i + 1], CardType.Base);
                    //  monstersList.Add(monster);
                    Associations.Add(new Association(monster, null, null));
                    MonstersList = Tuple.Create(monster, null, null);
                    i++;
                    continue;
                }
                if (currentLine.Contains('='))
                {
                    Monster monster = new Monster(currentLine.Split('=')[0], CardType.Base);
                    Monster fusionResult = new Monster(currentLine.Split('=')[1], CardType.FusionResult);
                    if (MonstersList.ContainsKey(monster))
                    {
                        MonstersList[monster].Add();
                    }

                }
                //Monster monster2 = new Monster(currentLine.Substring(0, currentLine.IndexOf("(")));
            }
        }
    }
}
