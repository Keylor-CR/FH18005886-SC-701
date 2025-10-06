using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyPP2MVC.Models;

public class ResultRow
{
    public string Label { get; set; }
    public string Bin { get; set; }
    public string Oct { get; set; }
    public string Dec { get; set; }
    public string Hex { get; set; }
} 