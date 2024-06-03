using Microsoft.AspNetCore.Mvc;


namespace ApiGabi.Controllers;

[ApiController]
[Route("[controller]")]
public class MostrarNumEnLetras : ControllerBase
{
    [HttpGet("NumeroEnLetras", Name = "NumeroEnLetras")]
    public string EnLetras(long numero)
    {
        string resultado = "";
        int cantDigitos = numero.ToString().Length;
        switch (cantDigitos)
        {
            case 1:
                resultado = MostrarEnUnidades(numero);
                break;
            case 2:
                resultado = MostrarEnDecenas(numero);
                break;
            case 3:
                resultado = MostrarEnCentenas(numero);
                break;
            case 4:
            case 5:
            case 6:
                resultado = MostrarEnMiles(numero);
                break;
            default:
                resultado = MostrarEnMillones(numero);
                break;
        }
        return resultado;
    }
    private string[] unidades = { "CERO", "UN", "DOS", "TRES", "CUATRO", "CINCO", "SEIS", "SIETE", "OCHO", "NUEVE" };
    private string[] decenasRedondas = { "", "DIEZ", "VEINTE", "TREINTA", "CUARENTA", "CINCUENTA", "SESENTA", "SETENTA", "OCHENTA", "NOVENTA", "CIEN" };
    private string[] decena = { "DIEZ", "ONCE", "DOCE", "TRECE", "CATORCE", "QUINCE", "DIECISÉIS", "DIECISIETE", "DIECIOCHO", "DIECINUEVE", "VEINTI" };
    private string[] grandesRedondos = { "CIENTO", "MIL", "MILLÓN" };
    private string[] centanasList = { "", "", "DOSCIENTOS", "TRESCIENTOS", "CUATROCIENTOS", "QUINIENTOS", "SEISCIENTOS", "SETECIENTOS", "OCHOCIENTOS", "NOVECIENTOS" };
    private string[] grande = { "CIENTOS", "MILES", "MILLONES" };

    private string MostrarEnUnidades(long numero)
    {
        if (numero == 1) return "UNO";
        return unidades[numero];
    }
    private string MostrarEnDecenas(long numero)
    {
        if (numero >= 10 && numero < 20)
        {
            return decena[numero - 10];
        }
        else if (numero == 20) return decenasRedondas[numero / 10];
        else if (numero > 20 && numero < 30)
        {
            int segundoDigito = ((int)numero % 10);
            return decena[10] + unidades[segundoDigito];
        }
        else
        {
            int primerDigito = (int)numero / 10;
            int segundoDigito = (int)numero % 10;

            string resultado = decenasRedondas[primerDigito];
            if (segundoDigito > 0)
            {
                resultado += " Y " + unidades[segundoDigito];
            }
            return resultado;
        }
    }
    private string MostrarEnCentenas(long numero)
    {
        int centenas = (int)(numero / 100);
        int decenas = (int)((numero % 100) / 10);
        int unidades = (int)(numero % 10);

        string resultado = "";

        if (centenas == 1 && decenas == 0 && unidades == 0)
        {
            resultado = decenasRedondas[numero / 10];
        }
        else if (centenas == 1 && decenas >= 0)
        {
            resultado += grandesRedondos[0];
        }
        else if (centenas > 1)
        {
            if (centenas > 1)
                resultado += this.centanasList[centenas];
        }
        if (decenas > 0)
        {
            if (resultado.Length > 0)
                resultado += " ";
            resultado += MostrarEnDecenas(decenas * 10 + unidades);
        }
        else if (unidades > 0)
        {
            if (resultado.Length > 0)
                resultado += " Y ";
            resultado += this.unidades[unidades];
        }
        return resultado;
    }
    private string MostrarEnMiles(long numero)
    {
        int miles = (int)(numero / 1000);
        int centenas = (int)((numero % 1000) / 100);
        int decenas = (int)((numero % 100) / 10);
        int unidades = (int)(numero % 10);

        string resultado = "";

        if (miles == 1 && centenas == 0 && decenas == 0 && unidades == 0)
        {
            resultado += grandesRedondos[miles];
        }

        if (miles >= 1)
        {
            resultado += MostrarEnCentenas(miles) + " MIL";
        }

        if (centenas > 0)
        {
            if (resultado.Length > 0)
                resultado += " ";
            resultado += MostrarEnCentenas(centenas * 100 + decenas * 10 + unidades);
        }
        else if (decenas > 0 || unidades > 0)
        {
            if (resultado.Length > 0)
                resultado += " Y ";
            resultado += MostrarEnDecenas(decenas * 10 + unidades);
        }

        return resultado;
    }
    private string MostrarEnMillones(long numero)
    {
        if (numero > 999999999)
        {
            return "Error: Número fuera del rango !!! ";
        }
        int millones = (int)(numero / 1000000);
        int miles = (int)((numero % 1000000) / 1000);
        int centenas = (int)(numero % 1000);

        string resultado = "";

        if (millones > 0)
        {
            resultado += MostrarEnCentenas(millones) + " MILLONES";
        }

        if (miles > 0)
        {
            if (resultado.Length > 0)
                resultado += " ";
            resultado += MostrarEnMiles(miles * 1000 + centenas);
        }
        else if (centenas > 0)
        {
            if (resultado.Length > 0)
                resultado += " Y ";
            resultado += MostrarEnCentenas(centenas);
        }
        return resultado;
    }
}








