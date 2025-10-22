using System.ComponentModel.DataAnnotations;
namespace MVC.Models;

public class TheModel
{
    [Length(5, 25, ErrorMessage = "El Phrase debe de ser de entre 5 y 10 characteres.")]
    public required string Phrase { get; set; }
    public Dictionary<char, int> Counts { get; set; } = [];
    public string Lower { get; set; } = string.Empty;
    public string Upper { get; set; } = string.Empty; //https://chatgpt.com/s/t_68f82d73a1548191b13ff304d0b6a034
}
