
using System;

DoublyLinkedList<int> numbers = new DoublyLinkedList<int>();

bool isRunning = true;

while (isRunning)
{
    Console.WriteLine();
    Console.WriteLine("==================================");
    Console.WriteLine("     DOUBLY LINKED LIST MENU");
    Console.WriteLine("==================================");
    Console.WriteLine("1. Add element");
    Console.WriteLine("2. Display forward");
    Console.WriteLine("3. Display backward");
    Console.WriteLine("4. Sort descending");
    Console.WriteLine("5. Display mode(s)");
    Console.WriteLine("6. Display frequency chart");
    Console.WriteLine("7. Search element");
    Console.WriteLine("8. Remove one occurrence");
    Console.WriteLine("9. Remove all occurrences");
    Console.WriteLine("0. Exit");
    Console.WriteLine("==================================");

    Console.Write("Select an option: ");

    string? option = Console.ReadLine();

    switch (option)
    {
        case "1":
            int newValue = ReadInteger("Enter an integer: ");

            numbers.AddSorted(newValue);

            Console.WriteLine("Element added successfully.");
            numbers.DisplayForward();
            break;

        case "2":
            numbers.DisplayForward();
            break;

        case "3":
            numbers.DisplayBackward();
            break;

        case "4":
            numbers.SortDescending();

            Console.WriteLine("List sorted in descending order.");
            numbers.DisplayForward();
            break;

        case "5":
            numbers.DisplayModes();
            break;

        case "6":
            numbers.DisplayFrequencyChart();
            break;

        case "7":
            int searchValue = ReadInteger("Enter the element to search: ");

            if (numbers.Exists(searchValue))
            {
                Console.WriteLine($"{searchValue} exists in the list.");
            }
            else
            {
                Console.WriteLine($"{searchValue} does not exist in the list.");
            }

            break;

        case "8":
            int removeValue = ReadInteger("Enter the element to remove: ");

            if (numbers.RemoveOne(removeValue))
            {
                Console.WriteLine("One occurrence removed successfully.");
            }
            else
            {
                Console.WriteLine("Element not found.");
            }

            numbers.DisplayForward();
            break;

        case "9":
            int removeAllValue = ReadInteger(
                "Enter the element to remove completely: "
            );

            int removedCount = numbers.RemoveAll(removeAllValue);

            Console.WriteLine(
                $"{removedCount} occurrence(s) removed."
            );

            numbers.DisplayForward();
            break;

        case "0":
            isRunning = false;

            Console.WriteLine("Program finished. Goodbye!");
            break;

        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}


static int ReadInteger(string message)
{
    while (true)
    {
        Console.Write(message);

        string? input = Console.ReadLine();

        if (int.TryParse(input, out int value))
        {
            return value;
        }

        Console.WriteLine("Invalid input. Please enter an integer.");
    }
}

