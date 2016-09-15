using BlackJackGame.Cards;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlackJackGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static GameRound round;
        private static DeckOfCards deck;
        private int faceup = 0;
        private MessageBoxResult messageBoxRes;

        public MainWindow()
        {
            deck = new DeckOfCards();
            messageBoxRes = MessageBoxResult.None;
            InitializeComponent();
            dealInitialCards();
            updateHandScore();
            dealerMoney.Text = "$100";
            playerMoney.Text = "$100";
        }

        private void updateMoney(bool playerWon)
        {
            if (playerWon)
            {
                string amnt = playerMoney.Text;
                amnt = amnt.Replace("$", "");
                int newAmount = Int16.Parse(amnt) + 10;
                playerMoney.Text = "$" + newAmount.ToString();

                amnt = dealerMoney.Text;
                amnt = amnt.Replace("$", "");
                newAmount = Int16.Parse(amnt) - 10;
                dealerMoney.Text = "$" + newAmount.ToString();
            }
            else
            {
                string amnt = playerMoney.Text;
                amnt = amnt.Replace("$", "");
                int newAmount = Int16.Parse(amnt) - 10;
                playerMoney.Text = "$" + newAmount.ToString();

                amnt = dealerMoney.Text;
                amnt = amnt.Replace("$", "");
                newAmount = Int16.Parse(amnt) + 10;
                dealerMoney.Text = "$" + newAmount.ToString();
            }
        }

        private void dealInitialCards()
        {
            round = new GameRound(deck);

            addEntireHandImage(round.dealer, dealerHand);
            addEntireHandImage(round.player, playerHand);
            updateHandScore();

            if (messageBoxRes == MessageBoxResult.OK)
            {
                if (round.player.getHandValue() == 21 || round.dealer.bustFlag)
                {
                    updateMoney(true);
                    newRound();
                }
                else if (round.dealer.getHandValue() == 21 || round.player.bustFlag)
                {
                    updateMoney(false);
                    newRound();
                }
            }
        }

        private Image constructCardImage(Card c)
        {
            ImageSource wpfBitmap = null;
            System.Drawing.Bitmap bitmap = null;
            if (c.isFaceUp)
            {
                c.cardFaceImage.Save("C:/Users/Muhammad Taha/Documents/Visual Studio 2015/Projects/BlackJackGame/BlackJackGame/CardImgs/tmp" + faceup + ".jpg");
                bitmap = new System.Drawing.Bitmap("C:/Users/Muhammad Taha/Documents/Visual Studio 2015/Projects/BlackJackGame/BlackJackGame/CardImgs/tmp" + faceup + ".jpg");
                IntPtr hBitmap = bitmap.GetHbitmap();
                wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            }

            Image card = new Image()
            {
                Name = "card",
                Margin = new Thickness(0, 0, 0, 0),
                Width = 75,
                Height = 90,
                Source = c.isFaceUp ? wpfBitmap : new BitmapImage(new Uri(c.cardBackImage)),
                IsEnabled = true,
            };

            if (c.isFaceUp)
                bitmap.Dispose();

            return card;

        }

        private void addEntireHandImage(Person person, StackPanel hand)
        {
            foreach (Card c in person.hand)
            {
                hand.Children.Add(constructCardImage(c));
                faceup++;
            }
        }

        private void addCardImageToHand(StackPanel hand, Card newCard)
        {
            hand.Children.Add(constructCardImage(newCard));
        }

        private void updateHandScore()
        {
            dealerScore.Text = round.dealer.getHandValue().ToString();
            if (round.dealer.bustFlag)
            {
                updateMoney(true);
                messageBoxRes = MessageBox.Show("Dealer busted!");
            }
            else if (round.dealer.getHandValue() == 21)
            {
                updateMoney(false);
                messageBoxRes = MessageBox.Show("Dealer Won!");
            }

            playerScore.Text = round.player.getHandValue().ToString();
            if (round.player.bustFlag)
            {
                updateMoney(false);
                messageBoxRes = MessageBox.Show("Player busted!");
            }
            else if (round.player.getHandValue() == 21)
            {
                updateMoney(true);
                messageBoxRes = MessageBox.Show("Player Won!");
            }
        }

        private void hit_Click(object sender, RoutedEventArgs e)
        {
            Card newCard = round.player.hit(deck);
            addCardImageToHand(playerHand, newCard);
            updateHandScore();

            if (messageBoxRes == MessageBoxResult.OK)
            {
                // player busts here
                newRound();
            }
        }

        private void stay_Click(object sender, RoutedEventArgs e)
        {
            dealerHand.Children.RemoveRange(0, dealerHand.Children.Count);
            round.dealer.turnAllCardsFaceUp();
            addEntireHandImage(round.dealer, dealerHand);
            // initial card turned over
            updateHandScore();
            if (round.dealer.getHandValue() > round.player.getHandValue())
            {
                updateMoney(false);
                messageBoxRes = MessageBox.Show("Dealer Won!");
            }

            while (!round.dealer.bustFlag && round.dealer.getHandValue() <= round.player.getHandValue())
            {
                Card newCard = deck.dealFaceUp();
                round.dealer.hand.Add(newCard);
                addCardImageToHand(dealerHand, newCard);
                updateHandScore();
                Thread.Sleep(100);

                if (!(messageBoxRes == MessageBoxResult.OK) && round.dealer.getHandValue() > round.player.getHandValue())
                {
                    updateMoney(false);
                    messageBoxRes = MessageBox.Show("Dealer Won!");
                    break;
                }
            }

            newRound();
        }

        private void newRound()
        {
            disposeStacks();
            messageBoxRes = MessageBoxResult.None;
            if (deck.deck.Count() > 4)
            {
                dealInitialCards();
                updateHandScore();
            } 
            else
            {
                var newGame = MessageBox.Show("Continue game with new deck?", "Game Over", MessageBoxButton.YesNo);

                switch (newGame)
                {
                    case (MessageBoxResult.Yes):
                        deck = new DeckOfCards();
                        dealInitialCards();
                        updateHandScore();
                        break;
                    case (MessageBoxResult.No):
                        hit.IsEnabled = false;
                        stay.IsEnabled = false;
                        break;
                }
            }

        }

        private void disposeStacks()
        {
            foreach (Image img in playerHand.Children)
            {
                img.Source = null;
            }
            foreach (Image img in dealerHand.Children)
            {
                img.Source = null;
            }

            round.player.disposeHand();
            round.dealer.disposeHand();

            playerHand.Children.Clear();
            dealerHand.Children.Clear();

            for (int i = 0; i < faceup; i++)
            {
                File.Delete("C:/Users/Muhammad Taha/Documents/Visual Studio 2015/Projects/BlackJackGame/BlackJackGame/CardImgs/tmp" + i + ".jpg");
            }

            faceup = 0;
        }
    }
}
