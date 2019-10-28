using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        //Two Lists - one for the drop down of members, one for the already selected members in the combo box.
        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();

        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();

        public CreateTeamForm()
        {
            InitializeComponent();

            //CreateSampleData();

            WireUpLists();
        }

        // create sample records that i then add to lists to make sure hte lists are workign the way theyre suppose to.
        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Joey", LastName = "Driscoll" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Jenna", LastName = "Driscoll" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Mark", LastName = "Driscoll" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Sam", LastName = "Driscoll" });
        }

        //wire up combobox and dropdown to our list
        private void WireUpLists()
        {
            // set to null initially so refresh of data binding occurs every time. Not the best solution.
            selectTeamMemberDropDown.DataSource = null;

            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = null;

            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";
        }

        private void CreateMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel p = new PersonModel();

                p.FirstName = firstNameValue.Text;
                p.LastName = lastNameValue.Text;
                p.EmailAddress = emailValue.Text;
                p.CellphoneNumber = cellphoneValue.Text;

                p = GlobalConfig.Connection.CreatePerson(p);

                //add new members to the combo box;
                selectedTeamMembers.Add(p);

                //refresh to show new value;
                WireUpLists();

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellphoneValue.Text = "";
            }
            else
            {
                MessageBox.Show("You need to fill in all of the fields.");
            }
        }

        private bool ValidateForm()
        {
            if (firstNameValue.Text.Length == 0)
            {
                return false;
            }

            if (lastNameValue.Text.Length == 0)
            {
                return false;
            }

            if (emailValue.Text.Length == 0)
            {
                return false;
            }

            if (cellphoneValue.Text.Length == 0)
            {
                return false;
            }

            return true;
        }

        private void AddMemberButton_Click(object sender, EventArgs e)
        {
            // cast to (PersonModel) - normally an object but this is more specifically a PersonModel
            PersonModel p = (PersonModel)selectTeamMemberDropDown.SelectedItem;

            if (p != null)
            {
                // Find the person and remove them from availableTeamMembers List (drop down)
                availableTeamMembers.Remove(p);
                // Find the person and add them to selectedTeamMembers List (combo box)
                selectedTeamMembers.Add(p);

                WireUpLists();
            }
        }

        private void RemoveSelectedMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)teamMembersListBox.SelectedItem;

            if (p != null)
            {
                selectedTeamMembers.Remove(p);
                availableTeamMembers.Add(p);

                WireUpLists();
            }
        }
    }
}