using System.Collections.Generic;
using System;

public class ProcessChainPlayer
{
    private Dictionary<List<Action<Action>>, short> _chinsIndexes = new Dictionary<List<Action<Action>>, short>();

    public void ExecuteChain(List<Action<Action>> chain)
    {
        if (_chinsIndexes.ContainsKey(chain))
            return;
        _chinsIndexes.Add(chain, -1);
        ExecuteNext(chain);
    }

    private void ExecuteNext(List<Action<Action>> chain)
    {
        _chinsIndexes[chain]++;
        if (_chinsIndexes[chain] == chain.Count - 1)
        {
            chain[_chinsIndexes[chain]](() => { });
            _chinsIndexes.Remove(chain);
            return;
        }
        chain[_chinsIndexes[chain]](() => ExecuteNext(chain));
    }
}