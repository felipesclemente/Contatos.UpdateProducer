using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Contatos.UpdateProducer.DTO
{
    public class AtualizacaoContato
    {
        [Required(ErrorMessage = "O ID deve ser fornecido. Utilize a rota adequada para buscar o ID de um contato.")]
        public long IdContato { get; set; }

        [MaxLength(1024, ErrorMessage = "O novo nome completo do contato não deve exceder 1024 caracteres.")]
        public string? NovoNomeCompleto { get; set; }

        [Range(11, 99, ErrorMessage = "O novo DDD do contato deve ser um valor entre 11 e 99.")]
        public int? NovoDDD { get; set; }

        public int? NovoTelefone { get; set; }

        [EmailAddress(ErrorMessage = "O novo endereço de e-mail do contato deve ser informado no formato: nome@servidor.dominio")]
        [MaxLength(512, ErrorMessage = "O novo endereço de e-mail do contato não deve exceder 512 caracteres.")]
        public string? NovoEmail { get; set; }

        public AtualizacaoContato(long idContato, string? novoNomeCompleto, int? novoDDD, int? novoTelefone, string? novoEmail)
        {
            IdContato = idContato;
            NovoNomeCompleto = novoNomeCompleto;
            NovoDDD = novoDDD;
            NovoTelefone = novoTelefone;
            NovoEmail = novoEmail;
        }
    }
}
