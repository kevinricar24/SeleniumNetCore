using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumNetCore
{
    public class Program
    {
        public IWebDriver driver;

        [SetUp]
        public void TestSetup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://google.com");
        }


        [Test]
        public void Test001_ValidateTittle()
        {
            // Arrange
            string titleExpected = "Google", titleActual = string.Empty;

            // Act
            titleExpected = titleExpected.ToLower();
            titleActual = driver.Title.ToLower();

           // Assert
            Assert.AreEqual(titleExpected, titleActual);
        }

        [TearDown]
        public void TestTearDown()
        {
            driver.Quit();
        }
    }
}
