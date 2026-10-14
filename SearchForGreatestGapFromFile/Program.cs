using PrimeLibrary;

Action<string> print = Console.WriteLine;
print("Search for the greatest prime gap");
// Create a dictionary to store the prime gaps the dictionary has the key of the primeand the value of number of gaps between the prime and the next prime
var dicoPrimeGap = new Dictionary<int, int>();
dicoPrimeGap.Add(2, 1); // 2 is the first prime and the gap to the next prime (3) is 1
dicoPrimeGap.Add(3, 1);
int maxNumber = 100_000_000;
// result
// prime number; max gap
// 1_000;20
// 10_000;36
// 100_000;72
// 1_000_000;114
// 10_000_000;154
// 100_000_000;220

for (int number = 3; number <= maxNumber; number += 2)
{
  if (Helper.IsPrime(number))
  {
    int nextPrime = number + 1;
    while (!Helper.IsPrime(nextPrime))
    {
      nextPrime++;
    }

    int gap = nextPrime - number;
    dicoPrimeGap[number] = gap;//number nextPrime
  }
}

// display the prime gaps
foreach (var number in dicoPrimeGap)
{
  print($"Prime: {number.Key}, Gap: {number.Value}");
}

// Find the greatest gap
var greatestGap = dicoPrimeGap.OrderByDescending(x => x.Value).First();
print($"The greatest prime gap up to {maxNumber} is {greatestGap.Value} between {greatestGap.Key} and {greatestGap.Key + greatestGap.Value}");

print("Press any key to exit...");
Console.ReadKey();
