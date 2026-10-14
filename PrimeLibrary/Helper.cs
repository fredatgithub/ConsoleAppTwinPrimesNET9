namespace PrimeLibrary
{
  public static class Helper
  {
    public static bool IsPrime(int number)
    {
      if (number < 2)
      {
        return false;
      }

      if (number == 2 || number == 3 || number == 5 || number == 7)
      {
        return true;
      }

      if (number % 2 == 0 || number % 3 == 0 || number % 5 == 0 || number % 7 == 0)
      {
        return false;
      }

      for (int diviseur = 11; diviseur * diviseur < number; diviseur += 2)
      {
        if (number % diviseur == 0)
        {
          return false;
        }
      }

      return true;
    }

    public static List<int> ListAllTwinPrimes(int nombre_max)
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
  }
}
