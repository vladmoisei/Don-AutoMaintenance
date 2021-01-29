using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCWithBlazor.Data;
using MVCWithBlazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWithBlazor.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ReportDbContext _context;

        public DashboardService(ReportDbContext context)
        {
            _context = context;
        }

        public async Task<List<UtilajModel>> GetListOfUtilaj()
        {
            return await _context.UtilajModels.ToListAsync();
        }

        public async Task<UtilajModel> GetUtilajById(int id)
        {
            return await _context.UtilajModels.FirstOrDefaultAsync(t => t.UtilajModelID == id);
        }

        public async Task<ActiuneModel> GetActiuneById(int id)
        {
            return await _context.ActiuneModels.FirstOrDefaultAsync(t => t.ActiuneModelID == id);
        }

        public async Task<ActionCheckModel> GetCheckById(int id)
        {
            return await _context.CheckModels.FirstOrDefaultAsync(t => t.ActionCheckModelID == id);
        }

        public async Task<List<ActiuneModel>> GetActiuniByTipAndByUtilaj(TipActiune tipActiune, int utilajId)
        {
            // Get Lista Actiuni By Type and By Utilaj
            List<ActiuneModel> listaActiuniZilnice = await _context.ActiuneModels.Where(actiune => actiune.Tip == tipActiune && actiune.UtilajModelID == utilajId).ToListAsync();
            return listaActiuniZilnice;
        }
        // Add New Check
        public async Task AddNewCheck(ActionCheckModel check)
        {
            _context.Add(check);
            await _context.SaveChangesAsync();
        }

        // Add Daily Checks
        public async Task AddNewDailyChecks(int utilajSelectatId, DateTime data)
        {
            var actionChecks = await GetDailyChecks(utilajSelectatId, data);

            if (actionChecks.Count == 0)
            {
                List<ActiuneModel> ListaActiuni = await GetActiuniByTipAndByUtilaj(TipActiune.Zilnic, utilajSelectatId);
                foreach (ActiuneModel actiune in ListaActiuni)
                {
                    for (int i = 0; i < DateTime.DaysInMonth(data.Year, data.Month); i++)
                    {
                        // Add Acton check by operator
                        _context.Add(new ActionCheckModel
                        {
                            ActiuneModelID = actiune.ActiuneModelID,
                            DataLucru = new DateTime(data.Year, data.Month, i + 1)
                        });

                        // Add Action Check by sef de schimb
                        _context.Add(new ActionCheckModel
                        {
                            ActiuneModelID = actiune.ActiuneModelID,
                            DataLucru = new DateTime(data.Year, data.Month, i + 1),
                            SefDeSchimb = "sef"
                        });
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        // Add Weekly Checks
        public async Task AddNewWeeklyChecks(int utilajSelectatId, DateTime data)
        {
            var actionChecks = await GetWeeklyChecks(utilajSelectatId, data);

            if (actionChecks.Count == 0)
            {
                {
                    List<ActiuneModel> ListaActiuni = await GetActiuniByTipAndByUtilaj(TipActiune.Saptamanal, utilajSelectatId);
                    foreach (ActiuneModel actiune in ListaActiuni)
                    {
                        for (int i = 0; i < DateTime.DaysInMonth(data.Year, data.Month); i++)
                        {
                            DayOfWeek day = new DateTime(data.Year, data.Month, i + 1).DayOfWeek;
                            // Add only on monday weekly actions
                            if (day == DayOfWeek.Monday)
                            {
                                // Add Acton check by operator
                                _context.CheckModels.Add(new ActionCheckModel
                                {
                                    ActiuneModelID = actiune.ActiuneModelID,
                                    DataLucru = new DateTime(data.Year, data.Month, i + 1)
                                });

                                // Add Action Check by sef de schimb
                                _context.CheckModels.Add(new ActionCheckModel
                                {
                                    ActiuneModelID = actiune.ActiuneModelID,
                                    DataLucru = new DateTime(data.Year, data.Month, i + 1),
                                    SefDeSchimb = "sef"
                                });
                            }
                        }
                    }
                    await _context.SaveChangesAsync();
                }

            }
        }
        // Add Monthly Checks
        public async Task AddNewMonthlyChecks(int utilajSelectatId, DateTime data)
        {
            var actionChecks = await GetMonthlyChecks(utilajSelectatId, data);

            if (actionChecks.Count == 0)
            {

                List<ActiuneModel> ListaActiuni = await GetActiuniByTipAndByUtilaj(TipActiune.Lunar, utilajSelectatId);
                foreach (ActiuneModel actiune in ListaActiuni)
                {
                    // Add only on 1st day monthly actions
                    // Add Acton check by operator
                    _context.Add(new ActionCheckModel
                    {
                        ActiuneModelID = actiune.ActiuneModelID,
                        DataLucru = new DateTime(data.Year, data.Month, 1)
                    });

                    // Add Action Check by sef de schimb
                    _context.Add(new ActionCheckModel
                    {
                        ActiuneModelID = actiune.ActiuneModelID,
                        DataLucru = new DateTime(data.Year, data.Month, 1),
                        SefDeSchimb = "sef"
                    });

                }
                await _context.SaveChangesAsync();
            }

        }

        // Add Trimestrial Checks
        public async Task AddNewTrimestrialChecks(int utilajSelectatId, DateTime data)
        {
            var actionChecks = await GetTrimestrialChecks(utilajSelectatId, data);

            if (actionChecks.Count == 0)
            {
                List<ActiuneModel> ListaActiuni = await GetActiuniByTipAndByUtilaj(TipActiune.Trimestrial, utilajSelectatId);
                foreach (ActiuneModel actiune in ListaActiuni)
                {
                    // Add only on 1st day trimestrial actions
                    // Add Acton check by operator
                    _context.Add(new ActionCheckModel
                    {
                        ActiuneModelID = actiune.ActiuneModelID,
                        DataLucru = new DateTime(data.Year, data.Month, 1)
                    });

                    // Add Action Check by sef de schimb
                    _context.Add(new ActionCheckModel
                    {
                        ActiuneModelID = actiune.ActiuneModelID,
                        DataLucru = new DateTime(data.Year, data.Month, 1),
                        SefDeSchimb = "sef"
                    });

                }

                await _context.SaveChangesAsync();
            }
        }

        // Add Semestrial Checks
        public async Task AddNewSemestrialChecks(int utilajSelectatId, DateTime data)
        {
            var actionChecks = await GetSemestrialChecks(utilajSelectatId, data);

            if (actionChecks.Count == 0)
            {
                List<ActiuneModel> ListaActiuni = await GetActiuniByTipAndByUtilaj(TipActiune.Semestrial, utilajSelectatId);
                foreach (ActiuneModel actiune in ListaActiuni)
                {
                    // Add only on 1st day semestrial actions
                    // Add Acton check by operator
                    _context.Add(new ActionCheckModel
                    {
                        ActiuneModelID = actiune.ActiuneModelID,
                        DataLucru = new DateTime(data.Year, data.Month, 1)
                    });

                    // Add Action Check by sef de schimb
                    _context.Add(new ActionCheckModel
                    {
                        ActiuneModelID = actiune.ActiuneModelID,
                        DataLucru = new DateTime(data.Year, data.Month, 1),
                        SefDeSchimb = "sef"
                    });
                }

                await _context.SaveChangesAsync();
            }
        }

        // Add Anual Checks
        public async Task AddNewAnualChecks(int utilajSelectatId, DateTime data)
        {
            var actionChecks = await GetAnualChecks(utilajSelectatId, data);

            if (actionChecks.Count == 0)
            {
                List<ActiuneModel> ListaActiuni = await GetActiuniByTipAndByUtilaj(TipActiune.Anual, utilajSelectatId);
                foreach (ActiuneModel actiune in ListaActiuni)
                {
                    // Add only on 1st day anual actions
                    // Add Acton check by operator
                    _context.Add(new ActionCheckModel
                    {
                        ActiuneModelID = actiune.ActiuneModelID,
                        DataLucru = new DateTime(data.Year, data.Month, 1)
                    });

                    // Add Action Check by sef de schimb
                    _context.Add(new ActionCheckModel
                    {
                        ActiuneModelID = actiune.ActiuneModelID,
                        DataLucru = new DateTime(data.Year, data.Month, 1),
                        SefDeSchimb = "sef"
                    });
                }

                await _context.SaveChangesAsync();
            }
        }

        // Creaza verificari pentru toata luna, pentru toate utilajele
        public async Task CreateChecksPerMonth(DateTime data)
        {
            // For each utilaj add checks every month
            foreach (var utilaj in await GetListOfUtilaj())
            {
                await AddNewDailyChecks(utilaj.UtilajModelID, data);
                await AddNewWeeklyChecks(utilaj.UtilajModelID, data);
                await AddNewMonthlyChecks(utilaj.UtilajModelID, data);
                if (data.Month % 3 == 0)
                    await AddNewTrimestrialChecks(utilaj.UtilajModelID, data);
                if (data.Month % 6 == 0)
                    await AddNewSemestrialChecks(utilaj.UtilajModelID, data);
            }
        }

        // Get Daily Actions by utilaj and by month
        public async Task<List<ActionCheckModel>> GetDailyChecks(int utilajSelectatId, DateTime data)
        {
            var actiuniZilnice = await _context.CheckModels.Include(o => o.ActiuneModel).Where(check =>
            check.ActiuneModel.UtilajModelID == utilajSelectatId &&
            check.ActiuneModel.Tip == TipActiune.Zilnic &&
            check.DataLucru.Year == data.Year &&
            check.DataLucru.Month == data.Month).ToListAsync();

            return actiuniZilnice;
        }

        // Get Weekly Actions by utilaj and by month
        public async Task<List<ActionCheckModel>> GetWeeklyChecks(int utilajSelectatId, DateTime data)
        {
            var actiuniSaptamanale = await _context.CheckModels.Include(o => o.ActiuneModel).Where(check =>
             check.ActiuneModel.UtilajModelID == utilajSelectatId &&
             check.ActiuneModel.Tip == TipActiune.Saptamanal &&
             check.DataLucru.Year == data.Year &&
             check.DataLucru.Month == data.Month).ToListAsync();

            return actiuniSaptamanale;
        }

        // Get Monthly Actions by utilaj and by month
        public async Task<List<ActionCheckModel>> GetMonthlyChecks(int utilajSelectatId, DateTime data)
        {
            var actiuniSaptamanale = await _context.CheckModels.Include(o => o.ActiuneModel).Where(check =>
             check.ActiuneModel.UtilajModelID == utilajSelectatId &&
             check.ActiuneModel.Tip == TipActiune.Lunar &&
             check.DataLucru.Year == data.Year &&
             check.DataLucru.Month == data.Month).ToListAsync();

            return actiuniSaptamanale;
        }

        // Get Trimestrial Actions by utilaj and by month
        public async Task<List<ActionCheckModel>> GetTrimestrialChecks(int utilajSelectatId, DateTime data)
        {
            var actiuniSaptamanale = await _context.CheckModels.Include(o => o.ActiuneModel).Where(check =>
             check.ActiuneModel.UtilajModelID == utilajSelectatId &&
             check.ActiuneModel.Tip == TipActiune.Trimestrial &&
             check.DataLucru.Year == data.Year &&
             check.DataLucru.Month == data.Month).ToListAsync();

            return actiuniSaptamanale;
        }

        // Get Semestrial Actions by utilaj and by month
        public async Task<List<ActionCheckModel>> GetSemestrialChecks(int utilajSelectatId, DateTime data)
        {
            var actiuniSaptamanale = await _context.CheckModels.Include(o => o.ActiuneModel).Where(check =>
             check.ActiuneModel.UtilajModelID == utilajSelectatId &&
             check.ActiuneModel.Tip == TipActiune.Semestrial &&
             check.DataLucru.Year == data.Year &&
             check.DataLucru.Month == data.Month).ToListAsync();

            return actiuniSaptamanale;
        }

        // Get Anual Actions by utilaj and by month
        public async Task<List<ActionCheckModel>> GetAnualChecks(int utilajSelectatId, DateTime data)
        {
            var actiuniSaptamanale = await _context.CheckModels.Include(o => o.ActiuneModel).Where(check =>
             check.ActiuneModel.UtilajModelID == utilajSelectatId &&
             check.ActiuneModel.Tip == TipActiune.Anual &&
             check.DataLucru.Year == data.Year &&
             check.DataLucru.Month == data.Month).ToListAsync();

            return actiuniSaptamanale;
        }

        // Handle Check Event By operator
        public async Task HandleCheckByOperator(int checkID, string user)
        {
            var check = _context.CheckModels.Include(o => o.ActiuneModel).FirstOrDefault(c =>
             c.ActionCheckModelID == checkID);

            if (check != null)
            {
                check.IsCheckedByOp = !check.IsCheckedByOp;
                check.DataBifatByOp = DateTime.Now;
                check.Username = user;
                _context.Update(check);

                await _context.SaveChangesAsync();
            }
        }

        // Handle Check Event by Sef De schimb
        public async Task HandleCheckBySef(int checkID, string user)
        {
            var check = _context.CheckModels.Include(o => o.ActiuneModel).FirstOrDefault(c =>
             c.ActionCheckModelID == checkID);

            if (check != null)
            {
                check.IsCheckedBySef = !check.IsCheckedBySef;
                check.DataBifatBySef = DateTime.Now;
                check.SefDeSchimb = user;
                _context.Update(check);

                await _context.SaveChangesAsync();
            }
        }
    }
}
