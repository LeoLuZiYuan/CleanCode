using System.CodeDom;

namespace BowlingScore
{
    public class Game
    {
        private int _score;
        private int[] _throws = new int[21];
        private int _currentTrow;
        private int _currentFrame = 1;
        private bool isFirstThrow = true;
        private int _ball;
        private int _firstThrow;
        private int _secondThrow;

        public int Score()
        {
            return ScoreForFrame(_currentFrame - 1);
        }

        public int CurrentFrame
        {
            get { return _currentFrame; }
        }

        public void Add(int pins)
        {
            _throws[_currentTrow++] = pins;
            _score += pins;

            AdjustCurrentFrame(pins);
        }

        private void AdjustCurrentFrame(int pins)
        {
            if (isFirstThrow)
            {
                if (pins == 10) //Strike
                    _currentFrame++;
                else
                    isFirstThrow = false;
            }
            else
            {
                //第2球投完局數+1
                isFirstThrow = true;
                _currentFrame++;
            }

            if (_currentFrame > 11)
                _currentFrame = 11;
        }


        public int ScoreForFrame(int theFrame)
        {
            _ball = 0;
            var score = 0;
            for (var currentFrame = 0;
                currentFrame < theFrame;
                currentFrame++)
            {
                _firstThrow = _throws[_ball];
                if (Strike())
                {
                    _ball++;
                    score += 10 + NextThrowBalls;
                }
                else
                {
                    score += HandleSecondThrow();
                }
            }
            return score;
        }

        private int HandleSecondThrow()
        {
            var score = 0;
            _secondThrow = _throws[_ball + 1];

            var frameScore = _firstThrow + _secondThrow;

            //spare時需要知道下一輪的第一投
            if (Spare())
            {
                _ball += 2;
                score += frameScore + NextBall;
            }
            else
            {
                score += TwoBallsInFrame;
                _ball += 2;
            }
            return score;
        }

        private bool Strike()
        {
            return _throws[_ball] == 10;
        }

        private int NextThrowBalls
        {
            get
            {
                return (_throws[_ball] + _throws[_ball + 1]);
            }
        }

        private bool Spare()
        {
            return _throws[_ball] + _throws[_ball + 1] == 10;
        }

        private int NextBall
        {
            get { return _throws[_ball]; }
        }

        private int TwoBallsInFrame
        {
            get { return _throws[_ball] + _throws[_ball + 1]; }
        }
    }
}