using MTGCardSearch.Model;
using MTGCardSearch.ViewModel.Commands;
using MTGCardSearch.ViewModel.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace MTGCardSearch.ViewModel
{
    class CardSearchVM : INotifyPropertyChanged
    {
        private const string cardBackUri = "/View/MTGCardBack.png";
        public event PropertyChangedEventHandler PropertyChanged;

        #region Public Properties
        private string query;
        public string Query
        {
            get { return query; }
            set 
            { 
                query = value;
                OnPropertyChanged("Query");
            }
        }

        private string statusBarText;

        public string StatusBarText
        {
            get { return statusBarText; }
            set 
            { 
                statusBarText = value;
                OnPropertyChanged("StatusBarText");
            }
        }


        private string cardImageUrl;
        public string CardImageUrl
        {
            get { return cardImageUrl; }
            set
            {
                cardImageUrl = value;
                OnPropertyChanged("CardImageUrl");
            }
        }

        private Card selectedCard;
        public Card SelectedCard
        {
            get { return selectedCard; }
            set 
            { 
                selectedCard = value;
                CardImageUrl = selectedCard?.imageUrl ?? cardBackUri;
                OnPropertyChanged("SelectedCard");
            }
        }


        public ObservableCollection<Card> Cards { get; private set; }
        public SearchCommand SearchCommand { get; private set; }
        #endregion

        public CardSearchVM()
        {
            SearchCommand = new SearchCommand(this);
            Cards = new ObservableCollection<Card>();

            if (true == DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                Cards.Add(new Card()
                {
                    name = "Okina, Temple to the Mommas",
                    manaCost = "{3}{W}{W}"
                });
            }
            CardImageUrl = cardBackUri;
            StatusBarText = $"Found {Cards.Count} results.";
        }

        public async void MakeQuery()
        {
            StatusBarText = "Querying MTG Database...";
            var cards = await MagicAPIHelper.GetCards(Query);

            Cards.Clear();
            foreach(Card card in cards.cards)
            {
                if(card.multiverseid != 0)
                    Cards.Add(card);
            }
            StatusBarText = $"Found {Cards.Count} results.";
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
