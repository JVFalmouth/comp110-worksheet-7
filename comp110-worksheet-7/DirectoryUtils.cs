using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp110_worksheet_7
{
	public static class DirectoryUtils
	{
		// Return the size, in bytes, of the given file
		public static long GetFileSize(string filePath)
		{
			return new FileInfo(filePath).Length;
		}

		// Return true if the given path points to a directory, false if it points to a file
		public static bool IsDirectory(string path)
		{
			return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
		}

        // Return the total size, in bytes, of all the files below the given directory
        public static long GetTotalSize(string directory)
        {
            long size = 0;
            Stack<string> searchStack = new Stack<string>();
            searchStack.Push(directory);
            while (searchStack.Count != 0)
            {
                var n = searchStack.Pop();
                Console.WriteLine(n);
                if (IsDirectory(n))
                {
                    var directories = Directory.GetDirectories(n);
                    var files = Directory.GetFiles(n);
                    foreach (string dir in directories)
                    {
                        searchStack.Push(dir);
                    }
                    foreach (string file in files)
                    {
                        size += GetFileSize(file);
                    }
                }
                else
                {
                    size += GetFileSize(n);
                }
            }
            return size;
        }

		// Return the number of files (not counting directories) below the given directory
		public static int CountFiles(string directory)
		{
            int count = 0;
            Stack<string> searchStack = new Stack<string>();
            searchStack.Push(directory);
            while (searchStack.Count != 0)
            {
                var n = searchStack.Pop();
                Console.WriteLine(n);
                if (IsDirectory(n))
                {
                    var directories = Directory.GetDirectories(n);
                    var files = Directory.GetFiles(n);
                    foreach (string dir in directories)
                    {
                        searchStack.Push(dir);
                    }
                    foreach (string file in files)
                    {
                        count ++;
                    }
                }
                else
                {
                    count++;
                }
            }
            return count;
        }

		// Return the nesting depth of the given directory. A directory containing only files (no subdirectories) has a depth of 0.
		public static int GetDepth(string directory)
		{
            int depth = 0;
            if (Directory.GetDirectories(directory).Length == 0)
            {
                return depth;
            }
            else
            {
                depth++;
                var subDirectories = Directory.GetDirectories(directory);
                foreach (string dir in subDirectories)
                {
                    depth += GetDepth(dir);
                }
                return depth;
            }     
		}

		// Get the path and size (in bytes) of the smallest file below the given directory
		public static Tuple<string, long> GetSmallestFile(string directory)
		{
            Tuple<string, long> smallestFile = new Tuple<string, long>("", long.MaxValue);
            Stack<string> searchStack = new Stack<string>();
            searchStack.Push(directory);
            while (searchStack.Count != 0)
            {
                var n = searchStack.Pop();
                Console.WriteLine(n);
                if (IsDirectory(n))
                {
                    var directories = Directory.GetDirectories(n);
                    var files = Directory.GetFiles(n);
                    foreach (string dir in directories)
                    {
                        searchStack.Push(dir);
                    }
                    foreach (string file in files)
                    {
                        searchStack.Push(file);
                    }
                }
                else
                {
                    if ((GetFileSize(n) < smallestFile.Item2))
                    {
                        smallestFile = new Tuple<string, long>(n, GetFileSize(n));
                    }
                }
            }                
            return smallestFile;
		}

		// Get the path and size (in bytes) of the largest file below the given directory
		public static Tuple<string, long> GetLargestFile(string directory)
		{
            Tuple<string, long> largestFile = new Tuple<string, long>("", 0);
            Stack<string> searchStack = new Stack<string>();
            searchStack.Push(directory);
            while (searchStack.Count != 0)
            {
                var n = searchStack.Pop();
                Console.WriteLine(n);
                if (IsDirectory(n))
                {
                    var directories = Directory.GetDirectories(n);
                    var files = Directory.GetFiles(n);
                    foreach (string dir in directories)
                    {
                        searchStack.Push(dir);
                    }
                    foreach (string file in files)
                    {
                        searchStack.Push(file);
                    }
                }
                else
                {
                    if ((GetFileSize(n) > largestFile.Item2))
                    {
                        largestFile = new Tuple<string, long>(n, GetFileSize(n));
                    }
                }
            }
            return largestFile;
        }

		// Get all files whose size is equal to the given value (in bytes) below the given directory
		public static IEnumerable<string> GetFilesOfSize(string directory, long size)
		{
            List<string> correctFiles = new List<string>();

            Stack<string> searchStack = new Stack<string>();
            searchStack.Push(directory);
            while (searchStack.Count != 0)
            {
                var n = searchStack.Pop();
                Console.WriteLine(n);
                if (IsDirectory(n))
                {
                    var directories = Directory.GetDirectories(n);
                    var files = Directory.GetFiles(n);
                    foreach (string dir in directories)
                    {
                        searchStack.Push(dir);
                    }
                    foreach (string file in files)
                    {
                        searchStack.Push(file);
                    }
                }
                else
                {
                    if ((GetFileSize(n) == size))
                    {
                        correctFiles.Add(n);
                    }
                }
            }
            return correctFiles;

        }
	}
}
