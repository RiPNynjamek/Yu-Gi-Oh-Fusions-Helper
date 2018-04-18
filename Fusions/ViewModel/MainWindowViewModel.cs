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
        private Association _selectedAssociation;
        private ObservableCollection<Combination> _availableCombinations;

        public Dictionary<string, string[]> Text { get; set; }
        public List<Association> Associations { get; set; }

        public Association SelectedAssociation
        {
            get { return _selectedAssociation; }
            set
            {
                _selectedAssociation = value;
                SelectedAssociationChanged();
                OnPropertyChanged("SelectedAssociation");
            }
        }

        public ObservableCollection<Combination> AvailableCombinations
        {
            get { return _availableCombinations; }
            set
            {
                _availableCombinations = value;
                OnPropertyChanged("AvailableCombinations");
            }
        }

        public MainWindowViewModel()
        {
            AvailableCombinations = new ObservableCollection<Combination>();
            var fp = new FileParser();
            var ex = new Extracter();
            Text = fp.GetFileContent(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                                    , "FusionsFile.txt"));
            ex.ExtractData(Text);
            Associations = ex.Fusions;
        }

        private void SelectedAssociationChanged()
        {
            if (AvailableCombinations.Count > 0)
            {
                AvailableCombinations.Clear();
            }
            
            try
            {
                var temp = Associations.Where(e => e.BaseMonster.Name == SelectedAssociation.BaseMonster.Name);
                foreach (var item in temp)
                {
                    foreach (var entry in item.Combination)
                    {
                        AvailableCombinations.Add(new Combination(entry.Key, entry.Value));
                    }
                }
            }
            catch (NullReferenceException)
            {
                return;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Combination
    {
        public Combination(string key, string value)
        {
            Key = key;
            Value = value;
        }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
