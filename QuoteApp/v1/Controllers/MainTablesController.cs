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
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [SwaggerTag("Асосий таблицалар синхронизацияси")]
    public class MainTablesController : ControllerBase
    {
        private readonly IUnitOfWork db;
        private readonly IGenSqlService gen;

        public MainTablesController(IUnitOfWork _db, IGenSqlService _gen)
        {
            db = _db;
            gen = _gen;
        }


        [HttpPost("get")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult> ToClient([FromBody] TransferModel r)
        {
            try
            {
                var ls = new tbDataList();

                var s = await gen.GetMainSqlAsync(r);
                var sql = s.Item1;
                var sqlV2 = s.Item2;

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
               
                // User clientlaga kerak emas
                // ls.lsUsers = await db.FromSqlAsNoTracking<tbUser>(string.Format(sql, "tbusers"));
                ls.lsRevaluationOfDrugs = await db.FromSqlAsNoTracking<tbRevaluationOfDrug>(string.Format(sql, "tbrevaluationofdrugs"));
                ls.lsWriteOffs = await db.FromSqlAsNoTracking<tbWriteOff>(string.Format(sql, "tbwriteoffs"));
                ls.lsDefectures = await db.FromSqlAsNoTracking<tbDefecture>(string.Format(sql, "tbdefectures"));
                ls.lsReceipts = await db.FromSqlAsNoTracking<tbReceipt>(string.Format(sql, "tbreceipts"));
                ls.lsSmenas = await db.FromSqlAsNoTracking<tbSmena>(string.Format(sql, "tbsmenas"));
                ls.lsOnlinePaySystem = await db.FromSqlAsNoTracking<tbOnlinePaySystem>(string.Format(sql, "tbonlinepaysystems"));

                #endregion

                #region set monitor value
                var ml = new List<tbMonitorSync>();
                ml.AddMonSys(ls.Ip, ls.Mac, "tbChangeHistories", 0, 0, ls.lsChangeHistories.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbCustomers", 0, 0, ls.lsCustomers.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbInventories", 0, 0, ls.lsInventories.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbInventoryCurrentLists", 0, 0, ls.lsInventoryCurrentLists.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbReturnOfProducts", 0, 0, ls.lsReturnOfProducts.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbRevaluationOfDrugs", 0, 0, ls.lsRevaluationOfDrugs.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbSmenas", 0, 0, ls.lsSmenas.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbWriteOffs", 0, 0, ls.lsWriteOffs.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbDrugAdditionalInfoes", 0, 0, ls.lsDrugAdditionalInfoes.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbDiscounts", 0, 0, ls.lsDiscountCard.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbCashExpenses", 0, 0, ls.lsCashExpenses.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbEncashments", 0, 0, ls.lsEncashments.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbCashVoucher", 0, 0, ls.lsCashVoucher.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbComingProducts", 0, 0, ls.lsComingProducts.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbReceipts", 0, 0, ls.lsReceipts.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbTurnovers", 0, 0, ls.lsTurnovers.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbCashVoucherItem", 0, 0, ls.lsCashVoucher.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbComingProductsItem", 0, 0, ls.lsComingProductsItem.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbDefectures", 0, 0, ls.lsDefectures.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbCashbackCards", 0, 0, ls.lsCashbackCards.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbBuyOnCredits", 0, 0, ls.lsBuyOnCredits.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbDebtRepayments", 0, 0, ls.lsDebtRepayments.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbInventoryItems", 0, 0, ls.lsInventoryItems.Count);
                ml.AddMonSys(ls.Ip, ls.Mac, "tbSdachis", 0, 0, ls.lsSdachis.Count);

                if (ml.Count > 0)
                {
                    var rp = db.GetRepository<tbMonitorSync>();

                    await rp.InsertAsync(ml);
                    await db.SaveChangesAsync();
                }

                #endregion


                Console.WriteLine("MAIN TABLE GET");
                CConsole.WriteLine(sql, ConsoleColor.Red);
                CConsole.WriteLine(ls.ToString(), ConsoleColor.Green);
                Console.WriteLine("");

                return Ok(ls);
            }
            catch (Exception ee)
            {
                var li = new LogItem
                {
                    App = "waAptekaN3",
                    Url = "api/MainTables",
                    Stacktrace = ee.GetStackTrace(5),
                    Message = ee.GetAllMessages(),
                    Method = "Put"
                };
                CLogJson.Write(li);
                return Ok(new ResponseModelV1() { Status = -1, StatusMessages = ee.Message + Environment.NewLine + ee.StackTrace });
            }
        }


        [HttpPost("set")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<ResponseModelV1>> FromClient()
        {
            var data = Request.BodyReader.AsStream();
            var ls = CZip.Decompress<tbDataList>(data);
            var ml = new List<tbMonitorSync>();


            try
            {
                db.BeginTransaction();
                // db.SetForceMode();

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


                //   db.DetectChanges();
                await db.SaveChangesAsync();
                await db.CommitAsync();


                return Ok(new ResponseModelV1()
                {
                    Status = 1,
                    StatusMessages = "OK",
                    ServerDateTime = DateTime.Now.ToString("ddMMyyyyHHmmss")
                });

            }
            catch (Exception ee)
            {
                db.Rollback();
                var li = new LogItem
                {
                    App = "waAptekaN3",
                    Url = "api/MainTables",
                    Stacktrace = ee.GetStackTrace(5),
                    Message = ee.GetAllMessages(),
                    Method = "Post"
                };
                CLogJson.Write(li);

                return Ok(new ResponseModelV1() { Status = -1, StatusMessages = ee.Message + Environment.NewLine + ee.StackTrace });
            }
        }

    }
}