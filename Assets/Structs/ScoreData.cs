namespace Structs
{
    public struct ScoreData
    {
        /// <summary>
        /// The amount of score obtained.
        /// </summary>
        public int Score
        {
            get;
            private set;
        }
        /// <summary>
        /// The name of the user who achieved the score.
        /// </summary>
        public string Username
        {
            get;
            private set;
        }

        public ScoreData(int _score, string _username)
        {
            Score = _score;
            Username = _username;
        }
    }
}
