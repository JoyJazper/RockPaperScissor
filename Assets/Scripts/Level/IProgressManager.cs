namespace RPS
{
    internal interface IProgressManager
    {
        int CurrentLevel { get; }
        float LevelProgress { get; }

        void SaveProgress(float progress);

        void SaveLevel(int level);

        void ResetData();
    }
}