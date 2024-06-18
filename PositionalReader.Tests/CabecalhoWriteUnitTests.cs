using System.Reflection;

namespace PositionalReader.Tests;
public class CabecalhoUnitTests
{
    private readonly Cabecalho _sut;
    public CabecalhoUnitTests()
    {
        _sut = new Cabecalho();
    }

    [Fact]
    public void GetCabecalho()
    {
        _sut.NumeroControle = "0";
        Assert.Equal("0000000", _sut.Write());
    }

    [Fact]
    public void GetDataRemessa()
    {
        _sut.DataRemessa = "20241220";
        Assert.Equal("20241220", _sut.Write());
    }

    [Fact]
    public void GetCodigoRemetente()
    {
        _sut.CodigoRemetente = "312";
        Assert.Equal("312", _sut.Write());
    }

    [Fact]
    public void GetVersaoArquivo()
    {
        _sut.VersaoArquivo = "1";
        Assert.Equal("0001", _sut.Write());
    }

    [Fact]
    public void GetNomeArquivo()
    {
        _sut.NomeArquivo = "ICF607";
        Assert.Equal("ICF607", _sut.Write());
    }

    [Fact]
    public void GetFiller()
    {
        _sut.Filler = "";
        Assert.Equal(162, _sut.Write().Length);
    }

    [Fact]
    public void GetSequencialArquivo()
    {
        _sut.SequencialArquivo = "1";
        Assert.Equal("0000000001", _sut.Write());
    }

    [Fact]
    public void GetWrite()
    {
        _sut.NumeroControle = "0";
        _sut.DataRemessa = "20241220";
        _sut.CodigoRemetente = "312";
        _sut.VersaoArquivo = "1";
        _sut.NomeArquivo = "ICF607";
        _sut.Filler = "";
        _sut.SequencialArquivo = "1";
        Assert.Equal(200, _sut.Write().Length);
    }

    [Fact]
    public void GetRead()
    {
        string linha = "0000000202412203120001ICF607                                                                                                                                                                  0000000001";
        _sut.Read(linha);
        Assert.Equal("0000000", _sut.NumeroControle);
        Assert.Equal("20241220", _sut.DataRemessa);
        Assert.Equal("312", _sut.CodigoRemetente);
        Assert.Equal("0001", _sut.VersaoArquivo);
        Assert.Equal("ICF607", _sut.NomeArquivo);
        Assert.Equal("                                                                                                                                                                  ", _sut.Filler);
        Assert.Equal("0000000001", _sut.SequencialArquivo);
        Assert.Equal(200, _sut.Write().Length);
    }
}

public enum PictureEnum
{
    Int = 1,
    Alfa = 2
}

//[ArquivoLayout("ICF607", 200)]
public class Cabecalho
{
    [PermissionRequired(1, 7, PictureEnum.Int)]
    public string NumeroControle { get; set; }

    [PermissionRequired(2, 8, PictureEnum.Int)]
    public string DataRemessa { get; set; }

    [PermissionRequired(3, 3, PictureEnum.Int)]
    public string CodigoRemetente { get; set; }

    [PermissionRequired(4, 4, PictureEnum.Int)]
    public string VersaoArquivo { get; set; }

    [PermissionRequired(5, 6, PictureEnum.Alfa)]
    public string NomeArquivo { get; set; }

    [PermissionRequired(6, 162, PictureEnum.Alfa)]
    public string Filler { get; set; }

    [PermissionRequired(7, 10, PictureEnum.Int)]
    public string SequencialArquivo { get; set; }

    public string Write()
    {
        List<PermissionRequired> PermissionRequiredList = new List<PermissionRequired>();
        Type type = this.GetType();
        PropertyInfo[] properties = type.GetProperties();
        if (properties == null | properties.Length == 0)
            return "";
        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo property = properties[i];
            var attr = property.GetCustomAttributes(typeof(PermissionRequiredAttribute), true)
                .Cast<PermissionRequiredAttribute>()
                .Select(x => x.PermissionRequired)
                .First();
            attr.Valor = property.GetValue(this)!;
            PermissionRequiredList.Add(attr);
        }
        var lista = PermissionRequiredList
            .OrderBy(x => x.Ordem)
            .Select(x =>
            {
                if (x.Valor != null)
                {
                    return x.Tipagem == PictureEnum.Int
                        ? x.Valor.ToString()!.PadLeft(x.Tamanho, '0')
                        : x.Valor.ToString()!.PadLeft(x.Tamanho, ' ');
                }
                return null;
            })
            .ToArray();
        return String.Join("", lista);
    }

    public void Read(string linha)
    {
        int tamanhoInicial = 0;
        int tamanhoFinal = 0;
        Type type = this.GetType();
        //var nameFile = type.GetCustomAttributes(typeof(ArquivoLayoutAttribute), true)
        //    .Cast<ArquivoLayoutAttribute>()
        //    .Select(x => new { x.Arquivo, x.Tamanho })
        //    .First();
        PropertyInfo[] properties = type.GetProperties();
        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo property = properties[i];
            var attr = property.GetCustomAttributes(typeof(PermissionRequiredAttribute), true)
                .Cast<PermissionRequiredAttribute>()
                .Select(x => x.PermissionRequired)
                .First();

            tamanhoFinal += attr.Tamanho;
            property.SetValue(this, linha[tamanhoInicial..tamanhoFinal], null);
            tamanhoInicial += attr.Tamanho;
        }
    }
}

public record PermissionRequired
{
    public int Ordem { get; set; }
    public int Tamanho { get; set; }
    public PictureEnum Tipagem { get; set; }
    public object Valor { get; set; }
}

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class PermissionRequiredAttribute : Attribute
{
    public PermissionRequiredAttribute(int ordem, int tamanho, PictureEnum picture)
    {
        PermissionRequired = new PermissionRequired()
        {
            Ordem = ordem,
            Tamanho = tamanho,
            Tipagem = picture
        };
    }
    public PermissionRequired PermissionRequired { get; }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ArquivoLayoutAttribute : Attribute
{
    public ArquivoLayoutAttribute(string arquivo, int tamanho)
    {
        Arquivo = arquivo;
        Tamanho = tamanho;
    }
    public string Arquivo { get; }
    public int Tamanho { get; }
}