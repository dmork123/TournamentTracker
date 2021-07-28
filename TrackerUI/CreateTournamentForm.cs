using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTournamentForm : Form, IPrizeRequester, ITeamRequester
    {
        private List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeam_All();
        List<TeamModel> selectedTeams = new List<TeamModel>();
        List<PrizeModel> selecetdPrizes = new List<PrizeModel>(); 
        public CreateTournamentForm()
        {
            InitializeComponent();
            WireUpLists();
        }
        private void WireUpLists()
        {
            selectTeamDropDown.DataSource = null;
            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = "TeamName";

            tournamentTeamsListBox.DataSource = null;
            tournamentTeamsListBox.DataSource = selectedTeams;
            tournamentTeamsListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = null;
            prizesListBox.DataSource = selecetdPrizes;
            prizesListBox.DisplayMember = "PlaceName";
        }

        private void AddTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel tm = (TeamModel)selectTeamDropDown.SelectedItem;
            if(tm!= null)
            {
                availableTeams.Remove(tm);
                selectedTeams.Add(tm);
                WireUpLists();
            }
        }

        private void CreatePrizeButton_Click(object sender, EventArgs e)
        {
            // Call the create prize form
            CreatePrizeForm frm = new CreatePrizeForm(this);
            frm.Show();
            

        }

        public void PrizeComplete(PrizeModel model)
        {

            // get back from the form a prizeModel
            // take the prize model and put it in the list of selected prizes 
            selecetdPrizes.Add(model);
            WireUpLists();
        }

        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
            WireUpLists();
        }

        private void CreateNewTeamLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm frm = new CreateTeamForm(this);
            frm.Show();
        }

        private void RemoveSelectedPlayerbutton_Click(object sender, EventArgs e)
        {
            TeamModel tm = (TeamModel)tournamentTeamsListBox.SelectedItem;
            if( tm!=null )
            {
                selectedTeams.Remove(tm);
                availableTeams.Add(tm);
                WireUpLists();
            }
            
        }

        private void RemoveSelectedPrizebutton_Click(object sender, EventArgs e)
        {
            PrizeModel pm = (PrizeModel)prizesListBox.SelectedItem;
            if (pm != null)
            {
                selecetdPrizes.Remove(pm);
                WireUpLists();
            }
        }

        private void CreateTournamentButton_Click(object sender, EventArgs e)
        {
            // validate data 
            decimal fee = 0;
            bool feeAcceptable = decimal.TryParse(entryFeeValue.Text, out fee);
            if(!feeAcceptable)
            {
                MessageBox.Show("You need to enter a valid entry fee.",
                    "Invalid fee", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                return;
            }
            // Create our tournament model
            TournamentModel tm = new TournamentModel();

            tm.TournamentName = tournamentNameValue.Text;
            tm.EntryFee = fee;
            tm.EnteredTeam = selectedTeams;
            tm.Prizes = selecetdPrizes;

            // TODO: Wire our matchups
            TournamentLogic.CreateRounds(tm);


            // Create Tournament entry
            // Create all of the prizes entries
            // Create all of team entries 
            GlobalConfig.Connection.CreateTournament(tm);

            

        }
    }
}
