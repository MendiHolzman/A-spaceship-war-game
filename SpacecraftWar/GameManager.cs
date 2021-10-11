using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpacecraftWar
{
    class GameManager
    {
        private Timer _timer;
        private BoardManager _board;

        public const int INTERVAL = 1300;

        public GameManager(int width, int height)
        {
            _board = new BoardManager(width, height);
        }

        public void StartGame()
        {
            _timer = new Timer(TimerCallback, null, 0, INTERVAL);
        }

        public void MovePlayer(Direction direction)
        {
            _board.MovePlayer(direction);
        }

        public GameSituation GetGameSituation()
        {
            return _board.GetGameSituation();
        }

        private void TimerCallback(object state)
        {
            _board.MoveEnemies();          
        }   
    }
}
