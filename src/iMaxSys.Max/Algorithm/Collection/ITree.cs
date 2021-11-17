using System;

namespace iMaxSys.Max.Algorithm.Collection;

public interface ITree
{
    public void Inert(int index, ITreeNode node);

    public void Remove(int index);
}


