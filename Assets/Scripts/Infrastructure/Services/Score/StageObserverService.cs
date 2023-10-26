using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Stage;

namespace Infrastructure.Services.Score
{
    public class StageObserverService : IStageObserverService
    {
        private List<IStage> _stages;

        private IStage _currentStage;

        public event Action OnAllowStageLoop;
        public event Action<IStage, IStage> OnStageChanged;

        public void AllowStageLoop()
        {
            _currentStage = _stages.First();
            OnAllowStageLoop?.Invoke();
        }

        public Queue<IStage> GetStages() => new(_stages);

        public IStage GetNextStage(IStage stage)
        {
            var nextStageIndex = _stages.IndexOf(stage) + 1;

            if (nextStageIndex >= _stages.Count)
                nextStageIndex = 0;

            return _stages[nextStageIndex];
        }

        public void RegisterStages(List<IStage> stages)
        {
            _stages = stages;

            for (int i = 0; i < stages.Count; i++)
            {
                _stages[i].SetIndex(i);
            }
        }

        public void StateChanged(IStage stage, IStage previousStage)
        {
            _currentStage = stage;
            OnStageChanged?.Invoke(stage, previousStage);
        }

        public IStage GetActualStage() => _currentStage;
    }
}
