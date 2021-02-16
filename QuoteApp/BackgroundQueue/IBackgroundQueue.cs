using System.Collections.Generic;

namespace QuoteServer.BackgroundQueue
{
    public interface IBackgroundQueue<T>
    {
        void Enqueue(T item);

        T Dequeue();
       
        IEnumerable<T> GetAll();
    }
}
