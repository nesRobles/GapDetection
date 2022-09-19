using System.Collections.Generic;

namespace GapDetectionDataManager.Library.Internal.DataAccess
{
    public interface ISqliteDataAccess
    {
        List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName, bool isStoredProcedure = false);
        void SaveData<T>(string storedProcedure, T parameters, string connectionStringName, bool isStoredProcedure = false);
    }
}