using System;
using System.Text;
Cabecalho cabecalho 


Console.WriteLine("Hello, World!");

//CriacaoArquivos.PrintAuthorInfo(typeof(Cabecalho));
//public enum PictureEnum
//{
//    Inteiro = 1,
//    AlfaNumerico = 2
//}
public class Cabecalho
{
    //[Author(1, 1, 7, PictureEnum.Inteiro)]
    public string NumeroControle { get; set; }
    //public string DataRemessa { get; set; }
    //public string CodigoRemetente { get; set; }
    //public string VersaoArquivo { get; set; }
    //public string NomeArquivo { get; set; }
    //public string Sequencial { get; set; }
}

//public static class CriacaoArquivos
//{
//    public static string PrintAuthorInfo(System.Type t)
//    {
//        StringBuilder linha = new StringBuilder();
//        try
//        {
//            var properties = t.GetProperties();
//            foreach (var property in properties)
//            {
//                System.Attribute[] attrs = System.Attribute.GetCustomAttributes(property);  // Reflection.
//                if (attr is AuthorAttribute a)
//                {
//                    System.Console.WriteLine($"   {a.GetName()}, version {a.Version:f}");
//                    linha.AppendFormat();
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            throw;
//        }
//    }
//}


//[AttributeUsage(AttributeTargets.Property,
//                       AllowMultiple = false)  // Multiuse attribute.
//]
//public class AuthorAttribute : System.Attribute
//{
//    string Name;
//    public double Version;

//    public AuthorAttribute(int numero, int posicaoInicial, int posicaoFinal, PictureEnum tipo, string name)
//    {
//        Name = name;

//        // Default value.
//        Version = 1.0;
//    }

//    public string GetName() => Name;
//}