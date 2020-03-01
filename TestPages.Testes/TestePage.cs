using System.IO;
using System.Globalization;
using Xunit;
using Microsoft.Extensions.Configuration;
using TestPage.Testes.PageObjects;
using TestPage.Testes.Utils;

namespace TestPage.Testes
{
    public class TestePage
    {
        private IConfiguration _configuration;

        public TestePage()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");
            _configuration = builder.Build();

            var padroesBR = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = padroesBR;
            CultureInfo.DefaultThreadCurrentUICulture = padroesBR;
        }

        [Theory]        
        [InlineData(Browser.Chrome, "ultimate4system", "ultimate123", "Executar Tarefa", "Teste 1234")]
    
        public void TestarPagina(
            Browser browser, string login, string password, string title, string about)
        {
            TelaTestPage tela =
                new TelaTestPage(_configuration, browser);

            tela.CarregarPagina();
            tela.ProcessarConversao();      
            tela.ObterLogin(login);
            tela.Obterpass(password);
            tela.SignIn();
            tela.wait();
            tela.addTask();
            tela.addATask();            
            tela.AddTitle(title);
            tela.tellAbout(about);
            tela.done();
            //tela.addDateLimit(); //Componente datepick está com algum erro, o componente pisca e não abre causando uma execeção no momento de executar o codigo
            tela.SaveTask();

            //tela.Fechar();  //Desconmentar esse trecho, caso queira fechar o navegador no final da execução.
        }
    }
}