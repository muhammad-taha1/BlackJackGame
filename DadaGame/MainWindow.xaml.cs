using BlackJackGame;
using BlackJackGame.Cards;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media.Effects;
using System.Collections.Generic;

namespace DadaGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DeckOfCards deck;
        private Person dealer;
        private Person player;
        private int faceup = 0;
        private int lastClick = 0;
        private Group possibleGroup;
        private Group validGroup;
        private List<Group> submittedGroups;
        private Stack<Card> tableCards;
        private bool isSwapping;

        public MainWindow()
        {
            InitializeComponent();

            // create players and deal initial cards
            dealer = new Person("dealer", false);
            player = new Person("player", true);

            dealInitialCards();
            possibleGroup = new Group();
            validGroup = new Group();
            tableCards = new Stack<Card>();
            isSwapping = false;

            // there should be 3 submittedGroups only
            submittedGroups = new List<Group>(3);
        }

        private void dealInitialCards()
        {
            // create and shuffle deck
            deck = new DeckOfCards();
            deck.cut();
            deck.shuffle();
            deck.cut();

            //deal 10 cards to each player. Dealer will be AI here
            for (int i = 0; i < 10; i++)
            {
                dealer.hand.Add(deck.dealFaceUp());
                player.hand.Add(deck.dealFaceUp());
            }

            // update image stacks for each player
            addEntireHandImage(dealer, dealerHand);
            addEntireHandImage(player, playerHand);
        }

        /// <summary>
        /// Function to create card face Images; back image is fixed
        /// </summary>
        /// <param name="c"> Card whose face image has to be assigned </param>
        /// <returns> Image of card's face </returns>
        private Image constructCardImage(Card c)
        {
            ImageSource wpfBitmap = null;
            System.Drawing.Bitmap bitmap = null;
            if (c.isFaceUp)
            {
                // save image as temp file. Should be deleted once the corresponding stack is disposed and image is no longer required
                c.cardFaceImage.Save("C:/Users/Muhammad Taha/Documents/Visual Studio 2015/Projects/BlackJackGame/BlackJackGame/CardImgs/tmp" + faceup + ".jpg");
                bitmap = new System.Drawing.Bitmap("C:/Users/Muhammad Taha/Documents/Visual Studio 2015/Projects/BlackJackGame/BlackJackGame/CardImgs/tmp" + faceup + ".jpg");
                IntPtr hBitmap = bitmap.GetHbitmap();
                wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }

            // creating image object. If card is not faceup, assign the fixed back image as source
            Image card = new Image()
            {
                Name = "card",
                Margin = new Thickness(0),
                Width = 75,
                Height = 90,
                Source = c.isFaceUp ? wpfBitmap : new BitmapImage(new Uri(c.cardBackImage)),
                IsEnabled = true,
            };

            // dispose temp image file once it's assigned to the card
            if (c.isFaceUp)
                bitmap.Dispose();


            // add mouse click and mouse hover event handlers to card's border
            card.MouseDown += (s, e) => toggleEffect(s, e, c);
            card.MouseUp += (s, e) => toggleEffect(s, e, c);

            // add hover effect for swap
            card.MouseEnter += hoverSwap;
            card.MouseLeave += hoverSwap;
            return card;
        }

        // construct card's image to be placed on table
        // note: The only difference here are the functions
        private Image constructCardImageForTable(Card c)
        {
            ImageSource wpfBitmap = null;
            System.Drawing.Bitmap bitmap = null;
            if (c.isFaceUp)
            {
                // save image as temp file. Should be deleted once the corresponding stack is disposed and image is no longer required
                c.cardFaceImage.Save("C:/Users/Muhammad Taha/Documents/Visual Studio 2015/Projects/BlackJackGame/BlackJackGame/CardImgs/tmp" + faceup + ".jpg");
                bitmap = new System.Drawing.Bitmap("C:/Users/Muhammad Taha/Documents/Visual Studio 2015/Projects/BlackJackGame/BlackJackGame/CardImgs/tmp" + faceup + ".jpg");
                IntPtr hBitmap = bitmap.GetHbitmap();
                wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }

            // creating image object. If card is not faceup, assign the fixed back image as source
            Image card = new Image()
            {
                Name = "card",
                Margin = new Thickness(0),
                Width = 75,
                Height = 90,
                Source = c.isFaceUp ? wpfBitmap : new BitmapImage(new Uri(c.cardBackImage)),
                IsEnabled = true,
            };

            // dispose temp image file once it's assigned to the card
            if (c.isFaceUp)
                bitmap.Dispose();

            //card.MouseDown += takeTopCard;
            //card.MouseUp += takeTopCard;

            return card;
        }

        private void takeTopCard(object sender, MouseButtonEventArgs e)
        {
            if (player.hand.Count == 10)
            {
                Card newCard = tableCards.Pop();
                player.hand.Add(newCard);
                addCardImageToHand(playerHand, newCard);

                if (tableCards.Count != 0)
                    tableCardsTopImg.Source = constructCardImageForTable(tableCards.Peek()).Source;
                else
                    tableCardsTopImg.Source = null;
                isSwapping = true;
            }
        }

        private void hoverSwap(object sender, MouseEventArgs e)
        {
            // only perform hover actions when isSwap is true
            if (isSwapping)
            {
                Image cardImg = (Image)sender;
                cardImg.Height = (cardImg.Height == 90) ? 100 : 90;
                cardImg.Width = (cardImg.Width == 75) ? 80 : 75;
            }
        }

        private void toggleEffect(object sender, MouseButtonEventArgs e, Card c)
        {
            Image cardImg = (Image)sender;
            int click = e.Timestamp;
            // actions for adding/removing cards to/from group. These shouldn't occur during a swap

            // toggle card's border image if timestamp detects one click and change group accordingly
            if (cardImg.Effect != null && (click - lastClick > 100))
                removeFromGroup(cardImg);
            // cards can be removed from group during swap, but not added
            else if (click - lastClick > 100 && !isSwapping)
                addToGroup(cardImg, c);


            // click actions for adding card to table
            if (isSwapping)
            {
                player.hand.Remove(c);
                playerHand.Children.Remove(cardImg);
                tableCards.Push(c);
                tableCardsTopImg.Source = constructCardImageForTable(tableCards.Peek()).Source;
                // disable swapping once a swap has been successful
                isSwapping = false;
            }

            lastClick = e.Timestamp;
        }

        private void removeFromGroup(Image card)
        {
            // remove highlights from the current group and create new group
            foreach (Image cardImage in possibleGroup.cardImagesOfGroup)
            {
                cardImage.Effect = null;
            }

            Group groupToRemove = new Group();
            // check and remove if card is in a submitted group
            foreach (Group submittedGroup in submittedGroups)
            {
                if (submittedGroup.cardImagesOfGroup.Contains(card))
                {
                    groupToRemove = submittedGroup;
                    foreach (Image blueCard in submittedGroup.cardImagesOfGroup)
                    {
                        blueCard.Effect = null;
                    }
                }
            }

            submittedGroups.Remove(groupToRemove);
            if (card != null) card.Effect = null;

            updateGroupText();
            //if (validGroup == possibleGroup)
            //{
            //    validGroup.Remove(possibleGroup);
            //}
            possibleGroup = new Group();
            validGroup = new Group();
        }

        // create card's border to add to a possible group
        private void addToGroup(Image card, Card c)
        {
            // submit button should be disabled unless we have a valid group
            submitGroups.IsEnabled = false;

            // create glow effect for car selection
            DropShadowEffect glow = new DropShadowEffect();
            glow.Color = Colors.Yellow;
            glow.ShadowDepth = 3;
            glow.BlurRadius = 5;
            glow.Opacity = 1;
            card.Effect = glow;

            // add to group and check if its valid
            possibleGroup.Add(c, card);
            bool isGroupValid = possibleGroup.checkIfGroupValid();

            if (isGroupValid)
            {
                foreach (Image cardImg in possibleGroup.cardImagesOfGroup)
                {
                    // glow effect to denote valid group
                    glow.ShadowDepth = 6;
                    glow.BlurRadius = 8;
                    glow.Color = Colors.DarkRed;
                    cardImg.Effect = glow;
                }

                validGroup = possibleGroup;
                submitGroups.IsEnabled = true;
            }

            if (!isGroupValid && validGroup.cardsInGroup.Count != 0)
            {
                removeFromGroup(card);
            }
        }

        // add card's image to a player's stack
        private void addEntireHandImage(Person person, StackPanel hand)
        {
            foreach (Card c in person.hand)
            {
                hand.Children.Add(constructCardImage(c));
                faceup++;
            }
        }

        // add group to list of final groups and remove valid and possible groups
        private void submitGroups_Click(object sender, RoutedEventArgs e)
        {
            // mark submitted group as green
            foreach (Image cardImg in validGroup.cardImagesOfGroup)
            {
                DropShadowEffect glow = new DropShadowEffect();
                glow.ShadowDepth = 6;
                glow.BlurRadius = 8;
                glow.Color = Colors.DarkBlue;
                cardImg.Effect = glow;
            }

            // update submitted group list
            submittedGroups.Add(validGroup);
            updateGroupText();

            // delete current possible and valid groups
            validGroup = new Group();
            possibleGroup = new Group();

            submitGroups.IsEnabled = false;

            // check if player won
            validateForVictory();
        }

        // Check's if player won and displays a message
        private void validateForVictory()
        {
            int groupsOfThree = 0;
            int groupsOfFour = 0;
            foreach (Group group in submittedGroups)
            {
                if (group.cardsInGroup.Count == 3) groupsOfThree++;
                if (group.cardsInGroup.Count == 4) groupsOfFour++;
            }

            if (groupsOfFour == 1 && groupsOfThree == 2)
            {
                MessageBox.Show("You Won!");
                mainGrid.IsEnabled = false;
            }
        }

        private void updateGroupText()
        {
            // update submitted group text
            string groupDisp = "";
            foreach (Group group in submittedGroups)
            {
                foreach (Card card in group.cardsInGroup)
                {
                    groupDisp += " " + card.shortString() + ",";
                }
                groupDisp = groupDisp.Remove(groupDisp.Length - 1, 1);
                groupDisp += ";";
            }
            groupDisplay.Content = groupDisp;
        }

        // deal card to player, only if the player has 10 cards
        private void deal_Click(object sender, RoutedEventArgs e)
        {
            if (player.hand.Count == 10)
            {
                Card newCard = deck.dealFaceUp();
                player.hand.Add(newCard);
                addCardImageToHand(playerHand, newCard);

                isSwapping = true;
            }

            //if (player.hand.Count == 11)
            //{
            //    isSwapping = true;
            //}
        }

        private void addCardImageToHand(StackPanel hand, Card newCard)
        {
            hand.Children.Add(constructCardImage(newCard));
        }
    }
}
