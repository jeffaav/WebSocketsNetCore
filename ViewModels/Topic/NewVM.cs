using System.ComponentModel.DataAnnotations;

namespace WebSocketsNetCore.ViewModels.Topic
{
    public class NewVM
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Ingrese un nombre")]
        public string Name { get; set; }
    }
}