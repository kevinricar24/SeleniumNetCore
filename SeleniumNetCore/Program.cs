using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumNetCore
{
    public class Program
    {
        public IWebDriver driver;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [SetUp]
        public void SetUp()
        {
            Console.WriteLine(@"Running {0}", GetTestName());
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
            Assert.AreEqual(titleExpected, titleActual, msgerror(titleExpected, titleActual));
        }

        [Test]
        public void Test002_ValidateSearch()
        {
            // Arrange
            string titleExpected = "selenium - buscar con google", titleActual = string.Empty;

            // Act
            var inputGoogleSearch = driver.FindElement(By.Name("q"));
            inputGoogleSearch.Clear();
            inputGoogleSearch.SendKeys("selenium");
            inputGoogleSearch.SendKeys(Keys.Enter);

            //var buttonGoogleSearch = driver.FindElement(By.Name("btnK"));
            //buttonGoogleSearch.Click();

            titleExpected = titleExpected.ToLower();
            titleActual = driver.Title.ToLower();

            // Assert
            Assert.AreEqual(titleExpected, titleActual, msgerror(titleExpected, titleActual));
        }

        [TearDown]
        public void TearDown()
        {
            string ResultTest = string.Empty;
            ResultTest = TestContext.CurrentContext.Result.Outcome.Status.ToString();
            if (driver == null) return;

            try
            {
                //Here the code to capture javascipt errors
            }
            finally
            {
                if (ResultTest.Equals("Failed"))
                {
                    CaptureImage(ResultTest, string.Empty);
                }
            }
            Console.WriteLine(@"" + ResultTest + " {0}", GetTestName());
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (driver != null)
            {
                driver.Manage().Cookies.DeleteAllCookies();
                driver.Quit();
            }
        }

        private static string GetTestName()
        {
            return TestContext.CurrentContext.Test.FullName;
        }

        public string msgerror(string var1, string var2)
        {
            return "Error: the values " + var1 + " = " + var2 + " Should be equals";
        }

        private void CaptureImage(string folder, string captureName)
        {
            try
            {
                var captureDriver = driver as ITakesScreenshot;
                if (captureDriver != null)
                {
                    var screenshot = captureDriver.GetScreenshot();
                    var path = Path.Combine(TestContext.CurrentContext.WorkDirectory, folder);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string captureSuffix = string.Empty;
                    if (!string.IsNullOrEmpty(captureName))
                    {
                        captureSuffix = "_" + captureName;
                    }

                    var filename = string.Format("{0}{1}.png", TestContext.CurrentContext.Test.FullName, captureSuffix);
                    screenshot.SaveAsFile(Path.Combine(path, filename), ScreenshotImageFormat.Png);
                }
            }
            catch
            {
            }
        }



    }
}
