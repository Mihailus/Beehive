namespace TodoList.Model
{
    using System;

    public interface IDataService
    {
        void GetData(Action<Item, Exception> callback);
    }
}
