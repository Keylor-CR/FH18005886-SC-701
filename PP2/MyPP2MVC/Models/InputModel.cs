using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyPP2MVC.Models;

public class InputModel
{
    [ValidarNumero]
    public string A { get; set; }

    [ValidarNumero]
    public string B { get; set; }
    public List<ResultRow>? Results { get; set; }
}
public class ValidarNumeroAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var valor = value as string;

        if (valor is null)
        {
            return new ValidationResult("El valor es requerido");
        }

        if (!Regex.IsMatch(valor, "^[01]+$"))
            return new ValidationResult("Solo se permiten los caracteres 0 y 1.");


        if (valor.Length > 8)
            return new ValidationResult("La longitud no puede exceder 8 caracteres.");

        if (valor.Length % 2 != 0)
            return new ValidationResult("La longitud del valor debe ser de 2, 4, 6 u 8 caracteres.");

        return ValidationResult.Success!;
    }
}