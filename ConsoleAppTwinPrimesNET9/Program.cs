using System.Diagnostics;

Action<string> print = Console.WriteLine;

int nombre_max = 1_000_000_000;
print($"Calculating twin primes up to {nombre_max:N0}");

Stopwatch chrono = Stopwatch.StartNew();
var twinPrimes = new List<int>();
twinPrimes = ListAllTwinPrimes(nombre_max);
chrono.Stop();
int nombrePaires = CalculerPremiersJumeaux(nombre_max);

SaveTwinPrimesToFile(twinPrimes, $"twin_primes_{nombre_max}.txt");
print($"Twin primes saved to file: twin_primes_{nombre_max}.txt");
print(string.Empty);
print($"Nombre de paires trouvées : {nombrePaires:N0}");
print($"Temps de calcul : {chrono.Elapsed:hh\\:mm\\:ss\\.fff}");
print(string.Empty);
// saving result to a file


Console.WriteLine("Appuyez sur une touche pour terminer...");
Console.ReadKey();

static int CalculerPremiersJumeaux(int nombre_max)
{
  if (nombre_max < 5)
    return 0;

  // Crible d'Ératosthène
  bool[] estPremier = new bool[nombre_max + 1];

  for (int i = 2; i <= nombre_max; i++)
    estPremier[i] = true;

  for (int i = 2; i <= nombre_max / i; i++)
  {
    if (estPremier[i])
    {
      for (int j = i * i; j <= nombre_max; j += i)
      {
        estPremier[j] = false;
      }
    }
  }

  // Recherche des nombres premiers jumeaux
  int nombrePaires = 0;

  for (int i = 3; i <= nombre_max - 2; i++)
  {
    if (estPremier[i] && estPremier[i + 2])
    {
      Console.WriteLine($"({i}, {i + 2})");
      nombrePaires++;
    }
  }

  return nombrePaires;
}

static List<int> ListAllTwinPrimes(int nombre_max)
{
  if (nombre_max < 5)
    return new List<int>();
  // Crible d'Ératosthène
  bool[] estPremier = new bool[nombre_max + 1];
  for (int i = 2; i <= nombre_max; i++)
    estPremier[i] = true;
  for (int i = 2; i <= nombre_max / i; i++)
  {
    if (estPremier[i])
    {
      for (int j = i * i; j <= nombre_max; j += i)
      {
        estPremier[j] = false;
      }
    }
  }
  // Recherche des nombres premiers jumeaux
  List<int> twinPrimes = new List<int>();
  for (int i = 3; i <= nombre_max - 2; i++)
  {
    if (estPremier[i] && estPremier[i + 2])
    {
      twinPrimes.Add(i);
      twinPrimes.Add(i + 2);
    }
  }

  return twinPrimes;
}

static void SaveTwinPrimesToFile(List<int> twinPrimes, string filePath)
{
  using StreamWriter writer = new StreamWriter(filePath);
  for (int i = 0; i < twinPrimes.Count; i += 2)
  {
    writer.WriteLine($"({twinPrimes[i]}, {twinPrimes[i + 1]})");
  }
}