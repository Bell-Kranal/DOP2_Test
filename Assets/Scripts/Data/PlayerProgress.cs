using System;

namespace Data
{
    [Serializable]
    public class PlayerProgress
    {
        public int Level;

        public PlayerProgress() =>
            Level = 0;
    }
}