using System;
using Logic.Stage;

namespace Infrastructure.Services.Score
{
    public class NextStageObserverService : INextStageObserverService
    {
        public event Action<int> OnNextStage;
        public event Action<IStage, IStage> OnNextStagePlayer;

        public void NotifyNextStage(int currStage) =>
            OnNextStage?.Invoke(currStage);

        public void NotifyNextStagePlayer(IStage currentStage, IStage previousStage)
        {
            OnNextStagePlayer?.Invoke(currentStage, previousStage);
        }
    }
}
