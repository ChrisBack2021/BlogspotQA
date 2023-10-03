using Blogspot.Pages;
using OpenQA.Selenium;

namespace Blogspot
{
    [TestClass]
    public class Tests : BaseTest
    {
        [TestMethod]
        public void InputTests()
        {
            TestForm form = new TestForm(driver);
            form.EnterName();
            var emailCheck = form.EnterEmail();
            Assert.AreEqual(emailCheck, form.emailString, "Email should be test4321@testing.com");

            var phoneCheck = form.EnterNumber();
            Assert.IsTrue(phoneCheck, "Phone number value should be same to property phoneString");

            form.EnterAddress();
            Assert.IsNotNull(form.Address.GetAttribute("value"), "Address value is currently null");

            var male = form.GenderBtn();
            Assert.AreEqual(male, "male", "Value selected should be male");

            var radioCheck = form.EnterDays();
            Assert.IsTrue(radioCheck, "Radio button should have been checked");

            form.SelectColors();

            var calender = form.EnterCalender();
            Assert.AreEqual(calender, "03/07/2024", "Date should be 03/07/2024");
        }

        [TestMethod]
        public void TableTests()
        {
            TestForm table = new TestForm(driver);
            var checkBookHeaders = table.BookHeaders();
            Assert.IsTrue(checkBookHeaders, "Book headers should match array as it is static");

            var checkBook = table.BookName();
            Assert.IsNotNull(checkBook, "Book name should be found and not null");
            var java = table.CheckRow();
            Assert.IsTrue(java, "Third row under subject is Java");

            string firstProduct = table.CheckPaginationTable();
            table.SecondPageBtn.Click();
            string secondPageFirstProduct = table.CheckPaginationTable();
            Assert.AreNotEqual(firstProduct, secondPageFirstProduct, "Same method should provide different results due to pagination");

            bool checkProduct = table.ProductCheck();
            Assert.IsTrue(checkProduct, "Product has not been found correctly");
        }

        [TestMethod]
        public void ActionTests()
        {
            TestForm action = new TestForm(driver);
            action.Search();

            string currentHandle = driver.CurrentWindowHandle;
            var currentUrl = driver.Url;
            var allHandles = driver.WindowHandles;

            foreach (var handle in allHandles)
            {
                if (handle != currentHandle)
                {
                    driver.SwitchTo().Window(handle);
                    break;
                }
            }

            var newUrl = driver.Url;
            Assert.AreNotEqual(currentUrl, newUrl, "The url should not be same as we switched window handle");
            driver.Close();
            driver.SwitchTo().Window(currentHandle);

            action.Alerts("Alert");
            IAlert alert = driver.SwitchTo().Alert();
            string firstAlertText = alert.Text;
            Assert.AreEqual(firstAlertText, "I am an alert box!", "Alert has not been found correctly");
            alert.Accept();

            action.Alerts("Confirm Box");
            string secondAlertText = alert.Text;
            Assert.AreEqual(secondAlertText, "Press a button!", "Incorrect alert has been found");
            alert.Dismiss();

            string checkDismiss = action.AlertParagraph("You pressed Cancel!");
            Assert.AreEqual(checkDismiss, "You pressed Cancel!", "Dismiss button is not working");

            string name = "Chris";
            action.Alerts("Prompt");
            alert.SendKeys(name);
            alert.Accept();

            Assert.AreNotEqual(action.FieldOne.GetAttribute("value"), action.FieldTwo.GetAttribute("value"), "Text input area should not be the same");
            action.DblClick();
            Assert.AreEqual(action.FieldOne.GetAttribute("value"), action.FieldTwo.GetAttribute("value"), "Text input area should now be the same");

            action.DragAndDrop();
            action.DragSlider();

            driver.SwitchTo().Frame(action.IFrame);
            action.EnterFrameName();
            action.EnterFrameGender();
            action.EnterFrameDOB();
            action.EnterFrameJob();
            action.SubmitFrame();
            var errorMsg = action.Message();
            Assert.IsTrue(errorMsg.Contains("result storage capacity"), "Incorrect error message has been found");

            driver.SwitchTo().DefaultContent();

            Assert.AreEqual(driver.Url, currentUrl, "driver has not switched back to parent frame");
        
        }
    }
}
