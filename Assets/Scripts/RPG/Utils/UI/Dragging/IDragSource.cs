﻿namespace RPG.Utils.UI.Dragging
{

    public interface IDragSource<T> where T : class
    {
        T GetItem();
        int GetNumber();
        void RemoveItems(int number);
    }
}