using System;
using System.Collections.Generic;

namespace GameCore.BoardGames
{
    public interface IDiceController
    {
        event Action<int, int> OnSingleDiceChange;
        event Action<IList<int>> OnEnd;
        event Action<int> OnSetDiceCount;
    }
}