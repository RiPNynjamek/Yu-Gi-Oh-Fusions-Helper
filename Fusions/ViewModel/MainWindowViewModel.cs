using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using Fusions.DataModel;
using System.Linq;
using Fusions.Tools;

namespace Fusions.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Dictionary<string, string[]> Text { get; set; }
        //public Tuple<Monster, Monster, Monster> MonstersList { get; set; }
        public List<Association> Associations { get; set; }
        public MainWindowViewModel()
        {
            var fp = new FileParser();
            var ex = new Extracter();
            Text= fp.GetFileContent(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "FusionsFile.txt"));
            ex.ExtractData(Text);
            Associations = ex.Fusions;
            //ExtractFusionCombos(Text)
        }

        

        


        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
