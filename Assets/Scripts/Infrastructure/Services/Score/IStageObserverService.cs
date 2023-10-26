using System;
using System.Collections.Generic;
using Logic.Stage;

namespace Infrastructure.Services.Score
{
    public interface IStageObserverService
    {
        Queue<IStage> GetStages();
        void RegisterStages(List<IStage> stages);
        event Action OnAllowStageLoop;
        void AllowStageLoop();
        void StateChanged(IStage stage, IStage previousStage);
        event Action<IStage, IStage> OnStageChanged;
        IStage GetActualStage();
        IStage GetNextStage(IStage stage);
    }
}
