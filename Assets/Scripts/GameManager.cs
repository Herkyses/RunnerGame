using System;

namespace DefaultNamespace
{
    public static class GameManager 
    {
        public static Action<float> OnAnswerControl;
        public static Action Victory;
        public static Action Failed;
    }
}