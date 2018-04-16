namespace TodoList.Design
{
    using System;

    using TodoList.Model;

    public class DesignDataService : IDataService
    {
        public void GetData(Action<Item, Exception> callback)
        {
            var item = new Item("test string1");
            callback(item, null);
        }
    }
}