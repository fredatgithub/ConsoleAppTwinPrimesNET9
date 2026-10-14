using PrimeLibrary;

Action<string> print = Console.WriteLine;
print("Search for prime triplet ending with 9-1-3");
int maxNumber = 1_000_000_000;
var allPrimes = new List<int>();
allPrimes.AddRange(new[] { 2, 3, 5, 7 });

for (int i = 3; i < maxNumber; i += 2)
{
	if (Helper.IsPrime(i))
	{
    allPrimes.Add(i);
	}
}

var triplets = new List<(int, int, int)>();
for (int i = 0; i < allPrimes.Count - 2; i++)
{
  int p1 = allPrimes[i];
  int p2 = allPrimes[i + 1];
  int p3 = allPrimes[i + 2];
  if (p2 == p1 + 2 && p3 == p2 + 2)
  {
    triplets.Add((p1, p2, p3));
  }
}

// display the triplets
foreach (var triplet in triplets)
{
  print($"({triplet.Item1}, {triplet.Item2}, {triplet.Item3})");
}

print($"Total triplets found: {triplets.Count}");
print($"Total primes found: {allPrimes.Count}");
print("Press any key to exit...");
Console.ReadKey();
