using UnityEngine;

public static class PauseController
{
    private static bool _isGamePaused = false;
    
    public static bool IsGamePaused 
    {
        get { return _isGamePaused; }
        private set { _isGamePaused = value; }
    }

    public static void SetPause(bool pause)
    {
        IsGamePaused = pause;
        
        // Se você quiser que o tempo do jogo PARE, adicione esta linha:
        Time.timeScale = IsGamePaused ? 0f : 1f;
    }
}