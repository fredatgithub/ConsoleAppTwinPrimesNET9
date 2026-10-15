using PrimeLibrary;

Action<string> print = Console.WriteLine;
print("Search for greatest gap between consecutive primes");
int maxNumber = 100_000_000;
// results:
// prime number - prime number + 1; max gap
// 4 652 353 - 4 652 507 = gap of 154
var allPrimes = new List<int>();
allPrimes.AddRange(new[] { 2, 3, 5, 7 });
var dicoPrimeGap = new Dictionary<int, int>();

for (int i = 3; i < maxNumber; i += 2)
{
	if (Helper.IsPrime(i))
	{
    allPrimes.Add(i);
    print($"Found prime: {i}");
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
