using System.ComponentModel.Design;
using System.Globalization;
using Azure.Messaging;
using Dapper;
using DataAccess;
using Dominio;

public partial class Program
{
    public static Contexto contexto = new Contexto();
    public static AluguelRepositorio aluguelRepositorio = new AluguelRepositorio(contexto);
    public static ClienteRepositorio clienteRepositorio = new ClienteRepositorio(contexto);
    public static JogoRepositorio jogoRepositorio = new JogoRepositorio(contexto);
    public static RelatorioRepositorio relatorioRepositorio = new RelatorioRepositorio(contexto);
    public static int opcao;
    public static void Main(string[] args)
    {
        MenuPrincipal();
    }

    static void MenuPrincipal()
    {
        try
        {
            do
            {
                Console.WriteLine("=== Menu Principal ===");
                Console.WriteLine("1. Gerenciamento de Jogos");
                Console.WriteLine("2. Gerenciamento de Clientes");
                Console.WriteLine("3. Registro de Alugueis");
                Console.WriteLine("4. Devoluções");
                Console.WriteLine("5. Relatórios");
                Console.WriteLine("6. Sair");
                Console.WriteLine("Escolha uma opção:");
                opcao = int.Parse(Console.ReadLine());

                switch ((MenuPrincipalEnum)opcao)
                {
                    case MenuPrincipalEnum.GerenciamentoDeJogos:
                        GerenciamentoDeJogos();
                        break;

                    case MenuPrincipalEnum.GerenciamentoDeClientes:
                        GerenciamentoDeClientes();
                        break;

                    case MenuPrincipalEnum.RegistroDeAlugueis:
                        RegistroDeAlugueis();
                        break;

                    case MenuPrincipalEnum.Devolucoes:
                        Devolucoes();
                        break;

                    case MenuPrincipalEnum.Relatorios:
                        Relatorios();
                        break;

                    case MenuPrincipalEnum.Sair:
                        Console.WriteLine("Obrigado e volte sempre!");
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            } while (opcao != 6);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }


    static void GerenciamentoDeJogos()
    {
        try
        {
            Console.WriteLine("=== Gerenciamento de Jogos ===");
            Console.WriteLine("1. Cadastrar Novo Jogo");
            Console.WriteLine("2. Editar Jogo Existente");
            Console.WriteLine("3. Excluir Jogo");
            Console.WriteLine("4. Voltar ao Menu Principal");
            Console.WriteLine("Escolha uma opção:");
            opcao = int.Parse(Console.ReadLine());

            switch ((GerenciamentoDeJogosEnum)opcao)
            {
                case GerenciamentoDeJogosEnum.CadastrarNovoJogo:
                    CadastrarNovoJogo();
                    break;

                case GerenciamentoDeJogosEnum.EditarJogoExistente:
                    EditarJogoExistente();
                    break;

                case GerenciamentoDeJogosEnum.ExcluirJogo:
                    ExcluirJogo();
                    break;

                case GerenciamentoDeJogosEnum.MenuPrincipal:
                    MenuPrincipal();
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }

    static void CadastrarNovoJogo()
    {
        try
        {
            Jogo jogo = new Jogo();

            Console.WriteLine("=== Cadastro Novo Jogo ===");
            Console.WriteLine("Nome do Jogo:");
            jogo.Nome = Console.ReadLine();

            Console.WriteLine("-------------------------------------------------");

            Console.WriteLine("Gênero:");
            jogo.Genero = Console.ReadLine();

            Console.WriteLine("-------------------------------------------------");

            Console.WriteLine("Plataforma:");
            jogo.Plataforma = Console.ReadLine();

            Console.WriteLine("-------------------------------------------------");

            Console.WriteLine("Disponibilidade: ('Disponivel'/'Indisponivel')");
            jogo.Disponibilidade = Console.ReadLine();

            Console.WriteLine("-------------------------------------------------");

            Console.WriteLine("Confirmação: Deseja salvar este jogo? (S/N)");
            string confirmacao = Console.ReadLine();

            Console.WriteLine("-------------------------------------------------");

            if (confirmacao == "S")
            {
                jogoRepositorio.AdicionarJogo(jogo);

                Console.WriteLine($"O jogo {jogo.Nome} foi salvo!");
            }
            else
            {
                Console.WriteLine("O jogo não foi salvo!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }

    static void EditarJogoExistente()
    {
        try
        {
            Console.WriteLine("=== Editar Jogo Existente ===");

            var jogos = contexto.Jogos.ToList();

            foreach (var item in jogos)
            {
                Console.WriteLine($"ID: {item.Id} | Nome: {item.Nome}");
            }

            Console.WriteLine("Digite o ID do Jogo para editar:");
            int jogoId = int.Parse(Console.ReadLine());

            var buscandoIdJogo = jogoRepositorio.ObterJogo(jogoId);

            if (buscandoIdJogo != null)
            {
                Console.WriteLine($"Jogo encontrado: {buscandoIdJogo.Nome}");

                Console.WriteLine("-------------------------------------------------");

                Console.WriteLine("Novo nome:");
                string novoNomeJogo = Console.ReadLine();

                if (!string.IsNullOrEmpty(novoNomeJogo))
                {
                    buscandoIdJogo.Nome = novoNomeJogo;
                }

                buscandoIdJogo.Nome = novoNomeJogo;

                Console.WriteLine("-------------------------------------------------");

                Console.WriteLine("Novo gênero:");
                string novoGeneroJogo = Console.ReadLine();

                if (!string.IsNullOrEmpty(novoGeneroJogo))
                {
                    buscandoIdJogo.Genero = novoGeneroJogo;
                }

                buscandoIdJogo.Genero = novoGeneroJogo;

                Console.WriteLine("-------------------------------------------------");

                Console.WriteLine("Nova plataforma:");
                string novaPlataformaJogo = Console.ReadLine();

                if (!string.IsNullOrEmpty(novaPlataformaJogo))
                {
                    buscandoIdJogo.Plataforma = novaPlataformaJogo;
                }

                buscandoIdJogo.Plataforma = novaPlataformaJogo;

                Console.WriteLine("-------------------------------------------------");

                Console.WriteLine("Nova disponibilidade:");
                string novaDisponibilidadeJogo = Console.ReadLine();

                if (!string.IsNullOrEmpty(novaDisponibilidadeJogo))
                {
                    buscandoIdJogo.Disponibilidade = novaDisponibilidadeJogo;
                }

                buscandoIdJogo.Disponibilidade = novaDisponibilidadeJogo;

                Console.WriteLine("-------------------------------------------------");

                Console.WriteLine("Confirmação: Deseja salvar as alterações? (S/N)");
                string confirmacao = Console.ReadLine();

                Console.WriteLine("-------------------------------------------------");

                if (confirmacao == "S")
                {
                    jogoRepositorio.AtualizarJogo(buscandoIdJogo);

                    Console.WriteLine($"O jogo {buscandoIdJogo.Nome} foi alterado!");
                }
                else
                {
                    Console.WriteLine("O jogo não foi alterado!");
                }
            }
            else
            {
                throw new Exception("Jogo não encontrado.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }

    static void ExcluirJogo()
    {
        try
        {
            Console.WriteLine("=== Excluir Jogo ===");

            var jogos = contexto.Jogos.ToList();

            foreach (var item in jogos)
            {
                Console.WriteLine($"ID: {item.Id} | Nome: {item.Nome}");
            }
            Console.WriteLine("Digite o ID do Jogo para exluir:");
            int jogoId = int.Parse(Console.ReadLine());

            var buscandoIdJogo = jogoRepositorio.ObterJogo(jogoId);

            if (buscandoIdJogo != null)
            {
                Console.WriteLine($"Jogo encontrado: {buscandoIdJogo.Nome}");

                Console.WriteLine("-------------------------------------------------");

                Console.WriteLine("Confirmação: Tem certeza que deseja excluir? (S/N)");
                string confirmacao = Console.ReadLine();

                Console.WriteLine("-------------------------------------------------");

                if (confirmacao == "S")
                {
                    jogoRepositorio.RemoverJogo(buscandoIdJogo);

                    Console.WriteLine($"O jogo {buscandoIdJogo.Nome} foi exluído!");
                }
                else
                {
                    Console.WriteLine("O jogo não foi excluído!");
                }
            }
            else
            {
                throw new Exception("Jogo não encontrado.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }

    static void GerenciamentoDeClientes()
    {
        try
        {
            Console.WriteLine("=== Gerenciamento de Clientes ===");
            Console.WriteLine("1. Cadastrar Novo Cliente");
            Console.WriteLine("2. Editar Cliente Existente");
            Console.WriteLine("3. Excluir Cliente");
            Console.WriteLine("4. Voltar ao Menu Principal");
            Console.WriteLine("Escolha uma opção:");
            opcao = int.Parse(Console.ReadLine());

            switch ((GerenciamentoDeClientesEnum)opcao)
            {
                case GerenciamentoDeClientesEnum.CadastrarNovoCliente:
                    CadastroDeClientes();
                    break;

                case GerenciamentoDeClientesEnum.EditarClienteExistente:
                    EditarClienteExistente();
                    break;

                case GerenciamentoDeClientesEnum.ExcluirCliente:
                    ExcluirCliente();
                    break;

                case GerenciamentoDeClientesEnum.MenuPrincipal:
                    MenuPrincipal();
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }
    static void CadastroDeClientes()
    {
        try
        {
            Cliente cliente = new Cliente();

            Console.WriteLine("=== Cadastro Novo Cliente ===");
            Console.WriteLine("Nome:");
            cliente.Nome = Console.ReadLine();

            Console.WriteLine("-------------------------------------------------");

            Console.WriteLine("Email:");
            cliente.Email = Console.ReadLine();

            Console.WriteLine("-------------------------------------------------");

            Console.WriteLine("Telefone:");
            cliente.Telefone = Console.ReadLine();

            Console.WriteLine("-------------------------------------------------");

            Console.WriteLine("Confirmação: Deseja salvar este cliente? (S/N)");
            string confirmacao = Console.ReadLine();

            Console.WriteLine("-------------------------------------------------");

            if (confirmacao == "S")
            {
                clienteRepositorio.AdicionarCliente(cliente);

                Console.WriteLine($"O cliente {cliente.Nome} foi salvo!");
            }
            else
            {
                Console.WriteLine("O cliente não foi salvo!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }

    static void EditarClienteExistente()
    {
        try
        {
            Console.WriteLine("=== Editar Cliente Existente ===");

            var clientes = contexto.Clientes.ToList();

            foreach (var item in clientes)
            {
                Console.WriteLine($"ID: {item.Id} | Nome: {item.Nome}");
            }

            Console.WriteLine("Digite o ID do Cliente para editar:");
            int clienteId = int.Parse(Console.ReadLine());

            var buscandoIdCliente = clienteRepositorio.ObterClientePorId(clienteId);

            if (buscandoIdCliente != null)
            {
                Console.WriteLine($"Cliente encontrado: {buscandoIdCliente.Nome}");

                Console.WriteLine("-------------------------------------------------");

                Console.WriteLine("Novo nome:");
                string novoNomeCliente = Console.ReadLine();

                if (!string.IsNullOrEmpty(novoNomeCliente))
                {
                    buscandoIdCliente.Nome = novoNomeCliente;
                }

                buscandoIdCliente.Nome = novoNomeCliente;

                Console.WriteLine("-------------------------------------------------");

                Console.WriteLine("Novo Email:");
                string novoEmailCliente = Console.ReadLine();


                if (!string.IsNullOrEmpty(novoEmailCliente))
                {
                    buscandoIdCliente.Email = novoEmailCliente;
                }

                buscandoIdCliente.Email = novoEmailCliente;

                Console.WriteLine("-------------------------------------------------");

                Console.WriteLine("Novo Telefone:");
                string novoTelefoneCliente = Console.ReadLine();

                if (!string.IsNullOrEmpty(novoTelefoneCliente))
                {
                    buscandoIdCliente.Telefone = novoTelefoneCliente;
                }

                buscandoIdCliente.Telefone = novoTelefoneCliente;

                Console.WriteLine("-------------------------------------------------");

                Console.WriteLine("Confirmação: Deseja salvar as alterações? (S/N)");
                string confirmacao = Console.ReadLine();

                Console.WriteLine("-------------------------------------------------");

                if (confirmacao == "S")
                {
                    clienteRepositorio.AtualizarCliente(buscandoIdCliente);

                    Console.WriteLine($"O cliente {buscandoIdCliente.Nome} foi alterado!");
                }
                else
                {
                    Console.WriteLine("O cliente não foi alterado!");
                }
            }
            else
            {
                throw new Exception("Cliente não encontrado.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }

    static void ExcluirCliente()
    {
        try
        {
            Console.WriteLine("=== Excluir Cliente ===");

            var clientes = contexto.Clientes.ToList();

            foreach (var item in clientes)
            {
                Console.WriteLine($"ID: {item.Id} | Nome: {item.Nome}");
            }

            Console.WriteLine("Digite o ID do Cliente para exluir:");
            int clienteId = int.Parse(Console.ReadLine());

            var buscandoIdCliente = clienteRepositorio.ObterClientePorId(clienteId);

            if (buscandoIdCliente != null)
            {
                Console.WriteLine($"Cliente encontrado: {buscandoIdCliente.Nome}");

                Console.WriteLine("-------------------------------------------------");

                Console.WriteLine("Confirmação: Tem certeza que deseja excluir? (S/N)");
                string confirmacao = Console.ReadLine();

                Console.WriteLine("-------------------------------------------------");

                if (confirmacao == "S")
                {
                    clienteRepositorio.RemoverCliente(buscandoIdCliente);

                    Console.WriteLine($"O cliente {buscandoIdCliente.Nome} foi exluído!");
                }
                else
                {
                    Console.WriteLine("O cliente não foi excluído!");
                }
            }
            else
            {
                throw new Exception("Cliente não encontrado.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }

    static void RegistroDeAlugueis()
    {
        try
        {
            Console.WriteLine("=== Registro de Alugueis ===");
            Console.WriteLine("1. Registrar Novo Aluguel");
            Console.WriteLine("2. Voltar ao Menu Principal");
            Console.WriteLine("Escolha uma opção:");
            opcao = int.Parse(Console.ReadLine());

            switch ((RegistroDeAlugueisEnum)opcao)
            {
                case RegistroDeAlugueisEnum.RegistrarNovoAluguel:
                    RegistrarNovoAluguel();
                    break;

                case RegistroDeAlugueisEnum.MenuPrincipal:
                    MenuPrincipal();
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }

    static void RegistrarNovoAluguel()
    {
        try
        {
            Aluguel aluguel = new Aluguel();

            Console.WriteLine("=== Registrar Novo Aluguel ===");

            var clientes = contexto.Clientes.ToList();

            foreach (var item in clientes)
            {
                Console.WriteLine($"ID: {item.Id} | Nome: {item.Nome}");
            }

            Console.WriteLine("ID do Cliente:");
            int clienteId = int.Parse(Console.ReadLine());

            Console.WriteLine("-------------------------------------------------");

            var jogos = contexto.Jogos.ToList();

            foreach (var item in jogos)
            {
                Console.WriteLine($"ID: {item.Id,-15} | Nome: {item.Nome,-15} | Disponibilidade: {item.Disponibilidade,-15}");
            }


            Console.WriteLine("ID do Jogo:");
            int jogoId = int.Parse(Console.ReadLine());

            var jogoEscolhido = jogoRepositorio.ObterJogo(jogoId);

            if (jogoEscolhido.Disponibilidade == "Indisponivel")
            {
                Console.WriteLine("Jogo escolhido está indiponível no momento.");
            }
            else
            {
                Console.WriteLine("-------------------------------------------------");

                Console.WriteLine($"Data de Aluguel: {aluguel.DataAluguel = DateTime.Now}");

                Console.WriteLine("-------------------------------------------------");

                Console.WriteLine($"Data da Devolução Prevista:");
                aluguel.DataDevolucao = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("-------------------------------------------------");

                Console.WriteLine("Confirmação: Deseja registrar este? (S/N)");
                string confirmacao = Console.ReadLine();

                Console.WriteLine("-------------------------------------------------");

                if (confirmacao == "S")
                {
                    aluguel.ClienteID = clienteId;

                    aluguel.JogoID = jogoId;

                    var jogoBuscado = jogoRepositorio.ObterJogo(jogoId);

                    if (jogoBuscado != null)
                    {
                        aluguelRepositorio.AdicionarAluguel(aluguel);
                        Console.WriteLine($"Aluguel {jogoBuscado.Nome} foi registrado com sucesso!");

                        jogoBuscado.Disponibilidade = "Indisponivel";

                        jogoRepositorio.AtualizarJogo(jogoBuscado);
                    }
                    else
                    {
                        throw new Exception("O jogo não foi encontrado!");
                    }

                }
                else
                {
                    Console.WriteLine("O aluguel não foi registrado!");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }

    static void Devolucoes()
    {
        try
        {
            Console.WriteLine("=== Devoluções ===");

            var devolucoes = contexto.Alugueis.ToList();

            foreach (var item in devolucoes)
            {
                Console.WriteLine($"ID: {item.Id}");
            }

            Console.WriteLine("Digite o ID do Aluguel:");
            int aluguelId = int.Parse(Console.ReadLine());

            var buscandoIdAluguel = aluguelRepositorio.ObterAluguel(aluguelId);

            if (buscandoIdAluguel == null)
            {
                throw new Exception("Aluguel não encontrado!");
            }

            Console.WriteLine("-------------------------------------------------");

            Console.WriteLine("Confirmação: Deseja registrar a devolução deste jogo? (S/N)");
            string confirmacao = Console.ReadLine();

            Console.WriteLine("-------------------------------------------------");

            if (confirmacao == "S")
            {
                Jogo jogo = jogoRepositorio.ObterJogo(buscandoIdAluguel.JogoID);

                Console.WriteLine($"O jogo {jogo.Nome} foi devolvido com sucesso!");

                jogo.Disponibilidade = "Disponivel";

                jogoRepositorio.AtualizarJogo(jogo);
            }
            else
            {
                Console.WriteLine("O aluguel não foi devolvido!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }

    static void Relatorios()
    {
        try
        {
            Console.WriteLine("=== Relatórios ===");
            Console.WriteLine("1. Mais Alugados");
            Console.WriteLine("2. Clientes que Mais Alugam");
            Console.WriteLine("3. Todos os Jogos Alugados");
            Console.WriteLine("4. Todos os Jogos Disponíveis");
            Console.WriteLine("5. Todos os Aluguéis Atrasados");
            Console.WriteLine("6. Voltar ao Menu Principal");
            Console.WriteLine("Escolha uma opção:");
            opcao = int.Parse(Console.ReadLine());

            switch ((RelatoriosEnum)opcao)
            {
                case RelatoriosEnum.MaisAlugados:
                    var listaMaisAlugados = relatorioRepositorio.MaisAlugados();

                    foreach (var item in listaMaisAlugados)
                    {
                        Console.WriteLine($"{item.nome,-15} | {item.quantidade}");
                    }
                    break;

                case RelatoriosEnum.ClientesQueMaisAlugam:
                    var listaClientesMaisAlugam = relatorioRepositorio.ClientesQueMaisAlugam();

                    foreach (var item in listaClientesMaisAlugam)
                    {
                        Console.WriteLine($"{item.nome,-15} | {item.quantidade}");
                    }
                    relatorioRepositorio.ClientesQueMaisAlugam();
                    break;

                case RelatoriosEnum.TodosJogosAlugados:
                    var listaTodosJogosAlugados = relatorioRepositorio.TodosJogosAlugados();

                    foreach (var item in listaTodosJogosAlugados)
                    {
                        Console.WriteLine($"{item.Nome}");
                    }
                    relatorioRepositorio.TodosJogosAlugados();
                    break;

                case RelatoriosEnum.TodosJogosDisponiveis:
                    var listaTodosJogosDisponiveis = relatorioRepositorio.TodosJogosDisponiveis();

                    foreach (var item in listaTodosJogosDisponiveis)
                    {
                        Console.WriteLine($"{item.Nome}");
                    }
                    relatorioRepositorio.TodosJogosDisponiveis();
                    break;

                case RelatoriosEnum.TodosAlugueisAtrasados:
                    var listaTodosAlugueisAtrasados = relatorioRepositorio.TodosAlugueisAtrasados(DateTime.Now);

                    foreach (var item in listaTodosAlugueisAtrasados)
                    {
                        Console.WriteLine($"{item.nomeCliente,-15} | {item.nomeJogo}");
                    }
                    relatorioRepositorio.TodosAlugueisAtrasados(DateTime.Now);
                    break;

                case RelatoriosEnum.MenuPrincipal:
                    MenuPrincipal();
                    break;

                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocorreu um erro inesperado: " + ex.Message);
        }
    }
}