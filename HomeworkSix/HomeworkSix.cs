using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Security.Policy;

namespace HomeworkSix
{
    [TestFixture]
    public class HomeworkSix : Setup
    {
        
        [Test]
        public void Transformer()
        {
            //1.Log in as a transporter.
            // Open form
            IWebElement showLoginForm = _webDriver.FindElement(By.Id("login"));
            showLoginForm.Click();

            // Wait until the button is visible
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("button[class='btn btn-green']")));

            // Wait to open the form
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("username")));

            // Target username and enter data
            IWebElement usernameField = _webDriver.FindElement(By.Id("username"));
            usernameField.Clear();
            usernameField.SendKeys("maryan.shapkaroski3@gmail.com");

            // Target password and enter data
            IWebElement passwordField = _webDriver.FindElement(By.Id("password"));
            passwordField.Clear();
            passwordField.SendKeys("123456");

            // Click on Sign in button
            _webDriver.FindElement(By.CssSelector("button[class='btn btn-green']")).Click();

            // Wait to load the dashboard
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.TagName("h2")));


            //2.Confirm you are on the Active Requests page.
            var activeRequest = _webDriver.FindElements(By.TagName("h2"))[0];
            string expected = "Active requests";
            string expectedMK = "Активни барања";
 
            if (activeRequest.Text.Contains(expected))
            {
                Assert.AreEqual(expected, activeRequest.Text);
            }
            else if (activeRequest.Text.Contains(expectedMK))
            {
                Assert.AreEqual(expectedMK, activeRequest.Text);
            }
            else
            {
                Assert.Fail();
            }

            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("table-body")));

            //3.Find the second request from the page and make an offer.

            // Second request and open it
            _webDriver.FindElement(By.XPath("/html/body/div[3]/div[1]/div[2]/request-search-list/div[2]/request-list/div[2]/div[2]/table/tbody/tr[2]/td[1]/a")).Click();

            // Wait 
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.CssSelector("button[class='details-panel__make-offer-btn']")));

            // Click on create offer button
            _webDriver.FindElement(By.CssSelector("button[class='details-panel__make-offer-btn")).Click();

            // Wait for Make offer form
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("form[name='makeOfferForm']")));

            // Fill form
            IWebElement offerPrice = _webDriver.FindElement(By.CssSelector("input[ng-model='paymentType.price']"));
            offerPrice.SendKeys("10");

            IWebElement offerPickUpTime = _webDriver.FindElement(By.CssSelector("input[ng-model='vm.pickUpTime']"));
            offerPickUpTime.SendKeys("09.11.2022 01:10");

            IWebElement offerDeliveryTime = _webDriver.FindElement(By.CssSelector("input[ng-model='vm.deliveryTime']"));
            offerDeliveryTime.SendKeys("16.11.2022 01:00");

            IWebElement offerIsValidUntil= _webDriver.FindElement(By.CssSelector("input[ng-model='vm.expirationDate']"));
            offerIsValidUntil.SendKeys("16.11.2022 01:00");

            IWebElement offerInsurance = _webDriver.FindElement(By.CssSelector("select[ng-model='vm.insuranceAmount']"));
            SelectElement selectEl = new SelectElement(offerInsurance);
            selectEl.SelectByValue("1000000");


            _webDriver.FindElement(By.CssSelector("textarea[ng-model='vm.offerComment']")).SendKeys("Thank you for the collaboration!");

            _webDriver.FindElement(By.CssSelector("button[class='make-offer__btn-create']")).Click();

            // Wait for Submit offer form appear
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("button[class='modal-footer__btn-save']")));

            // Click on CREATE OFFER
            _webDriver.FindElement(By.CssSelector("button[class='modal-footer__btn-save']")).Click();


            //4.Confirm that your offer is displayed in the My Offers page.

            // Wait to load the dashboard
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("button[class='details-panel__edit-offer-btn']")));


            IWebElement offerStrengthLike = _webDriver.FindElement(By.CssSelector("div[class='offer-strength offer-strength-thumb-up material-icons']"));

            Assert.IsNotNull(offerStrengthLike);

            //5.Log out and confirm that you are logged out.

            _webDriver.FindElement(By.Id("logout2")).Click();


            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlMatches(webUrl));

            Assert.Multiple(() =>
            {
                Assert.AreEqual(webUrl, _webDriver.Url, "Logged out successfully");
            });
            

        }

        public string RandomGenerateLetters(int lenght)
        {

            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrsqtuvwxyz";
            return new string(Enumerable.Repeat(letters, lenght)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string RandomNumberGenerator(int length)
        {
            int[] num = new int[length];
            for(var i = 0; i < length; i++)
            {

                num.Append(random.Next(1,10)); 
            }

            return num.ToString();
        }

    }
}
