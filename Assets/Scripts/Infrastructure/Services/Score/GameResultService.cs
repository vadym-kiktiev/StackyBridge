using System;

namespace Infrastructure.Services.Score
{
    public class GameResultService : IGameResultService
    {
        public event Action OnWin;
        public event Action OnLose;

        public void Win() => OnWin?.Invoke();

        public void Lose() => OnLose?.Invoke();
    }
}
