namespace BowlingScore
{
    public class Frame
    {
        private int _score;
        public int Score
        {
            get { return _score; }
        }

        public void Add(int pins)
        {
            _score += pins;
        }
    }
}