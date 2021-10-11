using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacecraftWar
{
    public class GameSituation
    {
        private MyPlayer _player;
        private EnemySpaceships[] _enemies;
        private GameStatus _status;
        private Shoots[] _shoots;

        public MyPlayer MyPlayer
        {
            get { return _player; }
            set { _player = value; }
        }

        public EnemySpaceships[] Enemies
        {
            get { return _enemies; }
            set { _enemies = value; }
        }

        public GameStatus GameStatus
        {
            get { return _status; }
            set { _status = value; }
        }

        public Shoots[] Shoots
        {
            get { return _shoots; }
            set { _shoots = value; }
        }
    }
}
