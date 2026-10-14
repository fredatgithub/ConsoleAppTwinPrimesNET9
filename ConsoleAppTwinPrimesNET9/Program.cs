using System.Diagnostics;
using PrimeLibrary;

Action<string> print = Console.WriteLine;

int nombre_max = 1_000_000_000;
print($"Calculating twin primes up to {nombre_max:N0}");

Stopwatch chrono = Stopwatch.StartNew();
var twinPrimes = new List<int>();
twinPrimes = Helper.ListAllTwinPrimes(nombre_max);
chrono.Stop();
int nombrePaires = CalculerPremiersJumeaux(nombre_max);

SaveTwinPrimesToFile(twinPrimes, $"twin_primes_{nombre_max}.txt");
print($"Twin primes saved to file: twin_primes_{nombre_max}.txt");
print(string.Empty);
print($"Nombre de paires trouvées : {nombrePaires:N0}");
print($"Temps de calcul : {chrono.Elapsed:hh\\:mm\\:ss\\.fff}");
print(string.Empty);

// calculer les triplets de nombres premiers
Console.WriteLine($"Recherche des triplets jusqu'à {nombre_max:N0}");
Console.WriteLine();
chrono = Stopwatch.StartNew();
int nombreTriplets = CalculerTriplets(nombre_max);
chrono.Stop();
// saver les triplets dans un fichier
SaveTripletPrimesToFile(ListAllTriplets(nombre_max), $"triplet_primes_{nombre_max}.txt");
print($"Triplets saved to file: triplet_primes_{nombre_max}.txt");

Console.WriteLine();
Console.WriteLine($"Nombre de triplets trouvés : {nombreTriplets:N0}");
Console.WriteLine($"Temps de calcul : {chrono.Elapsed:hh\\:mm\\:ss\\.fff}");

Console.WriteLine();
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

static void SaveTwinPrimesToFile(List<int> twinPrimes, string filePath)
{
  using StreamWriter writer = new StreamWriter(filePath);
  for (int i = 0; i < twinPrimes.Count; i += 2)
  {
    writer.WriteLine($"({twinPrimes[i]}, {twinPrimes[i + 1]})");
  }
}

static void SaveTripletPrimesToFile(List<int> triplets, string filePath)
{
  using StreamWriter writer = new StreamWriter(filePath);
  for (int i = 0; i < triplets.Count; i += 3)
  {
    writer.WriteLine($"({triplets[i]}, {triplets[i + 1]}, {triplets[i + 2]})");
  }
}

static int CalculerTriplets(int nombre_max)
{
  if (nombre_max < 11)
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
        estPremier[j] = false;
    }
  }

  // Recherche des deux formes de triplets
  int nombreTriplets = 0;

  for (int p = 2; p <= nombre_max - 6; p++)
  {
    if (!estPremier[p])
      continue;

    // Forme (p, p + 2, p + 6)
    if (estPremier[p + 2] && estPremier[p + 6])
    {
      Console.WriteLine($"({p}, {p + 2}, {p + 6})");
      nombreTriplets++;
    }

    // Forme (p, p + 4, p + 6)
    if (estPremier[p + 4] && estPremier[p + 6])
    {
      Console.WriteLine($"({p}, {p + 4}, {p + 6})");
      nombreTriplets++;
    }
  }

  return nombreTriplets;
}

static List<int> ListAllTriplets(int nombre_max)
{
  if (nombre_max < 11)
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
        estPremier[j] = false;
    }
  }
  // Recherche des deux formes de triplets
  List<int> triplets = new List<int>();
  for (int p = 2; p <= nombre_max - 6; p++)
  {
    if (!estPremier[p])
      continue;
    // Forme (p, p + 2, p + 6)
    if (estPremier[p + 2] && estPremier[p + 6])
    {
      triplets.Add(p);
      triplets.Add(p + 2);
      triplets.Add(p + 6);
    }
    // Forme (p, p + 4, p + 6)
    if (estPremier[p + 4] && estPremier[p + 6])
    {
      triplets.Add(p);
      triplets.Add(p + 4);
      triplets.Add(p + 6);
    }
  }

  return triplets;
}

static int CalculerQuadruplets(int nombre_max)
{
  if (nombre_max < 13)
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
        estPremier[j] = false;
    }
  }

  // Recherche des quadruplets :
  // (p, p + 2, p + 6, p + 8)
  int nombreQuadruplets = 0;

  for (int p = 2; p <= nombre_max - 8; p++)
  {
    if (estPremier[p] &&
        estPremier[p + 2] &&
        estPremier[p + 6] &&
        estPremier[p + 8])
    {
      Console.WriteLine($"({p}, {p + 2}, {p + 6}, {p + 8})");
      nombreQuadruplets++;
    }
  }

  return nombreQuadruplets;
}

static List<int> ListAllQuadruplets(int nombre_max)
{
  if (nombre_max < 13)
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
        estPremier[j] = false;
    }
  }
  // Recherche des quadruplets :
  // (p, p + 2, p + 6, p + 8)
  List<int> quadruplets = new();
  for (int p = 2; p <= nombre_max - 8; p++)
  {
    if (estPremier[p] &&
        estPremier[p + 2] &&
        estPremier[p + 6] &&
        estPremier[p + 8])
    {
      quadruplets.Add(p);
      quadruplets.Add(p + 2);
      quadruplets.Add(p + 6);
      quadruplets.Add(p + 8);
    }
  }
 
  return quadruplets;
}
