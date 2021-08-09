using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumWebTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [TestCategory("Purchase")]
        
        public void TestCase_01()
        {
            IWebDriver driver = new ChromeDriver();                                                      //Iniciate crome driver                          
            driver.Url="http://automationpractice.com/";                                                 // Access to page
            driver.FindElement(By.ClassName("sf-with-ul")).Click();                                      // Look for the lits of dresses
            driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(5);                    // Delay for 5 seconds         
            string[] items = new string[] { "color_13", "color_16", "color_43", "color_2", "color_21", "color_20", "color_19", "color_40", "color_31", "color_37" };  //List of Items to buy

            for (int i = 0; i < items.Length; i++)                                                      // Check the list to buy and add to the cart
            {
                string findDress = items[i];                                                             // Determine the dress to buy
                driver.FindElement(By.Id(findDress)).Click();                                            // Look for the dress to buy
                driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(5);                // Delay for 5 seconds
                driver.FindElement(By.Name("Submit")).Click();                                           // Look for the add button
                driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(5);                // Delay for 5 seconds
                driver.FindElement(By.CssSelector(".continue.btn.btn-default.button.exclusive-medium")).Click();  //Look for the continue button 
                driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(5);                // Delay 5 seconds
                driver.FindElement(By.ClassName("sf-with-ul")).Click();                                  // Look for the desses list                      
            }

            driver.FindElement(By.ClassName("ajax_cart_quantity")).Click();                              // Look for the Shopping cart
            int rowcount = driver.FindElements(By.XPath("//*[@id='cart_summary']/tbody/tr")).Count;      // Look for the table of items and count the rows

            Assert.AreEqual(rowcount, 10, "Error 1: The number of items in the cart is different to 10"); // Validate the list of items      
                        
            do
            {
                driver.FindElement(By.ClassName("cart_quantity_delete")).Click();                          // Look for the icon delete
                driver.Navigate().Refresh();                                                               // Refresh the page
                driver.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(5));                   // Delay after refresh
                rowcount = driver.FindElements(By.XPath("//*[@id='cart_summary']/tbody/tr")).Count;        // Count the rows of items
            } while (rowcount != 0);

            Assert.AreEqual(rowcount, 0, "Error 2: The number of items in the cart is different to 0");    // Validate the list of Items
            driver.Quit();                                                                                 // Close the browser and close the driver.          
        }
    }
}
