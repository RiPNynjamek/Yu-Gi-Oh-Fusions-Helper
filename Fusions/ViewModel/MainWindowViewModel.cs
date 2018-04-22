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
            CheckFurtherFusions(hand);
            RemoveDuplicates();
        }

        private void RemoveDuplicates()
        {
            var bite = new ObservableCollection<Combination>();

            foreach (var item in AvailableCombinations)
            {
                bite.Add(item);
            }
            foreach (var item in bite)
            {
                var comb1 = AvailableCombinations.Where(e => e.FirstCard.Name == item.FirstCard.Name && e.SecondCard.Name == item.SecondCard.Name).FirstOrDefault();
                var comb2 = AvailableCombinations.Where(e => e.FirstCard.Name == item.SecondCard.Name && e.SecondCard.Name == item.FirstCard.Name).FirstOrDefault();
                if (comb1 != null && comb2 != null)
                    AvailableCombinations.Remove(item);
            }
            AvailableCombinations = new ObservableCollection<Combination>(AvailableCombinations.Distinct());
        }

        private void CheckFurtherFusions(List<Card> hand)
        {
            var temp = new ObservableCollection<Combination>();
            foreach (var item in AvailableCombinations)
            {
                foreach (var card in hand)
                {
                    var test = Associations.Where(e => e.FirstCard == item.FusionResult && hand.Contains(e.FirstCard));
                }
                //var yolo = Associations.Where(j => hand.Any(o => o == j.FirstCard));
                var tes1t = Associations.Where(e => e.FirstCard.Name == item.FusionResult.Name && hand.Exists(p => p.Name == e.SecondCard.Name)).ToList();
                if (tes1t != null)
                    temp = new ObservableCollection<Combination>(temp.Concat(tes1t));
                var testing = Associations.Where(n => n.FirstCard.Name == hand.Find(e => e.Name == n.FirstCard.Name).Name && n.SecondCard.Name == item.FusionResult.Name);// 
                //if(Associations.Where(e => e.FirstCard == item.FusionResult) != null && Associations.Where(a => a.SecondCard == item.SecondCard) != null)
                if (Associations.Exists(n => n.SecondCard == item.FirstCard && n.FirstCard == item.FusionResult))
                {
                    var test = Associations.Where(n => n.SecondCard == item.SecondCard && n.FirstCard == item.FusionResult).FirstOrDefault();
                    temp.Add(Associations.Where(n => n.SecondCard == item.SecondCard && n.FirstCard == item.FusionResult).FirstOrDefault());
                }
            }
            AvailableCombinations = new ObservableCollection<Combination>(AvailableCombinations.Concat(temp));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}