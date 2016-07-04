using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RelayAppInsuranceCalculator
{
    public partial class InsuranceCalcForm : System.Web.UI.Page
    {
        public PolicyModel InsurancePolicy
        {
            get
            {
                var policy = ViewState["Policy"] as PolicyModel;
                if (policy == null)
                {
                    ViewState["Policy"] = new PolicyModel();
                }
                return ViewState["Policy"] as PolicyModel;
            }
            set
            {
                ViewState["Policy"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (InsurancePolicy.StartDate == DateTime.MinValue)
            {
                Calendar1.SelectedDate = DateTime.Now.Date;
                InsurancePolicy.StartDate = Calendar1.SelectedDate;
            }
            else
            {
                Calendar1.SelectedDate = InsurancePolicy.StartDate;
            }

            CalendarLabel.Text = Calendar1.SelectedDate.ToLongDateString();
            UpdateSummary();

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            CalendarLabel.Text = Calendar1.SelectedDate.ToLongDateString();
            InsurancePolicy.StartDate = Calendar1.SelectedDate;
            UpdateSummary();
        }

        protected void AddDriver_Click(object sender, EventArgs e)
        {
            DriverModel driver = new DriverModel();
            driver.Name = NameTextBox.Text;
            driver.Occupation = OccupationDropDown.Text;
            DateTime dob;
            if (DateTime.TryParse(DOBTextBox.Text, out dob))
            {
                driver.DateOfBirth = dob;
            }

            DateTime claim1;
            if (DateTime.TryParse(ClaimTextBox1.Text, out claim1))
            {
                driver.Claims.Add(new ClaimModel { DateOfClaim = claim1 });
            }
            DateTime claim2;
            if (DateTime.TryParse(ClaimTextBox2.Text, out claim2))
            {
                driver.Claims.Add(new ClaimModel { DateOfClaim = claim2 });
            }
            DateTime claim3;
            if (DateTime.TryParse(ClaimTextBox3.Text, out claim3))
            {
                driver.Claims.Add(new ClaimModel { DateOfClaim = claim3 });
            }
            DateTime claim4;
            if (DateTime.TryParse(ClaimTextBox4.Text, out claim4))
            {
                driver.Claims.Add(new ClaimModel { DateOfClaim = claim4 });
            }
            DateTime claim5;
            if (DateTime.TryParse(ClaimTextBox5.Text, out claim5))
            {
                driver.Claims.Add(new ClaimModel { DateOfClaim = claim5 });
            }

            InsurancePolicy.Drivers.Add(driver);
            UpdateSummary();

            NameTextBox.Text = string.Empty;
            DOBTextBox.Text = string.Empty;
            ClaimTextBox1.Text = string.Empty;
            ClaimTextBox2.Text = string.Empty;
            ClaimTextBox3.Text = string.Empty;
            ClaimTextBox4.Text = string.Empty;
            ClaimTextBox5.Text = string.Empty;
        }
        /// <summary>
        /// Handler for when Calculate Premium button is clicked
        /// If policy breaks the rules, Prints the declination message
        /// else Prints the premium amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CalculatePremiumBtn_Click(object sender, EventArgs e)
        {
            IPremiumCalculator calculator = new PremiumCalculator();
            PremiumModel premium = calculator.CalculatePremium(InsurancePolicy);

            if (premium.IsDeclined)
            {
                PremumResultLabel.Text = string.Format("Policy Declined: {0}", premium.Message);
            }
            else
            {
                PremumResultLabel.Text = string.Format("Premium: {0:C}", premium.Amount);
            }
            UpdateSummary();
        }
        /// <summary>
        /// Updates Policy Summary Box
        /// Prints Drivers and their claims on the policy
        /// </summary>
        private void UpdateSummary()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("<b>StartDate: </b>{0}<br/><br/>", InsurancePolicy.StartDate.ToLongDateString()));
            builder.Append("<u><b>Drivers</b></u><br/>");

            foreach (DriverModel driver in InsurancePolicy.Drivers)
            {
                builder.Append(string.Format("<b>Name:           </b>{0}</br>", driver.Name));
                builder.Append(string.Format("<b>Date Of Birth : </b>{0}</br>", driver.DateOfBirth.ToShortDateString()));
                builder.Append(string.Format("<b>Occupation:     </b>{0}</br>", driver.Occupation));
                if (driver.Claims != null && driver.Claims.Count > 0)
                {
                    builder.Append("<b>Claim Dates: </b> ");
                    foreach (ClaimModel claim in driver.Claims)
                    {
                        builder.Append(string.Format("[{0}] ", claim.DateOfClaim.ToShortDateString()));
                    }
                    builder.Append("<br/>");
                }
                builder.Append("<br/>");
            }

            PolicySummaryLabel.Text = builder.ToString();
        }
        /// <summary>
        /// Clears the policy data added so far by the user 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClearPolicyBtn_Click(object sender, EventArgs e)
        {
            InsurancePolicy = null;
            InsurancePolicy.StartDate = Calendar1.SelectedDate;
            UpdateSummary();
            PremumResultLabel.Text = string.Empty;
        }
    }
}