public static class GameEvents
{
    public enum EventType
    {
        /*-----GAME EVENTS-----*/
        GameStart,
        GamePause,
        GameResume,
        WaveStart,
        WaveEnd,
        AllEnemiesKilled,
        /*-----PLAYER EVENTS----*/
        PlayerEnabled,
        PlayerDisabled,
        PlayerDashStart,
        PlayerDashEnd,
        PlayerDeath,
        /*-----SHOP EVENTS------*/
        ShopOpen,
        ShopClose,
        ShopContinueBtnClick,
    }
}