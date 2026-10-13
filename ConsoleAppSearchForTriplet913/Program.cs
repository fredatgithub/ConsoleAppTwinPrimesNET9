Action<string> print = Console.WriteLine;
print("Search for prime triplet ending with 9-1-3");
int maxNumber = 1_000_000_000;
var allPrimes = new List<int>();
allPrimes.AddRange(new[] { 2, 3, 5, 7 });

for (int i = 3; i < maxNumber; i += 2)
{
	if (IsPrime(i))
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

static bool IsPrime(int number)
{
  if (number < 2)
    return false;

  if (number == 2 || number == 3 || number == 5 || number == 7)
    return true;

  if (number % 2 == 0 || number % 3 == 0 || number % 5 == 0 || number % 7 == 0)
    return false;

  for (int diviseur = 11; diviseur <= number / diviseur; diviseur += 2)
  {
    if (number % diviseur == 0)
      return false;
  }

  return true;
}