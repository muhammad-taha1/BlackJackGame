using BlackJackGame.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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

        public MainWindow()
        {
            InitializeComponent();
            dealInitialCards();
        }

        private void dealInitialCards()
        {
            deck = new DeckOfCards();
            round = new GameRound(deck);

            //Console.WriteLine(round.dealer.hand[0].ToString());
            round.dealer.hand[0].cardFaceImage.Save("C:/Users/Muhammad Taha/Documents/Visual Studio 2015/Projects/BlackJackGame/BlackJackGame/CardImgs/tmp.jpg");
            Image dealerCard = new Image()
            {
                Name = "card",
                Margin = new Thickness(0, 0, 0, 0),
                Width = 75,
                Height = 90,
                Source = new BitmapImage(new Uri("C:/Users/Muhammad Taha/Documents/Visual Studio 2015/Projects/BlackJackGame/BlackJackGame/CardImgs/tmp.jpg")),
                IsEnabled = true,

               
            };

            (this.Content as Grid).Children.Add(dealerCard);
            //playGame();
        }

       

        public static void playGame()
        {
            string inpput = "";
            while (inpput != "stay" && !round.player.bustFlag)
            {
                Console.Write("hit or stay?");

                inpput = Console.ReadLine();
                Console.Write("");


                if (inpput == "hit")
                {
                    round.player.hit(deck);
                    Console.WriteLine("players hand: " + round.player.getHand());
                    Console.WriteLine(round.player.getHandValue());


                }

                else
                {
                    // player calls stay
                    round.dealer.turnAllCardsFaceUp();
                    Console.WriteLine("dealers hand: " + round.dealer.getHand());
                    Console.WriteLine(round.dealer.getHandValue());

                }
            }

            if (round.player.bustFlag)
            {
                Console.WriteLine("player busted!!");
            }

            else
            {
                while (!round.dealer.bustFlag && (round.dealer.getHandValue() < round.player.getHandValue()))
                {
                    round.dealer.hand.Add(deck.dealFaceUp());
                    Console.WriteLine("dealers hand: " + round.dealer.getHand());
                    Console.WriteLine(round.dealer.getHandValue());
                }

                if (round.dealer.bustFlag)
                {
                    Console.WriteLine("dealer busted!");
                }
                else
                {
                    Console.WriteLine("player lost");
                }
            }


            Console.Write("Press any key to exit...");
            Console.ReadLine();
        }

       
    }
}
