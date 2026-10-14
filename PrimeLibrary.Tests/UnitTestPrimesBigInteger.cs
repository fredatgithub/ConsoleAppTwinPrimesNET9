using System.Numerics;

namespace PrimeLibrary.Tests
{
  public class UnitTestPrimesBigInteger
  {
    // Remplacez NumberHelper par la classe qui contient votre méthode IsPrime
    private static bool IsPrime(BigInteger n) => Helper.IsPrime(n);

    // ---------- Valeurs hors domaine ----------

    [Theory]
    [InlineData(-100)]
    [InlineData(-7)]
    [InlineData(-2)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    public void IsPrime_NombresInferieursA2_RetourneFalse(long n)
    {
      Assert.False(IsPrime(n));
    }

    // ---------- Petits nombres premiers ----------

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(7)]
    [InlineData(11)]
    [InlineData(13)]
    [InlineData(17)]
    [InlineData(19)]
    [InlineData(23)]
    [InlineData(97)]
    [InlineData(101)]
    [InlineData(7919)]      // 1000e nombre premier
    [InlineData(104729)]    // 10000e nombre premier
    public void IsPrime_PetitsPremiers_RetourneTrue(long n)
    {
      Assert.True(IsPrime(n));
    }

    // ---------- Composés ----------

    [Theory]
    [InlineData(4)]
    [InlineData(6)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    [InlineData(15)]
    [InlineData(21)]
    [InlineData(25)]
    [InlineData(35)]
    [InlineData(49)]
    [InlineData(77)]
    [InlineData(100)]
    public void IsPrime_PetitsComposes_RetourneFalse(long n)
    {
      Assert.False(IsPrime(n));
    }

    // Carrés de premiers : cas limites de la condition diviseur <= n / diviseur
    [Theory]
    [InlineData(121)]       // 11²
    [InlineData(169)]       // 13²
    [InlineData(289)]       // 17²
    [InlineData(361)]       // 19²
    [InlineData(529)]       // 23²
    [InlineData(10201)]     // 101²
    [InlineData(1000006000009)] // 1000003²
    public void IsPrime_CarresDePremiers_RetourneFalse(long n)
    {
      Assert.False(IsPrime(n));
    }

    // Produits de deux premiers proches (pire cas pour la division par essais)
    [Theory]
    [InlineData(143)]       // 11 × 13
    [InlineData(221)]       // 13 × 17
    [InlineData(10403)]     // 101 × 103
    [InlineData(1000036000099)] // 1000003 × 1000033
    public void IsPrime_ProduitsDeDeuxPremiers_RetourneFalse(long n)
    {
      Assert.False(IsPrime(n));
    }

    // Nombres de Carmichael : trompent le test de Fermat, pas la division par essais
    [Theory]
    [InlineData(561)]
    [InlineData(1105)]
    [InlineData(1729)]
    [InlineData(2465)]
    [InlineData(2821)]
    [InlineData(6601)]
    [InlineData(8911)]
    public void IsPrime_NombresDeCarmichael_RetourneFalse(long n)
    {
      Assert.False(IsPrime(n));
    }

    // ---------- Premiers plus grands ----------

    [Theory]
    [InlineData(1000003)]
    [InlineData(1000033)]
    [InlineData(1000000007)]
    [InlineData(1000000009)]
    [InlineData(2147483647)]    // 2³¹ − 1 (premier de Mersenne)
    public void IsPrime_GrandsPremiers_RetourneTrue(long n)
    {
      Assert.True(IsPrime(n));
    }

    // ---------- Limites de long / ulong et au-delà (spécifique à BigInteger) ----------

    [Fact]
    public void IsPrime_UlongMaxValue_RetourneFalse()
    {
      // 2⁶⁴ − 1 est divisible par 3, 5, 17, 257, ...
      Assert.False(IsPrime(ulong.MaxValue));
    }

    [Fact]
    public void IsPrime_GrandNombrePair_RetourneFalse()
    {
      Assert.False(IsPrime(BigInteger.Pow(2, 100)));
    }

    [Fact]
    public void IsPrime_GrandMultipleDePetitPremier_RetourneFalse()
    {
      // Multiple de 11 par construction
      BigInteger n = 11 * BigInteger.Pow(10, 30) + 11 * 13;
      Assert.False(IsPrime(n));
    }

    [Fact]
    public void IsPrime_Puissance10Plus1Divisible_RetourneFalse()
    {
      // 10²¹ + 1 est divisible par 7 (car 10³ ≡ -1 mod 7 et 21 est un multiple de 3 impair)
      Assert.False(IsPrime(BigInteger.Pow(10, 21) + 1));
    }

    // ---------- Vérification croisée avec un crible d'Ératosthène ----------

    [Fact]
    public void IsPrime_CorrespondAuCribleSur0Jusqua10000()
    {
      const int max = 10_000;
      bool[] estPremier = new bool[max + 1];
      for (int i = 2; i <= max; i++) estPremier[i] = true;

      for (int i = 2; (long)i * i <= max; i++)
      {
        if (!estPremier[i]) continue;
        for (int j = i * i; j <= max; j += i) estPremier[j] = false;
      }

      for (int n = 0; n <= max; n++)
      {
        Assert.True(estPremier[n] == IsPrime(n), $"Résultat incorrect pour n = {n}");
      }
    }

    [Fact]
    public void IsPrime_NombreDePremiersInferieursA10000_Est1229()
    {
      int compte = 0;
      for (int n = 0; n < 10_000; n++)
      {
        if (IsPrime(n)) compte++;
      }

      Assert.Equal(1229, compte);
    }
  }
}
