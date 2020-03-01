using System;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestPage.Testes.Utils;

namespace TestPage.Testes.PageObjects
{
    public class TelaTestPage
    {
        private IConfiguration _configuration;
        private Browser _browser;
        private IWebDriver _driver;

        public TelaTestPage(
            IConfiguration configuration, Browser browser)
        {
            _configuration = configuration;
            _browser = browser;

            string caminhoDriver = null;
            if (browser == Browser.Firefox)
            {
                caminhoDriver =
                    _configuration.GetSection("Selenium:CaminhoDriverFirefox").Value;
            }
            else if (browser == Browser.Chrome)
            {
                caminhoDriver =
                    _configuration.GetSection("Selenium:CaminhoDriverChrome").Value;
            }

            _driver = WebDriverFactory.CreateWebDriver(
                browser, caminhoDriver, true);
        }
        public void CarregarPagina()
        {
            _driver.LoadPage(
                TimeSpan.FromSeconds(Convert.ToInt32(
                    _configuration.GetSection("Selenium:Timeout").Value)),
                _configuration.GetSection("Selenium:UrlTela").Value);
        }

        public void PreencherDistanciaMilhas(string name)
        {
            _driver.SetText(
                By.Name("name"),
                name.ToString());
        }

        public void ProcessarConversao()
        {
            //_driver.Submit(By.XPath("//a[@class='waves-effect waves-light red darken-2 btn modal-trigger']"));

            _driver.FindElement(By.ClassName("modal-trigger")).Click();
            
            WebDriverWait wait = new WebDriverWait(
                _driver, TimeSpan.FromSeconds(Convert.ToInt32(
                    _configuration.GetSection("Selenium:Timeout").Value)));
            wait.Until((d) => d.FindElement(By.Name("login")) != null);
        }

        public void SignIn()
        {
            _driver.FindElement(By.XPath("//html/body/div[4]/div[2]/a")).Click();

            
            WebDriverWait wait = new WebDriverWait(
               _driver, TimeSpan.FromSeconds(Convert.ToInt32(
                   _configuration.GetSection("Selenium:Timeout").Value)));
        }

        public void wait()
        {                                  
            _driver.Navigate().GoToUrl("http://www.juliodelima.com.br/taskit/task");

            WebDriverWait wait = new WebDriverWait(
             _driver, TimeSpan.FromSeconds(Convert.ToInt32(
                 _configuration.GetSection("Selenium:Timeout").Value)));
        }

        public void addTask()
        {
             _driver.FindElement(By.XPath("//html/body/div[2]/div/div/p[2]/a")).Click();
        }

        public void addATask()
        {
            _driver.FindElement(By.XPath("//html/body/div[1]/div/div/div[2]/button")).Click();

            WebDriverWait wait = new WebDriverWait(
            _driver, TimeSpan.FromSeconds(Convert.ToInt32(
                _configuration.GetSection("Selenium:Timeout").Value)));
        }

        public void AddTitle(string title)
        {
            _driver.SetText(
               By.XPath("/html/body/div[2]/div[1]/div[2]/div/input"),
                title.ToString());
        }

        public void addDateLimit()
        {
            _driver.FindElement(By.XPath("/html/body/div[2]/div[1]/div[3]/div[1]/input")).Click();
            _driver.FindElement(By.XPath("//*[@id='P715415428_root'/div/div/div/div/div[2]/div[2]/button[1]")).Click();
            _driver.FindElement(By.XPath("//*[@id='P1123373691_root']/div/div/div/div/div[2]/div[2]/button[3]")).Click();            
        }

        public void pickDate()
        {
            _driver.FindElement(By.XPath("//*[@id='P1873688422_root']/div/div/div/div/div[2]/div[2]/button[1]")).Click();                       
        }

        public void tellAbout(string about)
        {
            _driver.SetText(
                By.XPath("/html/body/div[2]/div[1]/div[4]/div/textarea"),
                 about.ToString());
        }

        public void done()
        {
            _driver.FindElement(By.XPath("//*[@id='addtask']/div[1]/div[5]/select")).Click();
            _driver.FindElement(By.XPath("//*[@id='addtask']/div[1]/div[5]/select/option[2]")).Click();

            
        }

        public void ObterLogin(string login)
        {

            WebDriverWait wait = new WebDriverWait(
                _driver, TimeSpan.FromSeconds(Convert.ToInt32(
                    _configuration.GetSection("Selenium:Timeout").Value)));

            //_driver.FindElement(By.XPath("//input[contains(@placeholder, 'Please, tell us your login')]")).Click();

            _driver.SetText(
              By.XPath("//input[contains(@placeholder, 'Please, tell us your login')]"),
            login.ToString());


        }

        public void Obterpass(string password)
        {
            _driver.SetText(
               By.XPath("//input[contains(@placeholder, 'Excuse me, can you tell us a secret?')]"),
                password.ToString());
        }

        public void SaveTask()
        {
            _driver.FindElement(By.XPath("//*[@id='addtask']/div[2]/a")).Click();            
        }

        public void Fechar()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}