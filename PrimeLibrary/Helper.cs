using System.Numerics;

namespace PrimeLibrary
{
  public static class Helper
  {
    static readonly int[] SmallPrimes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41 };

    public static bool IsPrime(int number)
    {
      if (number < 2)
      {
        return false;
      }

      return IsPrime((ulong)number);
    }

    public static bool IsPrime(ulong number)
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

      for (ulong diviseur = 11; diviseur <= number / diviseur; diviseur += 2)
      {
        if (number % diviseur == 0)
        {
          return false;
        }
      }

      return true;
    }

    public static bool IsPrime(long number)
    {
      if (number < 2)
      {
        return false;
      }

      return IsPrime((ulong)number);
    }

    public static List<int> AllPrimesUpTo(int maxNumber)
    {
      var result = new List<int>();
      for (int number = 2; number <= maxNumber; number++)
      {
        if (IsPrime(number))
        {
          result.Add(number);
        }
      }

      return result;
    }

    public static List<int> ListAllTwinPrimes(int nombre_max)
    {
      if (nombre_max < 5)
      {
        return new List<int>();
      }

      // Crible d'Ératosthène
      bool[] estPremier = new bool[nombre_max + 1];
      for (int i = 2; i <= nombre_max; i++)
      {
        estPremier[i] = true;
      }

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
      List<int> twinPrimes = new();
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

    public static bool IsPrime(BigInteger number)
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

      for (BigInteger diviseur = 11; diviseur <= number / diviseur; diviseur += 2)
      {
        if (number % diviseur == 0)
        {
          return false;
        }
      }

      return true;
    }

    public static bool IsPrimeForBigIntegerMillerRabin(BigInteger number)
    {
      if (number < 2)
      {
        return false;
      }

      // Élimine rapidement les petits nombres et les multiples des petits premiers
      foreach (int smallPrime in SmallPrimes)
      {
        if (number == smallPrime)
        {
          return true;
        }

        if (number % smallPrime == 0)
        {
          return false;
        }
      }

      // Écrit number - 1 = d * 2^r avec d impair
      BigInteger nMinus1 = number - 1;
      BigInteger d = nMinus1;
      int r = 0;

      while (d.IsEven)
      {
        d >>= 1;
        r++;
      }

      // Témoins de Miller-Rabin
      foreach (int a in SmallPrimes)
      {
        BigInteger x = BigInteger.ModPow(a, d, number);

        if (x == 1 || x == nMinus1)
        {
          continue;
        }

        bool composite = true;

        for (int i = 1; i < r; i++)
        {
          x = (x * x) % number;

          if (x == nMinus1)
          {
            composite = false;
            break;
          }
        }

        if (composite)
        {
          return false;
        }
      }

      return true;
    }
  }
}
