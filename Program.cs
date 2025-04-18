using MyLinkedList;
namespace LaboratoryWork7
{
    internal class Program
    {
        /// <summary>
        /// Creates an array with randomly generated short values.
        /// </summary>
        /// <param name="n">Number of elements.</param>
        /// <returns>Array containing randomly generated values of type short.</returns>
        static short[] GenerateRandomValues(int n)
        {
            short[] values = new short[n];
            Random rand = new Random();
            for (int i = 0; i < n; i++)
                values[i] = (short)rand.Next(-300, 300);
            return values;
        }
        /// <summary>
        /// Shows all elements in list.
        /// </summary>
        /// <param name="list">The list whose elements should be shown.</param>
        static void ShowList(TheLinkedList list)
        {
            int counter = 0;            
            foreach (var item in list)
            {
                if (counter % 15 == 0 && counter > 0)
                    Console.WriteLine();
                Console.Write("{0, -6}", item);
                counter++;
            }            
            Console.WriteLine();
        }
        /// <summary>
        /// Used for assigning correct value to a variable of type short.
        /// </summary>
        /// <returns>Value of type short.</returns>
        static short ValidateInput(string message = "", short min = short.MinValue, short max = short.MaxValue)
        {
            bool isCorrect = true;
            short value = 0;
            do
            {
                Console.Write(message);
                try
                {
                    isCorrect = true;
                    value = short.Parse(Console.ReadLine());
                }
                catch (FormatException ex)
                {
                    Console.Write("Enter ONLY integer, not symbols.\nRepeat the input: ");
                    isCorrect = false;
                    continue;
                }
                catch (OverflowException ex)
                {
                    Console.Write($"You number should be from {short.MinValue} to {short.MaxValue}.\nRepeat the input: ");
                    isCorrect = false;
                    continue;
                }
                if (value < min || value > max)
                {
                    Console.Write("Your input value is not allowed.\nRepeat the input: ");
                    isCorrect = false;
                }
            } while (!isCorrect);
            return value;
        }
        static void Main(string[] args)
        {
            TheLinkedList myList = new TheLinkedList();
            Console.Write($"Enter the number which you want to add (from {short.MinValue} to {short.MaxValue}): ");
            short number = ValidateInput();
            myList.Add(number);
            int size1 = myList.Size;
            Console.WriteLine("The size of list: {0}", size1);
            Console.Write("The element" + (size1 > 1 ? "s" : "") + " in list: ");
            ShowList(myList);
            Console.ReadKey();
            Console.Write("Enter the number of elements you want to add to the list [1-100]: ");
            number = ValidateInput("", 1, 100);
            short[] numbers = new short[number];
            Console.WriteLine("Do you want to input values or use randomly generated ones?");
            Console.WriteLine("1. Input.");
            Console.WriteLine("2. Fill randomly.");
            short choice = ValidateInput("", 1, 2);
            switch (choice)
            {
                case 1:
                    Console.WriteLine($"Now enter your values (from {short.MinValue} to {short.MaxValue}).");
                    for (int i = 0; i < number; i++)
                        numbers[i] = ValidateInput($"{i + 1}-> ");
                    break;
                case 2:
                    numbers = GenerateRandomValues(number);
                    break;
                default:
                    Console.WriteLine("Something strange happened...");
                    break;
            }
            myList.AddSomeElements(numbers);
            size1 = myList.Size;
            Console.WriteLine($"List after adding {number} elements:");
            ShowList(myList);
            Console.WriteLine("The size of list: {0}", size1);
            Console.WriteLine("Now list is " + (myList.IsEmpty() ? "" : "not ") + "empty.");
            Console.Write($"Enter the number, which will be less than elements of new list (from {short.MinValue} to {short.MaxValue}): ");
            short compNumber = ValidateInput();
            TheLinkedList newList = myList.CreateListWithGreaterNumbers(compNumber);
            int size2 = newList.Size;
            if (newList.IsEmpty())
                Console.WriteLine("New list is empty.");
            else
            {                
                Console.WriteLine($"New list contains {size2} element" + (size2 > 1 ? "s." : "."));
                Console.WriteLine("Elements of new list:");
                ShowList(newList);
                Console.ReadKey();
                Console.WriteLine("Now we will find the multilpe to specific number in new list.");
                Console.Write("Enter the divider: ");
                short divider = ValidateInput();
                short multiple = 0;
                bool isMultiple = newList.FindTheMultipleNumber(divider, ref multiple);
                if (!isMultiple)
                    Console.WriteLine("There is no number which is multiple to {0}", divider);
                else
                    Console.WriteLine($"{multiple} is multiple to {divider}");
                Console.ReadKey();
                Console.WriteLine("We will change last element in new list to our divider.");
                Console.WriteLine($"Element at index {size2 - 1} before changing: {newList[size2 - 1]}");
                newList[size2 - 1] = divider;
                Console.ReadKey();
                Console.WriteLine($"Element at index {size2 - 1} after changing: {newList[size2 - 1]}");
                Console.Write("Now input the index at which element should be removed: ");
                int index = ValidateInput("", 0, (short)(size2 - 1));
                try
                {
                    newList.DeleteTheElementAtIndex(index);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                if (newList.IsEmpty())
                    Console.WriteLine("List is empty now.");
                else
                {
                    size2 = newList.Size;
                    Console.WriteLine($"Now new list contains {size2} element" + (size2 > 1 ? "s." : "."));
                    Console.WriteLine("All elements in new list:");
                    ShowList(newList);
                }
            }
            Console.WriteLine("Now first list will be changed");
            Console.ReadKey();
            Console.WriteLine("First list before changing:");
            ShowList(myList);
            Console.ReadKey();
            myList.ChangeElementsWithEvenIndex();
            Console.WriteLine("First list after changing:");
            ShowList(myList);
            Console.ReadKey();
            Console.WriteLine("We will also delete all elements at odd indexes from this list.");
            Console.WriteLine("The size before deleting: {0}", size1);
            Console.ReadKey();
            myList.DeleteElementsWithOddIndex();
            size1 = myList.Size;
            Console.WriteLine("The size after deleting: {0}", size1);
            Console.ReadKey();
            Console.WriteLine("The list looks like:");
            ShowList(myList);
            Console.WriteLine("And finally we will clear our lists.");
            Console.WriteLine("Size of first list before clearing: {0}", size1);
            myList.Clear();
            size1 = myList.Size;
            Console.WriteLine("Size of first list after clearing: {0}", size1);
            Console.ReadKey();
            Console.WriteLine("Size of second list before clearing: {0}", size2);
            newList.Clear();
            size2 = myList.Size;
            Console.WriteLine("Size of second list after clearing: {0}", size2);
            Console.ReadKey();
        }
    }
}

