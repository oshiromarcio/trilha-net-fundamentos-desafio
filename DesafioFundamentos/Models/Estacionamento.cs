using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            string placa = this.ObtemPlaca();
            if (!ValidaPlaca(placa))
            {
                return;
            }
            veiculos.Add(placa);
            return;
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");
            string placa = this.ObtemPlaca();

            // Verifica se o veículo existe
            if (veiculos.Any(x => x == placa))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                int horas = 0;
                int.TryParse(Console.ReadLine(), out horas);

                if (horas == 0)
                {
                    Console.WriteLine("Número de horas inválido, o mínimo de horas é 1. Operação cancelada.");
                    return;
                }
                decimal valorTotal = precoInicial + precoPorHora * horas;

                veiculos.Remove(placa);

                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente.");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                int contador = 0;
                Console.WriteLine("Os veículos estacionados são:");
                foreach(string veiculo in veiculos) {
                    contador++;
                    Console.WriteLine($"{contador} - {veiculo}");
                }

                Console.WriteLine("________________________");
                Console.WriteLine($"Total de veículos: {veiculos.Count}");
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        private string ObtemPlaca()
        {
            string novaPlaca = Console.ReadLine() ?? "";

            // Trata a placa digitada
            novaPlaca = novaPlaca.ToUpper();
            // Remove qualquer caracter que seja usado como separador
            novaPlaca = Regex.Replace(novaPlaca, @"[^a-zA-Z0-9]", "");

            return novaPlaca;
        }

        private bool ValidaPlaca(string novaPlaca)
        {
            // Valida se a placa foi digitada
            if (novaPlaca.Trim() == "")
            {
                Console.WriteLine("Placa não digitada, operação cancelada.");
                return false;
            }

            // Verifica se a placa é do padrão antigo ou Mercosul
            List<string> modelos = new List<string>();
            modelos.Add(@"^[a-zA-Z]{3}\d{4}$");
            modelos.Add(@"^[a-zA-Z]{3}\d{1}[a-zA-Z]{1}\d{2}$");

            // Verifica se a placa faz parte de algum padrão
            bool modeloOk = false;
            foreach(string modelo in modelos)
            {
                if (Regex.IsMatch(novaPlaca, modelo))
                {
                    modeloOk = true;
                    break;
                }
            }
            if (!modeloOk)
            {
                Console.WriteLine("Placa inválida, padrão não reconhecido.");
                return false;
            }

            // Valida se a placa já consta na lista de veículos estacionados
            if (veiculos.Any(x => x == novaPlaca))
            {
                Console.WriteLine("Veículo já consta na lista de veículos estacionados, operação cancelada.");
                return false;
            }

            // Placa válida
            return true;
        }
    }
}
