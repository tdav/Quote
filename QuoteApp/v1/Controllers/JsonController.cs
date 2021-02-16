using QuoteServer.Extensions;
using QuoteServer.Utils;
using QuoteServer.Utils.Serializable;
using QuoteServer.Views;
using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Quote.Utils;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuoteServer.v1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [SwaggerTag("Json")]
    public class JsonController : ControllerBase
    {
        private readonly IUnitOfWork db;

        public JsonController(IUnitOfWork _db) => db = _db;


        [HttpPost("load_json")]
        public async Task<ActionResult> LoadAsync()
        {
            _ = await LoadTableRefs();
            _ = await LoadTableMain();

            return Ok();
        }

        [HttpPost("save_json")]
        public async Task<ActionResult> SaveAsync()
        {
            _ = await SaveTableMain();
            _ = await SaveTableRef();

            return Ok();
        }


        private async Task<bool> LoadTableRefs()
        {
            var str = CFile.GetFileContents(AppDomain.CurrentDomain.BaseDirectory + "ref.json");
            var ls = JsonConvert.DeserializeObject<spDataList>(str);

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

            return true;
        }

        private async Task<bool> LoadTableMain()
        {
            var str = CFile.GetFileContents(AppDomain.CurrentDomain.BaseDirectory + "main.json");
            var ls = JsonConvert.DeserializeObject<tbDataList>(str);

            var ml = new List<tbMonitorSync>();

            db.BeginTransaction();

            int upd;
            int ins;
            int r;

            #region set value                  

            if (ls.lsChangeHistories != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbChangeHistory>();
                foreach (var it in ls.lsChangeHistories)
                {
                    it.Send = 1;
                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbChangeHistories", ins, upd);
            }

            if (ls.lsCustomers != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbCustomer>();
                foreach (var it in ls.lsCustomers)
                {
                    it.Send = 1;
                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbCustomers", ins, upd);
            }

            if (ls.lsInventories != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbInventory>();
                foreach (var it in ls.lsInventories)
                {
                    it.Send = 1;
                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbInventories", ins, upd);
            }

            if (ls.lsInventoryCurrentLists != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbInventoryCurrentList>();
                foreach (var it in ls.lsInventoryCurrentLists)
                {
                    it.Send = 1;
                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbInventoryCurrentLists", ins, upd);
            }

            if (ls.lsReturnOfProducts != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbReturnOfProduct>();
                foreach (var it in ls.lsReturnOfProducts)
                {
                    it.Send = 1;
                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbReturnOfProducts", ins, upd);
            }

            if (ls.lsRevaluationOfDrugs != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbRevaluationOfDrug>();
                foreach (var it in ls.lsRevaluationOfDrugs)
                {
                    it.Send = 1;
                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbRevaluationOfDrugs", ins, upd);
            }


            if (ls.lsSmenas != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbSmena>();
                foreach (var it in ls.lsSmenas)
                {
                    it.Send = 1;
                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbSmenas", ins, upd);
            }

            if (ls.lsWriteOffs != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbWriteOff>();
                foreach (var it in ls.lsWriteOffs)
                {
                    it.Send = 1;
                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbWriteOffs", ins, upd);
            }

            if (ls.lsUsers != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbUser>();
                foreach (var it in ls.lsUsers)
                {
                    it.Send = 1;
                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbUsers", ins, upd);
            }

            if (ls.lsDrugAdditionalInfoes != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbDrugAdditionalInfo>();
                foreach (var it in ls.lsDrugAdditionalInfoes)
                {
                    it.Send = 1;
                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbDrugAdditionalInfoes", ins, upd);
            }

            if (ls.lsDiscountCard != null)
            {
                upd = 0; ins = 0;
                foreach (var it in ls.lsDiscountCard)
                {
                    it.Send = 1;

                    var rp = db.GetRepository<tbDiscountCard>();
                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbDiscounts", ins, upd);
            }


            if (ls.lsCashbackCards != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbCashbackCard>();
                foreach (var it in ls.lsCashbackCards)
                {
                    it.Send = 1;

                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbCashbackCards", ins, upd);
            }

            if (ls.lsCashExpenses != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbCashExpenses>();
                foreach (var it in ls.lsCashExpenses)
                {
                    it.Send = 1;

                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbCashExpenses", ins, upd);
            }

            if (ls.lsEncashments != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbEncashment>();
                foreach (var it in ls.lsEncashments)
                {
                    it.Send = 1;

                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbEncashments", ins, upd);
            }

            if (ls.lsCashVoucher != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbCashVoucher>();
                foreach (var it in ls.lsCashVoucher)
                {
                    it.Send = 1;

                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbCashVoucher", ins, upd);
            }

            if (ls.lsComingProducts != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbComingProducts>();
                foreach (var it in ls.lsComingProducts)
                {
                    it.Send = 1;

                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbComingProducts", ins, upd);
            }

            if (ls.lsReceipts != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbReceipt>();
                foreach (var it in ls.lsReceipts)
                {
                    it.Send = 1;

                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbReceipts", ins, upd);
            }

            if (ls.lsTurnovers != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbTurnover>();
                foreach (var it in ls.lsTurnovers)
                {
                    it.Send = 1;

                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbTurnovers", ins, upd);
            }

            if (ls.lsCashVoucherItem != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbCashVoucherItem>();
                foreach (var it in ls.lsCashVoucherItem)
                {
                    it.Send = 1;

                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbCashVoucherItem", ins, upd);
            }

            if (ls.lsComingProductsItem != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbComingProductsItem>();
                foreach (var it in ls.lsComingProductsItem)
                {
                    it.Send = 1;

                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbComingProductsItem", ins, upd);
            }


            if (ls.lsDefectures != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbDefecture>();
                foreach (var it in ls.lsDefectures)
                {

                    it.Send = 1;

                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;

                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbDefectures", ins, upd);
            }

            if (ls.lsBuyOnCredits != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbBuyOnCredit>();
                foreach (var it in ls.lsBuyOnCredits)
                {
                    it.Send = 1;

                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbBuyOnCredits", ins, upd);
            }

            if (ls.lsDebtRepayments != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbDebtRepayment>();
                foreach (var it in ls.lsDebtRepayments)
                {
                    it.Send = 1;

                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbDebtRepayments", ins, upd);
            }

            if (ls.lsInventoryItems != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbInventoryItem>();
                foreach (var it in ls.lsInventoryItems)
                {
                    it.Send = 1;

                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbInventoryItems", ins, upd);
            }

            if (ls.lsSdachis != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbSdachi>();
                foreach (var it in ls.lsSdachis)
                {
                    it.Send = 1;

                    r = await rp.MergeAsync(it.Id, it);
                    if (r == 1) ins++; else upd++;
                }
                ml.AddMonSys(ls.Ip, ls.Mac, "tbSdachis", ins, upd);
            }

            if (ls.lsLogs != null)
            {
                upd = 0; ins = 0;
                var rp = db.GetRepository<tbLog>();
                await rp.InsertAsync(ls.lsLogs.ToArray());
                ins = ls.lsLogs.Count;

                ml.AddMonSys(ls.Ip, ls.Mac, "tbSdachis", ins, upd);
            }


            #endregion

            if (ml.Count > 0)
            {
                var rp = db.GetRepository<tbMonitorSync>();
                await rp.InsertAsync(ml);
                await db.SaveChangesAsync();

                Console.WriteLine("MAIN TABLE POST");
                CConsole.WriteLine(ml.ToValues(), ConsoleColor.Green);
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("MAIN TABLE POST");
                CConsole.WriteLine("Маълумот йук", ConsoleColor.Green);
                Console.WriteLine("");
            }


            await db.SaveChangesAsync();
            await db.CommitAsync();

            return true;
        }

        private async Task<bool> SaveTableRef()
        {
            var res = new spDataList();
            var sql = "select * from {0}";

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

            var str = CSerializerJson.ToStr(res);
            CFile.SaveFile(str, AppDomain.CurrentDomain.BaseDirectory + "ref.json");

            Console.WriteLine("REF TABLE GET");
            CConsole.WriteLine(sql, ConsoleColor.Red);
            CConsole.WriteLine(res.ToString(), ConsoleColor.Green);
            Console.WriteLine("");

            return true;
        }

        private async Task<bool> SaveTableMain()
        {
            var ls = new tbDataList();

            string sql = "select * from {0} ";
            string sqlV2 = "select * from {0} where discriminator = '{1}' ";

            #region set
            ls.lsDrugAdditionalInfoes = await db.FromSqlAsNoTracking<tbDrugAdditionalInfo>(string.Format(sql, "tbdrugadditionalinfos"));
            ls.lsBuyOnCredits = await db.FromSqlAsNoTracking<tbBuyOnCredit>(string.Format(sql, "tbbuyoncredits"));
            ls.lsCashbackCards = await db.FromSqlAsNoTracking<tbCashbackCard>(string.Format(sql, "tbcashbackcards"));
            ls.lsDiscountCard = await db.FromSqlAsNoTracking<tbDiscountCard>(string.Format(sql, "tbdiscountcards"));
            ls.lsCashExpenses = await db.FromSqlAsNoTracking<tbCashExpenses>(string.Format(sql, "tbcashexpenses"));
            ls.lsCashVoucher = await db.FromSqlAsNoTracking<tbCashVoucher>(string.Format(sqlV2, "tbdocs", "tbCashVoucher"));
            ls.lsCashVoucherItem = await db.FromSqlAsNoTracking<tbCashVoucherItem>(string.Format(sqlV2, "tbdocitems", "tbCashVoucherItem"));
            ls.lsComingProducts = await db.FromSqlAsNoTracking<tbComingProducts>(string.Format(sqlV2, "tbdocs", "tbComingProducts"));
            ls.lsComingProductsItem = await db.FromSqlAsNoTracking<tbComingProductsItem>(string.Format(sqlV2, "tbdocitems", "tbComingProductsItem"));
            ls.lsCustomers = await db.FromSqlAsNoTracking<tbCustomer>(string.Format(sql, "tbcustomers"));
            ls.lsChangeHistories = await db.FromSqlAsNoTracking<tbChangeHistory>(string.Format(sql, "tbchangehistories"));
            ls.lsDebtRepayments = await db.FromSqlAsNoTracking<tbDebtRepayment>(string.Format(sql, "tbdebtrepayments"));
            ls.lsEncashments = await db.FromSqlAsNoTracking<tbEncashment>(string.Format(sql, "tbencashments"));
            ls.lsInventories = await db.FromSqlAsNoTracking<tbInventory>(string.Format(sql, "tbinventories"));
            ls.lsInventoryItems = await db.FromSqlAsNoTracking<tbInventoryItem>(string.Format(sql, "tbinventoryitems"));
            ls.lsInventoryCurrentLists = await db.FromSqlAsNoTracking<tbInventoryCurrentList>(string.Format(sql, "tbinventorycurrentlists"));
            ls.lsMedicalInsurances = await db.FromSqlAsNoTracking<tbMedicalInsurance>(string.Format(sql, "tbmedicalinsurances"));
            ls.lsReturnOfProducts = await db.FromSqlAsNoTracking<tbReturnOfProduct>(string.Format(sql, "tbreturnofproducts"));
            ls.lsSdachis = await db.FromSqlAsNoTracking<tbSdachi>(string.Format(sql, "tbsdachis"));
            ls.lsTurnovers = await db.FromSqlAsNoTracking<tbTurnover>(string.Format(sql, "tbturnovers"));
            ls.lsUsers = await db.FromSqlAsNoTracking<tbUser>(string.Format(sql, "tbusers"));
            ls.lsRevaluationOfDrugs = await db.FromSqlAsNoTracking<tbRevaluationOfDrug>(string.Format(sql, "tbrevaluationofdrugs"));
            ls.lsWriteOffs = await db.FromSqlAsNoTracking<tbWriteOff>(string.Format(sql, "tbwriteoffs"));
            ls.lsDefectures = await db.FromSqlAsNoTracking<tbDefecture>(string.Format(sql, "tbdefectures"));
            ls.lsReceipts = await db.FromSqlAsNoTracking<tbReceipt>(string.Format(sql, "tbreceipts"));
            ls.lsSmenas = await db.FromSqlAsNoTracking<tbSmena>(string.Format(sql, "tbsmenas"));
            ls.lsOnlinePaySystem = await db.FromSqlAsNoTracking<tbOnlinePaySystem>(string.Format(sql, "tbonlinepaysystems"));

            #endregion

            Console.WriteLine("MAIN TABLE GET");
            CConsole.WriteLine(sql, ConsoleColor.Red);
            CConsole.WriteLine(ls.ToString(), ConsoleColor.Green);
            Console.WriteLine("");

            var str = CSerializerJson.ToStr(ls);
            CFile.SaveFile(str, AppDomain.CurrentDomain.BaseDirectory + "main.json");

            return true;
        }
    }
}
