using System.Collections.Generic;

public class CircularBuffer<T>
{
    private T[] buffer;
    private int head;
    private int tail;
    private int size;
    private bool isFull;

    public CircularBuffer(int capacity)
    {
        buffer = new T[capacity];
        head = 0;
        tail = 0;
        size = capacity;
        isFull = false;
    }

    public void Add(T item)
    {
        buffer[tail] = item;
        if (isFull)
        {
            head = (head + 1) % size;
        }
        tail = (tail + 1) % size;
        isFull = tail == head;
    }

    public List<T> GetAll()
    {
        List<T> items = new List<T>();
        int index = head;
        int count = isFull ? size : tail;

        for (int i = 0; i < count; i++)
        {
            items.Add(buffer[index]);
            index = (index + 1) % size;
        }

        return items;
    }

    public void Clear()
    {
        head = 0;
        tail = 0;
        isFull = false;
    }
}
