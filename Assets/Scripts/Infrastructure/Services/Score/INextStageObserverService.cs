using System;
using Logic.Stage;

namespace Infrastructure.Services.Score
{
    public interface INextStageObserverService
    {
        event Action<int> OnNextStage;
        event Action<IStage, IStage> OnNextStagePlayer;
        void NotifyNextStage(int currStage);
        void NotifyNextStagePlayer(IStage currentStage, IStage previousStage);
    }
}
