using System;

namespace Infrastructure.Services.Score
{
    public interface IGameResultService
    {
        void Win();
        void Lose();
        event Action OnWin;
        event Action OnLose;
    }
}
