
#nullable enable

using System;
using System.Collections.Generic;

public class DoublyLinkedList<T> where T : IComparable<T>
{
    private Node<T>? head;
    private Node<T>? tail;
    private bool isDescending;

    public int Count { get; private set; }


    public void AddSorted(T data)
    {
        if (isDescending)
        {
            SortAscending();
        }

        Node<T> newNode = new Node<T>(data);

        if (head == null)
        {
            head = newNode;
            tail = newNode;
            Count++;
            return;
        }

        if (data.CompareTo(head.Data) < 0)
        {
            newNode.Next = head;
            head.Previous = newNode;
            head = newNode;
            Count++;
            return;
        }

        Node<T> current = head;

        while (current.Next != null &&
               current.Next.Data.CompareTo(data) <= 0)
        {
            current = current.Next;
        }

        if (current.Next == null)
        {
            current.Next = newNode;
            newNode.Previous = current;
            tail = newNode;
        }
        else
        {
            newNode.Next = current.Next;
            newNode.Previous = current;

            current.Next.Previous = newNode;
            current.Next = newNode;
        }

        Count++;
    }


    public void DisplayForward()
    {
        if (head == null)
        {
            Console.WriteLine("The list is empty.");
            return;
        }

        Node<T>? current = head;

        Console.Write("Forward: ");

        while (current != null)
        {
            Console.Write(current.Data);

            if (current.Next != null)
            {
                Console.Write(" <-> ");
            }

            current = current.Next;
        }

        Console.WriteLine();
    }


    public void DisplayBackward()
    {
        if (tail == null)
        {
            Console.WriteLine("The list is empty.");
            return;
        }

        Node<T>? current = tail;

        Console.Write("Backward: ");

        while (current != null)
        {
            Console.Write(current.Data);

            if (current.Previous != null)
            {
                Console.Write(" <-> ");
            }

            current = current.Previous;
        }

        Console.WriteLine();
    }


    private void ReverseLinks()
    {
        Node<T>? current = head;

        while (current != null)
        {
            Node<T>? nextNode = current.Next;

            current.Next = current.Previous;
            current.Previous = nextNode;

            current = nextNode;
        }

        Node<T>? oldHead = head;
        head = tail;
        tail = oldHead;
    }


    public void SortDescending()
    {
        if (head == null || isDescending)
        {
            return;
        }

        ReverseLinks();
        isDescending = true;
    }


    private void SortAscending()
    {
        if (head == null || !isDescending)
        {
            return;
        }

        ReverseLinks();
        isDescending = false;
    }


    private List<(T Value, int Count)> GetFrequencies()
    {
        List<(T Value, int Count)> frequencies =
            new List<(T Value, int Count)>();

        Node<T>? current = head;

        while (current != null)
        {
            int index = -1;

            for (int i = 0; i < frequencies.Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(
                    frequencies[i].Value, current.Data))
                {
                    index = i;
                    break;
                }
            }

            if (index >= 0)
            {
                var item = frequencies[index];

                frequencies[index] =
                    (item.Value, item.Count + 1);
            }
            else
            {
                frequencies.Add((current.Data, 1));
            }

            current = current.Next;
        }

        return frequencies;
    }


    public void DisplayModes()
    {
        List<(T Value, int Count)> frequencies = GetFrequencies();

        if (frequencies.Count == 0)
        {
            Console.WriteLine("The list is empty.");
            return;
        }

        int maxCount = 0;

        foreach (var item in frequencies)
        {
            if (item.Count > maxCount)
            {
                maxCount = item.Count;
            }
        }

        Console.Write("Mode(s): ");

        bool firstMode = true;

        foreach (var item in frequencies)
        {
            if (item.Count == maxCount)
            {
                if (!firstMode)
                {
                    Console.Write(", ");
                }

                Console.Write(item.Value);
                firstMode = false;
            }
        }

        Console.WriteLine();
    }


    public void DisplayFrequencyChart()
    {
        List<(T Value, int Count)> frequencies = GetFrequencies();

        if (frequencies.Count == 0)
        {
            Console.WriteLine("The list is empty.");
            return;
        }

        frequencies.Sort((first, second) =>
            first.Value.CompareTo(second.Value));

        Console.WriteLine("Frequency chart:");

        foreach (var item in frequencies)
        {
            Console.WriteLine(
                $"{item.Value} {new string('*', item.Count)}"
            );
        }
    }


    public bool Exists(T data)
    {
        Node<T>? current = head;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(
                current.Data, data))
            {
                return true;
            }

            current = current.Next;
        }

        return false;
    }


    public bool RemoveOne(T data)
    {
        Node<T>? current = head;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(
                current.Data, data))
            {
                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    head = current.Next;
                }

                if (current.Next != null)
                {
                    current.Next.Previous = current.Previous;
                }
                else
                {
                    tail = current.Previous;
                }

                Count--;

                if (Count == 0)
                {
                    head = null;
                    tail = null;
                }

                return true;
            }

            current = current.Next;
        }

        return false;
    }


    public int RemoveAll(T data)
    {
        int removedCount = 0;
        Node<T>? current = head;

        while (current != null)
        {
            Node<T>? nextNode = current.Next;

            if (EqualityComparer<T>.Default.Equals(
                current.Data, data))
            {
                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    head = current.Next;
                }

                if (current.Next != null)
                {
                    current.Next.Previous = current.Previous;
                }
                else
                {
                    tail = current.Previous;
                }

                Count--;
                removedCount++;
            }

            current = nextNode;
        }

        if (Count == 0)
        {
            head = null;
            tail = null;
        }

        return removedCount;
    }
}



