Action<string> print = Console.WriteLine;
print("Search for greatest gap between consecutive primes");
int maxNumber = 10_000_000;

var allPrimes = new List<int>();
allPrimes.AddRange(new[] { 2, 3, 5, 7 });
var dicoPrimeGap = new Dictionary<int, int>();

for (int i = 3; i < maxNumber; i += 2)
{
	if (IsPrime(i))
	{
    allPrimes.Add(i);
	}
}

int greatestGap = 0;
(int, int) greatestGapPair = (0, 0);
for (int i = 0; i < allPrimes.Count - 1; i++)
{
  int gap = allPrimes[i + 1] - allPrimes[i];
  if (gap > greatestGap)
  {
    greatestGap = gap;
    greatestGapPair = (allPrimes[i], allPrimes[i + 1]);
  }
}

print($"Greatest gap: {greatestGap} between primes {greatestGapPair.Item1} and {greatestGapPair.Item2}");
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
