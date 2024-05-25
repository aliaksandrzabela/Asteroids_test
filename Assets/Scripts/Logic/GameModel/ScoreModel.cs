namespace Asteroids.Model
{
    public class ScoreModel : IScoreModel
    {
        public int Score => score;

        private int score = 0;

        public void AddScore(object model)
        {
            if (model is ShipUFOModel)
            {
                score += Config.SCORE_UFO;
            }
            if (model is AsteroidModel)
            {
                score += Config.SCORE_ASTEROID;
            }
        }
    }
}
