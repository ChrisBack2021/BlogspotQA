using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogspot.Pages
{
    public class TestForm
    {
        private IWebDriver driver;
        public string emailString = "test4321@testing.com";
        public string phoneString = "0404040404";
        private List<string> bookHeaders = new List<string>() { "BookName", "Author", "Subject", "Price" };

        public TestForm(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement Name => driver.FindElement(By.CssSelector("#name"));

        public IWebElement Email => driver.FindElement(By.Id("email"));

        public IWebElement Phone => driver.FindElement(By.XPath("//input[@id='phone']"));

        public IWebElement Address => driver.FindElement(By.CssSelector("#textarea"));

        public IWebElement Gender => driver.FindElement(By.XPath("//input[@value='male']"));

        public IList<IWebElement> Days => driver.FindElements(By.XPath("//div[@class='form-check form-check-inline']//input[@class='form-check-input']"));

        public IWebElement Countries => driver.FindElement(By.Id("country"));

        public IWebElement Colors => driver.FindElement(By.Id("colors"));

        public IList<IWebElement> ColorList => driver.FindElements(By.XPath("//select[@id='colors']/option"));

        public IWebElement Calender => driver.FindElement(By.Id("datepicker"));

        public IWebElement CalenderNextBtn => driver.FindElement(By.CssSelector("a.ui-datepicker-next.ui-corner-all[Title='Next']"));

        public IWebElement Month => driver.FindElement(By.CssSelector("span.ui-datepicker-month"));

        public IWebElement Year => driver.FindElement(By.CssSelector("span.ui-datepicker-year"));

        public IList<IWebElement> CalenderDays => driver.FindElements(By.CssSelector("a.ui-state-default"));

        public IList<IWebElement> BookTableHeader => driver.FindElements(By.XPath("//table[@name='BookTable']/tbody/tr/th"));

        public IList<IWebElement> Books => driver.FindElements(By.XPath("//table[@name='BookTable']/tbody//tr//td"));

        public IList<IWebElement> ThirdRow => driver.FindElements(By.XPath("//table[@name='BookTable']/tbody//tr[3]/td"));

        public IWebElement FirstProduct => driver.FindElement(By.CssSelector("table#productTable > tbody > tr > td"));

        public IWebElement SecondPageBtn => driver.FindElement(By.LinkText("2"));

        public IList<IWebElement> Products => driver.FindElements(By.CssSelector("table#productTable > tbody > tr"));

        public IWebElement WikiSearch => driver.FindElement(By.CssSelector("input.wikipedia-search-input"));

        public IWebElement WikiSearchBtn => driver.FindElement(By.XPath("//input[@class='wikipedia-search-button']"));

        public IList<IWebElement> SearchList => driver.FindElements(By.CssSelector("div#wikipedia-search-result-link > a"));

        public IList<IWebElement> Alert => driver.FindElements(By.XPath("//div[@id='HTML9']/div[@class='widget-content']/button"));

        public IWebElement ConfirmParagraph => driver.FindElement(By.Id("demo"));

        public IWebElement DblClickBtn => driver.FindElement(By.CssSelector("div#HTML10 > div.widget-content > button"));
        public IWebElement FieldOne => driver.FindElement(By.Id("field1"));

        public IWebElement FieldTwo => driver.FindElement(By.Id("field2"));

        public IWebElement Drag => driver.FindElement(By.Id("draggable"));

        public IWebElement Drop => driver.FindElement(By.Id("droppable"));

        public IWebElement Slider => driver.FindElement(By.CssSelector("span.ui-slider-handle.ui-corner-all"));

        public IWebElement IFrame => driver.FindElement(By.TagName("iframe"));

        public IWebElement FrameName => driver.FindElement(By.Id("RESULT_TextField-0"));

        public IWebElement FrameGender => driver.FindElement(By.XPath("//label[@for='RESULT_RadioButton-1_0']"));

        public IWebElement FrameDOB => driver.FindElement(By.Id("RESULT_TextField-2"));

        public IWebElement FrameJob => driver.FindElement(By.Id("RESULT_RadioButton-3"));
        public IWebElement FrameSubmit => driver.FindElement(By.Id("FSsubmit"));

        public IWebElement FrameError => driver.FindElement(By.XPath("//div[@class='full_width_space']"));

        public string Message()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(e => e.FindElement(By.XPath("//div[@class='full_width_space']")).Text.Contains("result storage capacity has been reached"));

            return FrameError.Text;

        }
        public void SubmitFrame()
        {
            FrameSubmit.Click();
        }

        public void EnterFrameJob()
        {
            SelectElement select = new SelectElement(FrameJob);
            select.SelectByText("Automation Engineer");
        }

        public void EnterFrameDOB()
        {
            FrameDOB.SendKeys("19/01/1996");
        }

        public void EnterFrameGender()
        {
            FrameGender.Click();
        }


        public void EnterFrameName()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].scrollIntoView();", FrameName);
            FrameName.SendKeys("Chrees");
        }



        public void DragSlider()
        {

            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].scrollIntoView();", Slider);

            Point start = Slider.Location;
            Point finish = new Point(1490, 1270);

            new Actions(driver).DragAndDropToOffset(Slider, finish.X - start.X, finish.Y - start.Y).Perform(); 
        }

        public void DragAndDrop()
        {
            new Actions(driver).DragAndDrop(Drag, Drop).Perform();
        }


        public void DblClick()
        {
            new Actions(driver).DoubleClick(DblClickBtn).Perform();
        }


        public string AlertParagraph(string text)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(e => e.FindElement(By.Id("demo")).Text == text);

            return ConfirmParagraph.Text;
        }

        

        public void Alerts(string text)
        {
            foreach (var alerts in Alert)
            {
                if (alerts.Text.Contains(text))
                {
                    alerts.Click();
                    break;
                }
            }
        }

        public void Search()
        {
            WikiSearch.SendKeys("Selenium");
            WikiSearchBtn.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(e => e.FindElement(By.CssSelector("div#wikipedia-search-result-link > a")).Displayed);

            foreach (var link in SearchList)
            {
                if (link.Text.Contains("software"))
                    link.Click();
            }

        }

        public bool ProductCheck()
        {
            foreach (var product in Products)
            {
                if (product.Text.Contains("$9.99"))
                {
                    return true;
                }
            }
            return false;
        }

        public string CheckPaginationTable()
        {
            return FirstProduct.Text;
        }

        public bool CheckRow()
        {
            for (int i = 0; i < ThirdRow.Count; i ++)
            {
                if (ThirdRow[2].Text == "Java")
                    return true;
            }
            return false;
        }

        public string BookName()
        {
            foreach (var book in Books)
            {
                if (book.Text == "Master In Selenium")
                    return book.Text;
            }
            return null;
        }

        public bool BookHeaders()
        {
            bool flag = false;
            for (int i = 0; i < BookTableHeader.Count; i++)
            {
                if (BookTableHeader[i].Text != bookHeaders[i])
                    return flag;
            }
            return flag = true;

        }

        public string EnterCalender()
        {
            Calender.Click();
            while (Month.Text != "March" || Year.Text != "2024")
            {
                CalenderNextBtn.Click();
            }

            foreach (var day in CalenderDays)
            {
                if (day.Text == "7")
                    day.Click();
            }
            return Calender.GetAttribute("value");
        }


        public void SelectColors()
        {
            SelectElement select = new SelectElement(Colors);
            select.SelectByValue("red");
            select.SelectByText("Green");
            select.SelectByIndex(4);
        }

        public void SelectCountry()
        {
            SelectElement select = new SelectElement(Countries);
            select.SelectByValue("australia");
            
        }
        

        public bool EnterDays()
        {
        
            foreach (var day in Days)
            {
                if (day.GetAttribute("value") == "wednesday")
                {
                    day.Click();
                    return day.Selected;
                }
            }
            return false;
        }

        public string GenderBtn()
        {
            Gender.Click();
            return Gender.GetAttribute("value");
        }

        public void EnterAddress()
        {
            Address.SendKeys("50 Homer Street");
        }
        public bool EnterNumber()
        {
            Phone.SendKeys(phoneString);
            if (Phone.GetAttribute("value") == phoneString)
                return true;
            return false;
        }

        public string EnterEmail()
        {
            Email.SendKeys(emailString);
            string email = Email.GetAttribute("value");
            return email;
        }

        public void EnterName()
        {
            Name.SendKeys("Jerry");
        }

    }
}
