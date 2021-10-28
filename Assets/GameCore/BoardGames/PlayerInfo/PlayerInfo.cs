namespace GameCore.BoardGames
{
    public class PlayerContainer
    {
        public int CurrentNodeIndex;
        public Chip Chip;
        public PlayerInfo PlayerInfo;
    }

    public class PlayerInfo
    {
        public string Name;
        public int Index;
        public int Score;
        public int Skin;
        public bool IsBot;
    }
}