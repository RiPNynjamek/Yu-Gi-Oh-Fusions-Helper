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
        private Card _firstSelectedCard;
        private Card _secondSelectedCard;
        private Card _thirdSelectedCard;
        private Card _fourthSelectedCard;
        private Card _fifthSelectedCard;

        private ObservableCollection<Combination> _availableCombinations;

        public Dictionary<string, string[]> Text { get; set; }
        public List<Combination> Associations { get; set; }
        public List<Card> AllCards { get; set; }

        public Card FirstSelectedCard
        {
            get { return _firstSelectedCard; }
            set
            {
                _firstSelectedCard = value;
                SelectedAssociationChanged();
                OnPropertyChanged("FirstSelectedAssociation");
            }
        }

        public Card SecondSelectedCard
        {
            get { return _secondSelectedCard; }
            set
            {
                _secondSelectedCard = value;
                SelectedAssociationChanged();
                OnPropertyChanged("SecondSelectedAssociation");
            }
        }
        public Card ThirdSelectedCard
        {
            get { return _thirdSelectedCard; }
            set
            {
                _thirdSelectedCard = value;
                SelectedAssociationChanged();
                OnPropertyChanged("ThirdSelectedAssociation");
            }
        }
        public Card FourthSelectedCard
        {
            get { return _fourthSelectedCard; }
            set
            {
                _fourthSelectedCard = value;
                SelectedAssociationChanged();
                OnPropertyChanged("FourthSelectedAssociation");
            }
        }
        public Card FifthSelectedCard
        {
            get { return _fifthSelectedCard; }
            set
            {
                _fifthSelectedCard = value;
                SelectedAssociationChanged();
                OnPropertyChanged("FifthSelectedAssociation");
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
            AllCards = ex.CardsList;
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
               var data = (new Card[] { FirstSelectedCard, SecondSelectedCard, ThirdSelectedCard, FourthSelectedCard, FifthSelectedCard })
                            .Where(x => x != null).ToList();
                foreach (var item in Associations)
                {
                    if (data.Contains(item.FirstCard))
                    {
                        AvailableCombinations.Add(item);
                    }
                }
                RestrainCombinations(data);
            }
            catch (NullReferenceException e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }

        private void RestrainCombinations(List<Card> hand)
        {

            var bite =new ObservableCollection<Combination>();

            foreach (var item in AvailableCombinations)
            {
                bite.Add(item);
            }

            foreach (var item in bite)
            {
                if (!(hand.Exists(e => e.Name == item.FirstCard.Name) && (hand.Exists(e => e.Name == item.SecondCard.Name))))
                {
                    AvailableCombinations.Remove(item);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}