namespace Asteroids.Model
{
    public interface IScoreModel
    {
        int Score { get; }
        void AddScore(object model);
    }
}
