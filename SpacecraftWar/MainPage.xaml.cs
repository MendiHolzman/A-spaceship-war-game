using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SpacecraftWar
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Border[] _UiEnemies = new Border[10];

        Ellipse[] ellipses = new Ellipse[4];

        private DispatcherTimer _timer;
        private DispatcherTimer _timer1;
        Ellipse el;
        private GameManager _gameManager;


        public MainPage()
        {
            InitializeComponent();

            _UiEnemies[0] = Enemy1;
            _UiEnemies[1] = Enemy2;
            _UiEnemies[2] = Enemy3;
            _UiEnemies[3] = Enemy4;
            _UiEnemies[4] = Enemy5;
            _UiEnemies[5] = Enemy6;
            _UiEnemies[6] = Enemy7;
            _UiEnemies[7] = Enemy8;
            _UiEnemies[8] = Enemy9;
            _UiEnemies[9] = Enemy10;

            ellipses[0] = el1;
            ellipses[1] = el2;
            ellipses[2] = el3;
            ellipses[3] = el4;

           











            _gameManager = new GameManager((int)Window.Current.Bounds.Width, (int)Window.Current.Bounds.Height);
            CheckStatusAsync();
            Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated += OnKeyDown;
        }

        public void v()
        {
            double a = Canvas.GetTop(MyPlayer);
            double b = Canvas.GetLeft(MyPlayer);

            for (int i = 0; i < ellipses.Length; i++)
            {
                double c = Canvas.GetTop(ellipses[i]);
                double d = Canvas.GetLeft(ellipses[i]);
                if (IsTooClose(a, c, 40) && IsTooClose(b, d, 40))
                {
                    ellipses[i].Visibility = Visibility.Collapsed;
                }

            }

        }
        private async void CheckStatusAsync()
        {
            GameSituation gameSituation = _gameManager.GetGameSituation();

            SetLocation(MyPlayer, gameSituation.MyPlayer);

            DrawEnemies(gameSituation.Enemies);



            switch (gameSituation.GameStatus)
            {
                case GameStatus.Win:
                    _timer.Stop();
                    await new MessageDialog("You won(-:", "Well done!!! Let's see you play again ...").ShowAsync();
                    break;
                case GameStatus.Loss:
                    _timer.Stop();
                    await new MessageDialog("you lose ); ", "Never mind, try again ...").ShowAsync();
                    break;
            }
        }

        public void c()
        {
            if (el == null)
            {
                el = new Ellipse();
                el.Width = 40;
                el.Height = 40;
                el.Fill = new SolidColorBrush(Colors.Green);
                MyCanvas.Children.Add(el);
                Canvas.SetTop(el, Canvas.GetTop(MyPlayer));
                Canvas.SetLeft(el, Canvas.GetLeft(MyPlayer));
                el.Visibility = Visibility.Collapsed;
            }
        }


        private void OnKeyDown(CoreDispatcher sender, AcceleratorKeyEventArgs args)
        {
            if (_timer == null || !_timer.IsEnabled)
            {
                _gameManager.StartGame();
                _timer = new DispatcherTimer();
                _timer.Interval = TimeSpan.FromMilliseconds(GameManager.INTERVAL);
                _timer.Tick += ss1;
                _timer.Start();
            }

            _timer1 = new DispatcherTimer();
            _timer1.Interval = new TimeSpan(0, 0, 0, 0, 60);
            _timer1.Tick += _timer1_Tick1;
            _timer1.Start();





            switch (args.VirtualKey)
            {
                case VirtualKey.Space:

                    




                    break;
            }

            switch (args.VirtualKey)
            {
                case VirtualKey.Left:
                    _gameManager.MovePlayer(Direction.Left);
                    break;
                case VirtualKey.Up:
                    _gameManager.MovePlayer(Direction.Up);
                    break;
                case VirtualKey.Right:
                    _gameManager.MovePlayer(Direction.Right);
                    break;
                case VirtualKey.Down:
                    _gameManager.MovePlayer(Direction.Down);
                    break;
            }


            CheckStatusAsync();
        }

        private void _timer1_Tick1(object sender, object e)
        {         
            v();
            CheckWin();
        }
        public  void CheckWin()
        {
            
            if (el1.Visibility==Visibility.Collapsed&&
                el2.Visibility == Visibility.Collapsed&&
                el3.Visibility == Visibility.Collapsed &&
                el4.Visibility == Visibility.Collapsed )
            {
                _timer.Stop();
                for (int i = 0; i < _UiEnemies.Length; i++)
                {
                    _UiEnemies[i].Visibility = Visibility.Collapsed;
                    
                   
                    

                }
                
                MyPlayer.Width = 400;
                MyPlayer.Height = 400;
                Canvas.SetTop(MyPlayer, 250);
                Canvas.SetLeft(MyPlayer, 600);
            }

        }
        private void ss1(object sender, object args)
        {
            Task.Delay(100).Wait();

            CheckStatusAsync();


        }

        private void DrawEnemies(EnemySpaceships[] enemies)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                Border border = _UiEnemies[i];
                SetLocation(border, enemies[i]);

                border.Visibility = Visibility.Visible;
            }

            for (int i = enemies.Length; i < _UiEnemies.Length; i++)
            {
                _UiEnemies[i].Visibility = Visibility.Collapsed;
            }
        }

        private void SetLocation(UIElement UIElement, GameTool gameTool)
        {
            Canvas.SetLeft(UIElement, gameTool.X);
            Canvas.SetTop(UIElement, gameTool.Y);
        }

        private void SetLocation1(UIElement UIElement, UIElement UIElement1)
        {
            Canvas.SetLeft(UIElement, Canvas.GetLeft(UIElement1));
            Canvas.SetTop(UIElement, Canvas.GetTop(UIElement1));
        }








        private void _timer1_Tick(object sender, object e)
        {
            double x = Canvas.GetLeft(el);
            double y = Canvas.GetTop(el);



            Canvas.SetTop(el, y - 20);
            el.Visibility = Visibility.Visible;



        }
        private bool IsTooClose(double value1, double value2, double delta)
        {
            return Math.Abs(value1 - value2) <= delta;
        }
    }
}
