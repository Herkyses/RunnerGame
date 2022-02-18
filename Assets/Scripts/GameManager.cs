using System;

namespace DefaultNamespace
{
    public static class GameManager 
    {
        public static Action<int> OnAnswerControl;
        public static Action Victory;
        public static Action Failed;
    }
}