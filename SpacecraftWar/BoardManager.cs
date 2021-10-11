using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SpacecraftWar
{
    class BoardManager
    {
        const double StepSize = 80;
        const double difference = 50;
        private int _width;
        private int _height;
        

        MyPlayer _myPlayer;
        EnemySpaceships[] _es;
        Shoots[] _shoots;

        int shootsCount;
        int enemiesCount;

        public BoardManager(int width, int height)
        {
            Init(width, height);
            _height = height;
            _width = width;
        }

        public void Init(int width, int height)
        {
            _myPlayer = new MyPlayer(width / 2.2, height/1.3);

            shootsCount = 10;
            _shoots = new Shoots[shootsCount];

            
            for (int i = 0; i < shootsCount; i++)
            {
                _shoots[i] = new Shoots(width / 2.2, height / 1.3);
            }
            
            
            

            enemiesCount = 10;
            _es = new EnemySpaceships[enemiesCount];

            double HeightEnemiesAtFirst = height / 10;
            double safeDistance = width / 10;
            double x, y;
            for (int i = 0; i < enemiesCount; i++)
            {
                if (i == 0)
                {
                    x = 70.5;
                }
                else
                {
                    x = _es[i - 1].X + safeDistance;
                }
                
                y = HeightEnemiesAtFirst;
                _es[i] = new EnemySpaceships(x, y);
            }
        }
             
        public GameSituation GetGameSituation()
        {
            GameSituation gameSituation = new GameSituation();
            gameSituation.MyPlayer = _myPlayer;
            gameSituation.GameStatus = CheckStatus();

            gameSituation.Enemies = new EnemySpaceships[enemiesCount];
            for (int i = 0; i < enemiesCount; i++)
            {
                gameSituation.Enemies[i] = _es[i];
            }

            gameSituation.Shoots = new Shoots[shootsCount];
            for (int i = 0; i < shootsCount; i++)
            {
                gameSituation.Shoots[i] = _shoots[i];
            }

            return gameSituation;
        }

        private GameStatus CheckStatus()
        {
            //Check loos
            int i = 0;
            do
            {
                EnemySpaceships enemy = _es[i];
                if (IsTooClose(_myPlayer.X, enemy.X, difference) &&
                    IsTooClose(_myPlayer.Y, enemy.Y, difference))
                {
                    return GameStatus.Loss;
                }
                i++;
            } while (i < enemiesCount);

            //Check win                   
            if (enemiesCount <= 1)
            {
                return GameStatus.Win;
            }

            //Continue play...
            return GameStatus.Playing;
        }

        private bool IsTooClose(double value1, double value2, double delta)
        {
            return Math.Abs(value1 - value2) <= delta;
        }

        private double CalculateLocation(double enemyValue)
        {
            return enemyValue + StepSize;           
        }

        public void MoveEnemies()
        {
            for (int i = 0; i < enemiesCount; i++)
            {
                EnemySpaceships enemy = _es[i];               
                enemy.Y = CalculateLocation(enemy.Y);
            }           
        }
      
        public void MovePlayer(Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    if (_myPlayer.X + StepSize < _width)
                    {
                        _myPlayer.X += StepSize;
                    }
                    break;
                case Direction.Left:
                    if (_myPlayer.X - StepSize >= 0)
                    {
                        _myPlayer.X -= StepSize;
                    }

                    break;
                case Direction.Up:
                    if (_myPlayer.Y - StepSize >= 0)
                    {
                        _myPlayer.Y -= StepSize;
                    }

                    break;
                case Direction.Down:
                    if (_myPlayer.Y + StepSize < _width)
                    {
                        _myPlayer.Y += StepSize;
                    }

                    break;
            }

           
        }

        public void CheckIfAShotHasHitTheEnemy()
        {
            for (int i = 0; i < _shoots.Length; i++)
            {
                Shoots shoots = _shoots[i];
                for (int j = 0; j < _es.Length; j++)
                {
                    
                    if (IsTooClose(shoots.X, _es[j].X, difference)&&
                        IsTooClose(shoots.Y, _es[j].Y, difference))
                    {
                        _es[j] = null;
                    }
                }
            }
           
        }

        public bool CheckIfUsed()
        {
            foreach (Shoots shoot in _shoots)
            {
                if (!shoot._active)
                {
                    shoot._active = true;
                    return shoot._active;
                }
               
            }
            return false;
        }
    }
}
