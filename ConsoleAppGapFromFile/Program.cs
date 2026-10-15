Action<string> print = Console.WriteLine;
print("Searching for the 913 pattern from a file");
string filename = "triplet_primes_1_000_000_000.txt";
var lines = File.ReadAllLines(filename);
var results = new List<(int, int, int)>();
foreach (var line in lines)
{
  //string oneLine = line.Trim().Replace('(', string.Empty.ToCharArray()[0]).Replace(')', string.Empty.ToCharArray()[0]);
  string oneLine = line.Trim().Replace("(", "").Replace(")", "");

  var array = oneLine.Split(',');
  if (array[0].EndsWith('9') && array[1].EndsWith('1') && array[2].EndsWith('3'))
  {
    print($"Found '913' in line: {line}");
    results.Add((int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2])));
  }
  else
  {
    print($"No '913' in line: {line}");
  }
}

if (results.Count == 0)
{
  print("No triplet found with the '913' pattern.");
}
else
{
  // display the results
  foreach (var result in results)
  {
    print($"Found triplet: ({result.Item1}, {result.Item2}, {result.Item3})");
  }

  print($"Total triplets found with the '913' pattern: {results.Count}");
}

print("Press any key to exit...");
Console.ReadKey();
