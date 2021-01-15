using MVCWithBlazor.Data;
using MVCWithBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWithBlazor.Services
{
    public interface IDashboardService
    {
        Task<List<UtilajModel>> GetListOfUtilaj();

        Task<UtilajModel> GetUtilajById(int id);

        Task<ActiuneModel> GetActiuneById(int id);

        Task<ActionCheckModel> GetCheckById(int id);

        Task<List<ActiuneModel>> GetActiuniByTipAndByUtilaj(TipActiune tipActiune, int utilajId);

        Task AddNewCheck(ActionCheckModel check);

        Task AddNewDailyChecks(int utilajSelectatId, DateTime data);

        Task AddNewWeeklyChecks(int utilajSelectatId, DateTime data);

        Task AddNewMonthlyChecks(int utilajSelectatId, DateTime data);

        Task AddNewTrimestrialChecks(int utilajSelectatId, DateTime data);

        Task AddNewSemestrialChecks(int utilajSelectatId, DateTime data);

        Task AddNewAnualChecks(int utilajSelectatId, DateTime data);

        Task CreateChecksPerMonth(DateTime data);

        Task<List<ActionCheckModel>> GetDailyChecks(int utilajSelectatId, DateTime data);

        Task<List<ActionCheckModel>> GetWeeklyChecks(int utilajSelectatId, DateTime data);

        Task<List<ActionCheckModel>> GetMonthlyChecks(int utilajSelectatId, DateTime data);

        Task<List<ActionCheckModel>> GetTrimestrialChecks(int utilajSelectatId, DateTime data);

        Task<List<ActionCheckModel>> GetSemestrialChecks(int utilajSelectatId, DateTime data);

        Task<List<ActionCheckModel>> GetAnualChecks(int utilajSelectatId, DateTime data);


    }


}
