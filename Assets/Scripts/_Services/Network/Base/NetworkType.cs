namespace Services.Network
{
    public enum NetworkType 
    {
       NoneNetwork, // Одиночная игра.
       
       DynamicSinglePlayer, // Одиночная игра с простыми сетевыми функциями, вроде всяких
       // социальных досок рекордов, кнопок "поделится" и регулярными обновлениями контента.
       TurnedBasedMultiplayer, // Пошаговый геймплей, с участием более одоного игрока.
       RealTimeMultiplayer, // Геймплей в реальном времени, с участием не более 10/16/25/...игроков. Сессионные игры.
       PersistentGameSpaces // Геймплей в реальном времени, с общим для все игроков состоянием. Mmo rpg example.
    }
}
