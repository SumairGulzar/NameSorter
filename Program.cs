using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NameSorter
{
    // Single Responsibility Principle
    // Class representing a person's name
    public class Name
    {
        public string LastName { get; }
        public List<string> GivenNames { get; }

        public Name(string lastName, List<string> givenNames)
        {
            LastName = lastName;
            GivenNames = givenNames;
        }

        // Override ToString() to provide a string representation of the name
        public override string ToString()
        {
            return string.Join(" ", GivenNames) + " " + LastName;
        }
    }

    // Class responsible for sorting names
    // Method to sort a list of names
    public class NameSorter
    {
        // Open/Closed Principle, The SortNames method is open for extension
        // like changing sorting criteria but closed for modification.
        
        public List<Name> SortNames(List<Name> names)
        {
            // Sorting names by last name, then by given names
            return names.OrderBy(n => n.LastName)
                        .ThenBy(n => string.Join(" ", n.GivenNames))
                        .ToList();
        }

        //...we can enhance this class by adding more sorting methods like SortNamesByGivenThenLast and more...
    }




    // Single Responsibility Principle
    // FileWriter class to handle file writing responsibilities
    public class FileWriter
    {
        // Method to write sorted names to a text file
        public static void WriteNamesToTextFile(List<Name> names, string filePath)
        {
            try
            {
                // Using StreamWriter to write names to the specified file path
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var name in names)
                    {
                        // Writing each name to a new line in the file
                        writer.WriteLine(name);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handling any errors that may occur during file writing
                Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
            }
        }

        //this class can be enhanced and multiple file writing methods can be added that can write to different file types...
    }

    // Single Responsibility Principle
    // FileReader class to handle file reading responsibilities
    public class FileReader
    {
        // ReadNamesFromFile method reads names from a file and returns a list of Name objects.
        public static List<Name> ReadNamesFromTextFile(string filePath)
        {
            List<Name> names = new List<Name>();

            try
            {
                // Using StreamReader to read names from the specified file path
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    // Reading each line until the end of the file
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Splitting each line into parts to extract last name and given names
                        string[] nameParts = line.Split(' ');
                        string lastName = nameParts.Last();
                        List<string> givenNames = nameParts.Take(nameParts.Length - 1).ToList();
                        // Creating a new Name object and adding it to the list
                        names.Add(new Name(lastName, givenNames));
                    }
                }
            }
            catch (Exception ex)
            {
                // Handling any errors that may occur during file reading
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
            }

            return names;
        }

        //this class can be enhanced and multiple file reading methods can be added that reads from different file types...
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                // Displaying usage instructions if incorrect number of arguments provided
                Console.WriteLine("Usage: name-sorter <file-path>");
                return;
            }

            string filePath = args[0];

            // Dependency Inversion Principle, Program depends on abstractions (List<Name>)
            // not depending on concrete implementations (specific file handling logic).

            // Reading unsorted names from the specified file
            List<Name> names = FileReader.ReadNamesFromTextFile(filePath);
            if (names.Count == 0)
            {
                // Displaying message if no names found in the file
                Console.WriteLine("No names found in the file.");
                return;
            }

            // Sorting the list of names
            NameSorter nameSorter = new NameSorter();
            List<Name> sortedNames = nameSorter.SortNames(names);

            // Displaying sorted names to the console
            Console.WriteLine("Sorted Names:");
            foreach (var name in sortedNames)
            {
                Console.WriteLine(name);
            }

            // Writing sorted names to a file
            string sortedFilePath = "sorted-names-list.txt";
            FileWriter.WriteNamesToTextFile(sortedNames, sortedFilePath);
            Console.WriteLine($"Sorted names written to '{sortedFilePath}' file.");
        }

    }
}