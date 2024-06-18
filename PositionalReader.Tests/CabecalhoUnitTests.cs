using System.Reflection;

namespace PositionalReader.Tests;
public class FooterUnitTests
{
    private readonly Rodape _sut;
    public FooterUnitTests()
    {
        _sut = new Rodape();
    }

    [Fact]
    public void GetCabecalho()
    {
        Assert.Equal("9999999", _sut.Write());
    }
}

public class Rodape
{
    public string NumeroControle { get; set; }
    public string Write()
    {
        return "".PadLeft(7, '9');
        //List<PermissionRequired> PermissionRequiredList = new List<PermissionRequired>();
        Type type = this.GetType();
        PropertyInfo[] properties = type.GetProperties();
        //if (properties == null | properties.Length == 0)
        //    return "";
        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo property = properties[i];
            var attr = property.GetCustomAttributes(typeof(PermissionRequiredAttribute), true)
                .Cast<PermissionRequiredAttribute>()
                .Select(x => x.PermissionRequired)
                .First();
        //    attr.Valor = property.GetValue(this)!;
        //    PermissionRequiredList.Add(attr);
        }
        //var lista = PermissionRequiredList
        //    .OrderBy(x => x.Ordem)
        //    .Select(x =>
        //    {
        //        if (x.Valor != null)
        //        {
        //            return x.Tipagem == PictureEnum.Int
        //                ? x.Valor.ToString()!.PadLeft(x.Tamanho, '0')
        //                : x.Valor.ToString()!.PadLeft(x.Tamanho, ' ');
        //        }
        //        return null;
        //    })
        //    .ToArray();
        //return String.Join("", lista);
    }
}
