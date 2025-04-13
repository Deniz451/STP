public static class GameEvents
{
    public enum EventType
    {
        /*-----GAME EVENTS-----*/
        GameStart,
        GamePause,
        GameResume,
        GameReload,
        WaveStart,
        WaveEnd,
        AllEnemiesKilled,
        /*-----PLAYER EVENTS----*/
        PlayerEnabled,
        PlayerDisabled,
        PlayerDashStart,
        PlayerDashEnd,
        PlayerDeath,
        EnablePlayerControls,
        /*-----SHOP EVENTS------*/
        ShopOpen,
        ShopClose,
        ShopContinueBtnClick,
        ClickedChangeablePart,
        ShopRightClicked,
        SelectedGunL,
        SelectedGunR,
        /*---PAUSE MENU EVENTS---*/
        ResumeBtnClick,
        ExitBtnClick,
        /*CAMERA TRANSITIONS EVENTS*/
        CameraToGameEvent,
        CameraInGameEvent,
        CameraToShopEvent,
        CameraInShopEvent,
    }
}