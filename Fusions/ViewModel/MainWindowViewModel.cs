using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using Fusions.DataModel;
using System.Linq;
using Fusions.DataModel;
using Fusions.Tools;

namespace Fusions.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Dictionary<string, string[]> Text { get; set; }
        public Tuple<Monster, Monster, Monster> MonstersList { get; set; }

        public MainWindowViewModel()
        {
            FileParser fp = new FileParser();
            Text= fp.GetFileContent(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "FusionsFile.txt"));
            ExtractData(Text);
            //ExtractFusionCombos(Text)
        }

        private void ExtractData(Dictionary<string, string[]> text)
        {
            foreach (var item in text)
            {
                var subItem = new ObservableCollection<string>();
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
                    MonstersList.Add(monster, new List<Monster>());
                    MonstersList = Tuple.Create( monster, null, null );
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


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
