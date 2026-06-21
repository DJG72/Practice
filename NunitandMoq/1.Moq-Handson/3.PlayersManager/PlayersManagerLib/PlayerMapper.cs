using System.Data;
using System.Data.SqlClient;

namespace PlayersManagerLib
{
    public class PlayerMapper : IPlayerMapper
    {
        public bool IsPlayerNameExistsInDb(string name)
        {
            return false;
        }

        public void AddNewPlayerIntoDb(string name)
        {
        }
    }
}