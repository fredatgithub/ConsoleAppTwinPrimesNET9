namespace PrimeLibrary.Tests
{
  public class UnitTestPrimes
  {
    [Theory]
    [InlineData(1, false)]
    [InlineData(2, true)]
    [InlineData(3, true)]
    [InlineData(4, false)]
    [InlineData(5, true)]
    [InlineData(6, false)]
    [InlineData(7, true)]
    [InlineData(8, false)]
    [InlineData(9, false)]
    [InlineData(10, false)]
    [InlineData(11, true)]
    [InlineData(12, false)]
    [InlineData(13, true)]
    [InlineData(17, true)]
    [InlineData(19, true)]
    [InlineData(23, true)]
    [InlineData(29, true)]
    [InlineData(31, true)]
    [InlineData(37, true)]
    public void Test_IsPrime_several_numbers(int source, bool expected)
    {
      bool result = Helper.IsPrime(source);
      Assert.Equal(expected, result);
    }

    // Test d'exception
    [Fact]
    public void Diviser_ParZero_LeveUneException()
    {
      int diviseur = 0;
      Assert.Throws<DivideByZeroException>(() => 10 / diviseur);
    }

    [Fact]
    public void IntParse_TexteNonNumerique_LeveUneFormatException()
    {
      Assert.Throws<FormatException>(() => int.Parse("abc"));
    }

    [Fact]
    public void ListeVide_AccesIndexHorsLimites_LeveUneException()
    {
      var liste = new List<int>();

      Assert.Throws<ArgumentOutOfRangeException>(() => liste[0]);
    }

    // Nombres inférieurs à 2 : jamais premiers
    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-17)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    public void IsPrime_NombreInferieurA2_RetourneFalse(int nombre)
    {
      Assert.False(Helper.IsPrime(nombre));
    }

    // Petits premiers, dont ceux traités par les cas particuliers (2, 3, 5, 7)
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
    [InlineData(29)]
    [InlineData(31)]
    [InlineData(97)]
    [InlineData(101)]
    public void IsPrime_PetitNombrePremier_RetourneTrue(int nombre)
    {
      Assert.True(Helper.IsPrime(nombre));
    }

    // Grands premiers connus
    [Theory]
    [InlineData(7919)]        // 1000e nombre premier
    [InlineData(104729)]      // 10 000e nombre premier
    [InlineData(1000000007)]  // premier très utilisé en algorithmique
    public void IsPrime_GrandNombrePremier_RetourneTrue(int nombre)
    {
      Assert.True(Helper.IsPrime(nombre));
    }

    // Nombres composés simples, dont les multiples de 2, 3, 5 et 7
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
    [InlineData(91)]     // 7 x 13
    [InlineData(100)]
    [InlineData(1000)]
    public void IsPrime_NombreCompose_RetourneFalse(int nombre)
    {
      Assert.False(Helper.IsPrime(nombre));
    }

    // Composés dont les plus petits facteurs sont >= 11 (passent le filtre 2/3/5/7)
    [Theory]
    [InlineData(143)]   // 11 x 13
    [InlineData(221)]   // 13 x 17
    [InlineData(323)]   // 17 x 19
    [InlineData(1001)]  // 7 x 11 x 13
    public void IsPrime_ComposeDeGrandsFacteurs_RetourneFalse(int nombre)
    {
      Assert.False(Helper.IsPrime(nombre));
    }

    // Carrés de nombres premiers >= 11 : cas limite de la boucle
    [Theory]
    [InlineData(121)]    // 11²
    [InlineData(169)]    // 13²
    [InlineData(289)]    // 17²
    [InlineData(361)]    // 19²
    [InlineData(529)]    // 23²
    [InlineData(10201)]  // 101²
    public void IsPrime_CarreDUnPremier_RetourneFalse(int nombre)
    {
      Assert.False(Helper.IsPrime(nombre));
    }

    // Plus grande valeur d'int : 2^31 - 1 est un nombre premier (Mersenne)
    [Fact]
    public void IsPrime_IntMaxValue_RetourneTrue()
    {
      Assert.True(Helper.IsPrime(int.MaxValue));
    }

    // Bornes qui ne contiennent aucun nombre premier : liste vide
    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-10)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    public void AllPrimesUpTo_BorneInferieureA2_RetourneListeVide(int maxNumber)
    {
      var result = Helper.AllPrimesUpTo(maxNumber);

      Assert.Empty(result);
    }

    // Petites bornes : on vérifie la liste exacte
    [Fact]
    public void AllPrimesUpTo_2_RetourneUniquementDeux()
    {
      Assert.Equal(new[] { 2 }, Helper.AllPrimesUpTo(2));
    }

    [Fact]
    public void AllPrimesUpTo_3_RetourneDeuxEtTrois()
    {
      Assert.Equal(new[] { 2, 3 }, Helper.AllPrimesUpTo(3));
    }

    [Fact]
    public void AllPrimesUpTo_30_RetourneLesDixPremiersNombresPremiers()
    {
      var attendu = new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 };

      Assert.Equal(attendu, Helper.AllPrimesUpTo(30));
    }

    // La borne est incluse quand elle est première
    [Fact]
    public void AllPrimesUpTo_BorneEstPremiere_LInclutDansLeResultat()
    {
      var result = Helper.AllPrimesUpTo(13);

      Assert.Equal(13, result[^1]);
    }

    // La borne n'est pas incluse quand elle est composée
    [Fact]
    public void AllPrimesUpTo_BorneEstCompose_LExclutDuResultat()
    {
      var result = Helper.AllPrimesUpTo(14);

      Assert.Equal(13, result[^1]);
      Assert.DoesNotContain(14, result);
    }

    // Nombre de premiers connus (fonction de comptage π(n))
    [Theory]
    [InlineData(10, 4)]
    [InlineData(100, 25)]
    [InlineData(1000, 168)]
    [InlineData(10000, 1229)]
    public void AllPrimesUpTo_Borne_RetourneLeBonNombreDePremiers(int maxNumber, int nombreAttendu)
    {
      Assert.Equal(nombreAttendu, Helper.AllPrimesUpTo(maxNumber).Count);
    }

    // Test de non-régression : 121 = 11² et 169 = 13² ne doivent jamais apparaître
    [Fact]
    public void AllPrimesUpTo_ContientDesCarresDePremiers_LesExclut()
    {
      var result = Helper.AllPrimesUpTo(300);

      Assert.DoesNotContain(121, result);
      Assert.DoesNotContain(169, result);
      Assert.DoesNotContain(289, result);
    }

    // Propriétés de la liste : triée, sans doublons, uniquement des premiers
    [Fact]
    public void AllPrimesUpTo_Resultat_EstTrieSansDoublons()
    {
      var result = Helper.AllPrimesUpTo(1000);

      Assert.Equal(result.OrderBy(n => n), result);
      Assert.Equal(result.Count, result.Distinct().Count());
    }

    [Fact]
    public void AllPrimesUpTo_Resultat_NeContientQueDesPremiers()
    {
      var result = Helper.AllPrimesUpTo(1000);

      Assert.All(result, n => Assert.True(Helper.IsPrime(n)));
    }

    // Comparaison avec une référence indépendante (crible d'Ératosthène écrit dans le test)
    [Theory]
    [InlineData(50)]
    [InlineData(500)]
    [InlineData(10000)]
    public void AllPrimesUpTo_ComparéAuCribleDEratosthene_DonneLeMemeResultat(int maxNumber)
    {
      var attendu = CribleDEratosthene(maxNumber);

      Assert.Equal(attendu, Helper.AllPrimesUpTo(maxNumber));
    }

    private static List<int> CribleDEratosthene(int max)
    {
      var estPremier = new bool[max + 1];
      Array.Fill(estPremier, true);
      estPremier[0] = false;
      if (max >= 1) estPremier[1] = false;

      for (int i = 2; (long)i * i <= max; i++)
      {
        if (!estPremier[i]) continue;
        for (int multiple = i * i; multiple <= max; multiple += i)
        {
          estPremier[multiple] = false;
        }
      }

      var result = new List<int>();
      for (int i = 2; i <= max; i++)
      {
        if (estPremier[i]) result.Add(i);
      }

      return result;
    }

    [Theory]
    [InlineData(long.MinValue)]
    [InlineData(-7L)]
    [InlineData(0L)]
    [InlineData(1L)]
    public void IsPrime_Long_InferieurA2_RetourneFalse(long nombre)
    {
      Assert.False(Helper.IsPrime(nombre));
    }

    [Theory]
    [InlineData(2147483647L)]        // 2^31 - 1
    [InlineData(4294967291L)]        // plus grand premier < 2^32
    [InlineData(999999999989L)]      // plus grand premier < 10^12
    [InlineData(1000000000039L)]     // plus petit premier > 10^12
    public void IsPrime_Long_GrandPremier_RetourneTrue(long nombre)
    {
      Assert.True(Helper.IsPrime(nombre));
    }

    [Theory]
    [InlineData(4294967297L)]        // 641 x 6700417 (nombre de Fermat F5)
    [InlineData(1000006000009L)]     // 1000003²
    [InlineData(1000000000000L)]     // 10^12
    public void IsPrime_Long_GrandCompose_RetourneFalse(long nombre)
    {
      Assert.False(Helper.IsPrime(nombre));
    }

    [Theory]
    [InlineData(0UL)]
    [InlineData(1UL)]
    public void IsPrime_ULong_InferieurA2_RetourneFalse(ulong nombre)
    {
      Assert.False(Helper.IsPrime(nombre));
    }

    [Theory]
    [InlineData(4294967291UL)]
    [InlineData(1000000000039UL)]
    public void IsPrime_ULong_GrandPremier_RetourneTrue(ulong nombre)
    {
      Assert.True(Helper.IsPrime(nombre));
    }

    [Theory]
    [InlineData(4294967297UL)]
    [InlineData(1000006000009UL)]
    [InlineData(ulong.MaxValue)]     // divisible par 3, donc rapide à rejeter
    public void IsPrime_ULong_GrandCompose_RetourneFalse(ulong nombre)
    {
      Assert.False(Helper.IsPrime(nombre));
    }
  }
}
