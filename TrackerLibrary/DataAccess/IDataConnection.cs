using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAcess
{
    public interface IDataConnection
    {
        // Contract, speficy what methods, properties that need to be here
        PrizeModel createPrize(PrizeModel model); // everything is public in the interface, we cant put code in here 
                                                  // just saying whoever is implementing IDataCollection will have a method called create Prize

        PersonModel CreatePerson(PersonModel model);
        List<PersonModel> GetPersonAll();
        TeamModel CreateTeam(TeamModel model);
        List<TeamModel> GetTeam_All();
        void CreateTournament(TournamentModel model);
    }
}
