/// ---------------------------------------------
/// Dyp Penguin Character | Dypsloom
/// Copyright (c) Dyplsoom. All Rights Reserved.
/// https://www.dypsloom.com
/// ---------------------------------------------

namespace Dypsloom.DypThePenguin.Scripts.Items
{
    using Character = Character;

    public interface IItemUser
    {
        void TickUse(IUsableItem usableItem);
        Character Character { get; }
    }
}