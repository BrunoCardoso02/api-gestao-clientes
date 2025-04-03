namespace ClientAPI.DTOs
{
    public class ClienteDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public List<ContatoDTO> Contatos { get; set; }
        public List<EnderecoDTO> Enderecos { get; set; }
    }
}
