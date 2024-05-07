using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NameSorter.Tests
{
    // Test fixture for NameSorter class
    [TestFixture]
    public class NameSorterTests
    {
        // Test method to verify that SortNames sorts names correctly
        [Test]
        public void SortNames_SortsNamesCorrectly()
        {
            // Creating a list of names to be sorted
            List<Name> names = new List<Name>
            {
                new Name("Parsons", new List<string>{"Janet"}),
                new Name("Lewis", new List<string>{"Vaughn"}),
                new Name("Archer", new List<string>{"Adonis", "Julius"})
            };

            // Creating an instance of NameSorter class
            NameSorter sorter = new NameSorter();

            // Sorting the list of names
            List<Name> sortedNames = sorter.SortNames(names);

            // Verifying that the names are sorted correctly
            Assert.That(("Adonis Julius Archer").Equals(sortedNames[0].ToString()));
            Assert.That(("Vaughn Lewis").Equals(sortedNames[1].ToString()));
            Assert.That(("Janet Parsons").Equals(sortedNames[2].ToString()));
        }
    }

    // Test fixture for FileReader class
    [TestFixture]
    public class FileReaderTests
    {
        // Test method to verify that ReadNamesFromFile reads names from a file correctly
        [Test]
        public void ReadNamesFromTextFile_ReturnsNamesFromTextFile()
        {
            // Creating a test file path
            string filePath = "testingNames.txt";
            // Creating test data: lines to be written to the test file
            string[] lines = new string[]
            {
                "Janet Parsons",
                "Vaughn Lewis",
                "Adonis Julius Archer"
            };
            // Writing test data to the test file
            System.IO.File.WriteAllLines(filePath, lines);

            // Reading names from the test file
            List<Name> names = FileReader.ReadNamesFromTextFile(filePath);

            // Verifying that the names are read correctly from the file
            Assert.That((3).Equals(names.Count));
            Assert.That(("Parsons").Equals(names[0].LastName));
            Assert.That(("Lewis").Equals(names[1].LastName));
            Assert.That(("Archer").Equals(names[2].LastName));

            // Clean up
            // Deleting the test file after the test
            System.IO.File.Delete(filePath);
        }
    }

    // Test fixture for FileWriter class
    [TestFixture]
    public class FileWriterTests
    {
        // Test method to verify that WriteNamesToFile writes names to a file correctly
        [Test]
        public void WriteNamesToTextFile_WritesNamesToTextFile()
        {
            // Creating a list of names to be written to a file
            List<Name> names = new List<Name>
            {
                new Name("Parsons", new List<string>{"Janet"}),
                new Name("Lewis", new List<string>{"Vaughn"}),
                new Name("Archer", new List<string>{"Adonis", "Julius"})
            };
            // Creating a test output file path
            string filePath = "testingTextFile.txt";

            // Writing names to the test output file
            FileWriter.WriteNamesToTextFile(names, filePath);

            // Verifying that the names are written correctly to the file
            string[] fileContent = System.IO.File.ReadAllLines(filePath);
            Assert.That((3).Equals(fileContent.Length));
            Assert.That(("Janet Parsons").Equals(fileContent[0]));
            Assert.That(("Vaughn Lewis").Equals(fileContent[1]));
            Assert.That(("Adonis Julius Archer").Equals(fileContent[2]));

            // Clean up
            // Deleting the test output file after the test
            System.IO.File.Delete(filePath);
        }
    }
}
