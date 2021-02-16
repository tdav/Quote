using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuoteServer.Extensions;
using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Quote.Utils;
using Quote.Utils.Compress;
using Swashbuckle.AspNetCore.Annotations;
using QuoteServer.Views;
using QuoteServer.Utils;
using QuoteServer.Database.Services;
using Microsoft.AspNetCore.Authorization;

namespace QuoteServer.v1.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [SwaggerTag("Кушимча таблицалар синхронизацияси")]
    public class RefTablesController : ControllerBase
    {
        private readonly IUnitOfWork db;
        private readonly IGenSqlService gen;
        public RefTablesController(IUnitOfWork _db, IGenSqlService _gen)
        {
            db = _db;
            gen = _gen;
        }


        /// <summary>
        /// Получения справочных таблиц  (sp)
        /// </summary>
        /// <param name="r">счётчики RowVersion + инфо клиента</param>
        /// <returns></returns>
        [HttpPost("get")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<spDataList>> ToClient([FromBody] TransferModel r)
        {
            try
            {
                var res = new spDataList();
                var sql = gen.GetRefSql(r);
                
                #region get values
                res.lsAccessList = await db.FromSqlAsNoTracking<spAccessList>(string.Format(sql, "spaccesslists"));
                res.lsCountries = await db.FromSqlAsNoTracking<spCountry>(string.Format(sql, "spcountries"));
                res.lsDiscountTypes = await db.FromSqlAsNoTracking<spDiscountType>(string.Format(sql, "spdiscounttypes"));
                res.lsDistributors = await db.FromSqlAsNoTracking<spDistributor>(string.Format(sql, "spdistributors"));
                res.lsDistricts = await db.FromSqlAsNoTracking<spDistrict>(string.Format(sql, "spdistricts"));
                res.lsDrugs = await db.FromSqlAsNoTracking<spDrug>(string.Format(sql, "spdrugs"));
                res.lsDrugCategorys = await db.FromSqlAsNoTracking<spDrugCategory>(string.Format(sql, "spDrugCategorys"));
                res.lsDrugStores = await db.FromSqlAsNoTracking<spDrugStore>(string.Format(sql, "spdrugstores"));
                res.lsInsurances = await db.FromSqlAsNoTracking<spInsurance>(string.Format(sql, "spinsurances"));
                res.lsManufacturers = await db.FromSqlAsNoTracking<spManufacturer>(string.Format(sql, "spmanufacturers"));
                res.lsOnlinePayments = await db.FromSqlAsNoTracking<spOnlinePayment>(string.Format(sql, "sponlinepayments"));
                res.lsOperationTypes = await db.FromSqlAsNoTracking<spOperationType>(string.Format(sql, "spoperationtypes"));
                res.lsOrderStatus = await db.FromSqlAsNoTracking<spOrderStatus>(string.Format(sql, "sporderstatus"));
                res.lsPharmGroups = await db.FromSqlAsNoTracking<spPharmGroup>(string.Format(sql, "sppharmgroups"));
                res.lsRegions = await db.FromSqlAsNoTracking<spRegion>(string.Format(sql, "spregions"));
                res.lsRoles = await db.FromSqlAsNoTracking<spRole>(string.Format(sql, "sproles"));
                res.lsTypeOfPayments = await db.FromSqlAsNoTracking<spTypeOfPayment>(string.Format(sql, "sptypeofpayments"));
                res.lsStatus = await db.FromSqlAsNoTracking<spStatus>(string.Format(sql, "spstatus"));
                res.lsUnits = await db.FromSqlAsNoTracking<spUnit>(string.Format(sql, "spunits"));
                res.lsDrugRecomendations = await db.FromSqlAsNoTracking<spDrugRecomendation>(string.Format(sql, "spdrugrecomendations"));
                res.lsDrugAdditionalInfoes = await db.FromSqlAsNoTracking<spDrugAdditionalInfo>(string.Format(sql, "spdrugadditionalinfos"));

                #endregion

                #region set monitor value
                var ml = new List<tbMonitorSync>();
                ml.AddMonSys(r.Ip, r.Mac, "spAccessList", 0, 0, res.lsAccessList.Count);
                ml.AddMonSys(r.Ip, r.Mac, "spCountries", 0, 0, res.lsCountries.Count);
                ml.AddMonSys(r.Ip, r.Mac, "spDiscountTypes", 0, 0, res.lsDiscountTypes.Count);
                ml.AddMonSys(r.Ip, r.Mac, "spDistributors", 0, 0, res.lsDistributors.Count);
                ml.AddMonSys(r.Ip, r.Mac, "spDrugAdditionalInfoes", 0, 0, res.lsDrugAdditionalInfoes.Count);
                ml.AddMonSys(r.Ip, r.Mac, "spDrugCategorys", 0, 0, res.lsDrugCategorys.Count);
                ml.AddMonSys(r.Ip, r.Mac, "spDrugRecomendations", 0, 0, res.lsDrugRecomendations.Count);
                ml.AddMonSys(r.Ip, r.Mac, "spOperationTypes", 0, 0, res.lsOperationTypes.Count);
                ml.AddMonSys(r.Ip, r.Mac, "spOrderStatus", 0, 0, res.lsOrderStatus.Count);
                ml.AddMonSys(r.Ip, r.Mac, "spPharmGroups", 0, 0, res.lsPharmGroups.Count);
                ml.AddMonSys(r.Ip, r.Mac, "spRegions", 0, 0, res.lsRegions.Count);
                ml.AddMonSys(r.Ip, r.Mac, "spRoles", 0, 0, res.lsRoles.Count);
                ml.AddMonSys(r.Ip, r.Mac, "spStatus", 0, 0, res.lsStatus.Count);
                ml.AddMonSys(r.Ip, r.Mac, "spTypeOfPayments", 0, 0, res.lsTypeOfPayments.Count);
                ml.AddMonSys(r.Ip, r.Mac, "spUnits", 0, 0, res.lsUnits.Count);
                ml.AddMonSys(r.Ip, r.Mac, "spManufacturers", 0, 0, res.lsManufacturers.Count);
                ml.AddMonSys(r.Ip, r.Mac, "spDistricts", 0, 0, res.lsDistricts.Count);
                ml.AddMonSys(r.Ip, r.Mac, "spDrugStores", 0, 0, res.lsDrugStores.Count);
                ml.AddMonSys(r.Ip, r.Mac, "spDrugs", 0, 0, res.lsDrugs.Count);

                if (ml.Count > 0)
                {
                    var rp = db.GetRepository<tbMonitorSync>();
                    await rp.InsertAsync(ml);
                    await db.CommitAsync();
                }

                #endregion

                Console.WriteLine("REF TABLE GET");
                CConsole.WriteLine(sql, ConsoleColor.Red);
                CConsole.WriteLine(r.ToString(), ConsoleColor.Green);
                Console.WriteLine("");

                return Ok(res);
            }
            catch (Exception ee)
            {
                var li = new LogItem
                {
                    App = "waAptekaN3",
                    Url = "api/RefTables",
                    Stacktrace = ee.GetStackTrace(5),
                    Message = ee.GetAllMessages(),
                    Method = "Put"
                };
                CLogJson.Write(li);
                return Ok(new ResponseModelV1() { Status = -1, StatusMessages = ee.Message + Environment.NewLine + ee.StackTrace });
            }
        }


        /// <summary>
        /// Отправка справочных таблиц (sp)
        /// </summary>
        /// <param name="spResponseList">Таблицы для сервера + инфо клиента</param>
        /// <returns></returns>
        [HttpPost("set")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<ResponseModelV1>> FromClient()
        {
            try
            {
                var data = Request.BodyReader.AsStream();
                var ls = CZip.Decompress<spDataList>(data);

                db.BeginTransaction();
                db.SetForceMode();

                #region set data

                int r = 0, upd = 0, ins = 0;
                var ml = new List<tbMonitorSync>();

                if (ls.lsAccessList != null)
                {
                    var rp = db.GetRepository<spAccessList>();
                    foreach (var it in ls.lsAccessList)
                    {
                        r = await rp.MergeAsync(it.Id, it);
                        if (r == 1) ins++; else upd++;
                    }
                    ml.AddMonSys(ls.Ip, ls.Mac, "spAccessList", ins, upd);
                }

                if (ls.lsCountries != null)
                {
                    upd = 0; ins = 0;
                    var rp = db.GetRepository<spCountry>();
                    foreach (var it in ls.lsCountries)
                    {
                        r = await rp.MergeAsync(it.Id, it);
                        if (r == 1) ins++; else upd++;
                    }
                    ml.AddMonSys(ls.Ip, ls.Mac, "spCountries", ins, upd);
                }

                if (ls.lsDiscountTypes != null)
                {
                    upd = 0; ins = 0;
                    var rp = db.GetRepository<spDiscountType>();
                    foreach (var it in ls.lsDiscountTypes)
                    {
                        r = await rp.MergeAsync(it.Id, it);
                        if (r == 1) ins++; else upd++;
                    }
                    ml.AddMonSys(ls.Ip, ls.Mac, "spDiscountTypes", ins, upd);
                }

                if (ls.lsDistributors != null)
                {
                    upd = 0; ins = 0;
                    var rp = db.GetRepository<spDistributor>();
                    foreach (var it in ls.lsDistributors)
                    {
                        r = await rp.MergeAsync(it.Id, it);
                        if (r == 1) ins++; else upd++;
                    }
                    ml.AddMonSys(ls.Ip, ls.Mac, "spDistributors", ins, upd);
                }

                if (ls.lsDrugAdditionalInfoes != null)
                {
                    upd = 0; ins = 0;
                    var rp = db.GetRepository<spDrugAdditionalInfo>();
                    foreach (var it in ls.lsDrugAdditionalInfoes)
                    {
                        r = await rp.MergeAsync(it.Id, it);
                        if (r == 1) ins++; else upd++;
                    }
                    ml.AddMonSys(ls.Ip, ls.Mac, "spDrugAdditionalInfos", ins, upd);
                }

                if (ls.lsDrugCategorys != null)
                {
                    upd = 0; ins = 0;
                    var rp = db.GetRepository<spDrugCategory>();
                    foreach (var it in ls.lsDrugCategorys)
                    {
                        r = await rp.MergeAsync(it.Id, it);
                        if (r == 1) ins++; else upd++;
                    }
                    ml.AddMonSys(ls.Ip, ls.Mac, "spDrugCategorys", ins, upd);
                }

                if (ls.lsDrugRecomendations != null)
                {
                    upd = 0; ins = 0;
                    var rp = db.GetRepository<spDrugRecomendation>();
                    foreach (var it in ls.lsDrugRecomendations)
                    {
                        r = await rp.MergeAsync(it.Id, it);
                        if (r == 1) ins++; else upd++;
                    }
                    ml.AddMonSys(ls.Ip, ls.Mac, "spDrugRecomendations", ins, upd);
                }

                if (ls.lsOperationTypes != null)
                {
                    upd = 0; ins = 0;
                    var rp = db.GetRepository<spOperationType>();
                    foreach (var it in ls.lsOperationTypes)
                    {
                        r = await rp.MergeAsync(it.Id, it);
                        if (r == 1) ins++; else upd++;
                    }
                    ml.AddMonSys(ls.Ip, ls.Mac, "spOperationTypes", ins, upd);
                }

                if (ls.lsOrderStatus != null)
                {
                    upd = 0; ins = 0;
                    var rp = db.GetRepository<spOrderStatus>();
                    foreach (var it in ls.lsOrderStatus)
                    {
                        r = await rp.MergeAsync(it.Id, it);
                        if (r == 1) ins++; else upd++;
                    }
                    ml.AddMonSys(ls.Ip, ls.Mac, "spOrderStatus", ins, upd);
                }

                if (ls.lsPharmGroups != null)
                {
                    upd = 0; ins = 0;
                    var rp = db.GetRepository<spPharmGroup>();
                    foreach (var it in ls.lsPharmGroups)
                    {
                        r = await rp.MergeAsync(it.Id, it);
                        if (r == 1) ins++; else upd++;
                    }
                    ml.AddMonSys(ls.Ip, ls.Mac, "spPharmGroups", ins, upd);
                }

                if (ls.lsRegions != null)
                {
                    upd = 0; ins = 0;
                    var rp = db.GetRepository<spRegion>();
                    foreach (var it in ls.lsRegions)
                    {
                        r = await rp.MergeAsync(it.Id, it);
                        if (r == 1) ins++; else upd++;
                    }
                    ml.AddMonSys(ls.Ip, ls.Mac, "spRegions", ins, upd);
                }

                if (ls.lsRoles != null)
                {
                    upd = 0; ins = 0;
                    var rp = db.GetRepository<spRole>();
                    foreach (var it in ls.lsRoles)
                    {
                        r = await rp.MergeAsync(it.Id, it);
                        if (r == 1) ins++; else upd++;
                    }
                    ml.AddMonSys(ls.Ip, ls.Mac, "spRoles", ins, upd);
                }

                if (ls.lsStatus != null)
                {
                    upd = 0; ins = 0;
                    var rp = db.GetRepository<spStatus>();
                    foreach (var it in ls.lsStatus)
                    {
                        r = await rp.MergeAsync(it.Id, it);
                        if (r == 1) ins++; else upd++;
                    }
                    ml.AddMonSys(ls.Ip, ls.Mac, "spStatus", ins, upd);
                }

                if (ls.lsTypeOfPayments != null)
                {
                    upd = 0; ins = 0;
                    var rp = db.GetRepository<spTypeOfPayment>();
                    foreach (var it in ls.lsTypeOfPayments)
                    {
                        r = await rp.MergeAsync(it.Id, it);
                        if (r == 1) ins++; else upd++;
                    }
                    ml.AddMonSys(ls.Ip, ls.Mac, "spTypeOfPayments", ins, upd);
                }

                if (ls.lsUnits != null)
                {
                    upd = 0; ins = 0;
                    var rp = db.GetRepository<spUnit>();
                    foreach (var it in ls.lsUnits)
                    {
                        r = await rp.MergeAsync(it.Id, it);
                        if (r == 1) ins++; else upd++;
                    }
                    ml.AddMonSys(ls.Ip, ls.Mac, "spUnits", ins, upd);
                }

                if (ls.lsManufacturers != null)
                {
                    upd = 0; ins = 0;
                    var rp = db.GetRepository<spManufacturer>();
                    foreach (var it in ls.lsManufacturers)
                    {
                        r = await rp.MergeAsync(it.Id, it);
                        if (r == 1) ins++; else upd++;
                    }
                    ml.AddMonSys(ls.Ip, ls.Mac, "spManufacturers", ins, upd);
                }

                if (ls.lsDistricts != null)
                {
                    upd = 0; ins = 0;
                    var rp = db.GetRepository<spDistrict>();
                    foreach (var it in ls.lsDistricts)
                    {
                        r = await rp.MergeAsync(it.Id, it);
                        if (r == 1) ins++; else upd++;
                    }
                    ml.AddMonSys(ls.Ip, ls.Mac, "spDistricts", ins, upd);
                }

                if (ls.lsDrugStores != null)
                {
                    upd = 0; ins = 0;
                    var rp = db.GetRepository<spDrugStore>();
                    foreach (var it in ls.lsDrugStores)
                    {
                        r = await rp.MergeAsync(it.Id, it);
                        if (r == 1) ins++; else upd++;
                    }
                    ml.AddMonSys(ls.Ip, ls.Mac, "spDrugStores", ins, upd);
                }

                if (ls.lsDrugs != null)
                {
                    upd = 0; ins = 0;
                    var rp = db.GetRepository<spDrug>();
                    foreach (var it in ls.lsDrugs)
                    {
                        r = await rp.MergeAsync(it.Id, it);
                        if (r == 1) ins++; else upd++;
                    }
                    ml.AddMonSys(ls.Ip, ls.Mac, "spDrugs", ins, upd);
                }


                #endregion

                db.DetectChanges();

                if (ml.Count > 0)
                {
                    var rp = db.GetRepository<tbMonitorSync>();
                    await rp.InsertAsync(ml);
                    await db.CommitAsync();

                    Console.WriteLine("REF TABLE POST");
                    CConsole.WriteLine(ml.ToValues(), ConsoleColor.Green);
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("REF TABLE POST");
                    CConsole.WriteLine("Маълумот йук", ConsoleColor.Green);
                    Console.WriteLine("");
                }


                await db.SaveChangesAsync();
                await db.CommitAsync();

                return Ok(new ResponseModelV1()
                {
                    Status = 1,
                    StatusMessages = "OK",
                    ServerDateTime = DateTime.Now.ToString("ddMMyyyyHHmmss")
                });
            }
            catch (System.Exception ee)
            {
                db.Rollback();
                return Ok(new ResponseModelV1() { Status = -1, StatusMessages = ee.Message + Environment.NewLine + ee.StackTrace });
            }
        }
    }
}
